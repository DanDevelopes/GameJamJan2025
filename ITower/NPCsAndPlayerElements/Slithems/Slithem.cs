using Godot;
using ITower.Level_Assets;
using ITower.NPCsAndPlayerElements.NPCLogic;
using ITower.NPCsAndPlayerElements.NPCLogic.StatsAndWieghting;
using System;

public class Slithem : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    GroundMobileLogic ai;
    NPCStats npcStats;
    AnimatedSprite animatedSprite;
    string targetName = string.Empty;
    private int shootReadyInSamples;
    private int shotFinishedInSamples;
    private float shootTimer;
    private float animationTimer;
    private bool isShooting;
    bool shootMode;
    private int animationFrame = 0;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ai = GetNode<GroundMobileLogic>("AI");
        npcStats = new NPCStats();
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        ai.ImportNpc(this.Name, true, 0.2f, GetRid());
    }
    public override void _Process(float delta)
    {
        if(delta != 0)
            animationTimer += delta;
        GetTargetLocation();
        npcStats = SharedStats.getStats(this.Name);
        if (npcStats.health < 1)
        {
            this.QueueFree();
        }
        if (isShooting && shootMode)
        {
            var result = IsStillShoot(delta);
            shootMode = result;
            isShooting = result;
            animatedSprite.Frame = animationFrame;
            return;
        }
        if (ai.IsTargetInsight())
        {
            isShooting = true;
            shootMode = true;
            animationFrame = 0;
            return;
        }
        if (animationFrame < 4 || animationFrame > 7)
            animationTimer = 4f;
        animationFrame = (int)Math.Round(animationTimer);
        animatedSprite.Frame = animationFrame;
        animationFrame++;
        base._Process(delta);
        var moveTo = ai.GetNewLocation(this.GlobalPosition);
        ai.SetNPCLocation(this.Position);

        MoveAndSlide(moveTo);
        this.Rotation = ai.GetRotation();

    }
    private bool IsStillShoot(float delta)
    {
        shootTimer += delta * 5;
        if (animationFrame > 5 && ai.Attack())
        {
            animationFrame = (int)shootTimer;
            return true;
        }
        if (animationFrame <= 7)
        {
            animationFrame = (int)shootTimer;
            return true;
        }
        if (animationFrame > 7)
        {
            shootTimer = 5;
            animationFrame = 5;
        }
        if (!ai.IsTargetInsight())
        {
            shootTimer = 0;
            animationFrame = 0;
        }
        return false;
    }
    private void Bite()
    {

    }
    private void GetTargetLocation() 
    {
        if (targetName == string.Empty)
        {
            targetName = GetTarget();
        }
        if (targetName == string.Empty)
            return;
        if (SharedStats.getStats(targetName).health <= 0)
        {
            GetTarget();
        }
        var myPositions = OtherElement.GetPosition(targetName);
        ai.TargetLocation(myPositions[targetName]);
    }

    private string GetTarget() 
    {
        foreach (var npcName in OtherElement.npcNames)
        {
            // where 
            if (SharedStats.getStats(npcName).health > 0 && SharedStats.getStats(npcName).isPlayer == true)
            {
                // select I miss linq so much
                return npcName;
                
            }

        }
        return string.Empty;
    }
    private void Alert()
    {

    }

    

}
