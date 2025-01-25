using Godot;
using ITower.Level_Assets;
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
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ai = GetNode<GroundMobileLogic>("AI");
        npcStats = new NPCStats();

        ai.ImportNpc(this.Name, true, 0.2f, GetRid());
    }
    public override void _Process(float delta)
    {
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
        animationFrame = 0;
        base._Process(delta);

        ai.SetNPCLocation(this.Position);

        MoveAndSlide(ai.GetNewLocation());
        this.Rotation = ai.GetRotation();

    }
    private bool IsStillShoot(float delta)
    {
        shootTimer += delta * 5;
        if (animationFrame > 5 && ai.Shoot())
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
    private void Shoot()
    {

    }

    private void Alert()
    {

    }

    private int shootReadyInSamples;
    private int shotFinishedInSamples;
    private float shootTimer;
    private bool isShooting;
    bool shootMode;
    private int animationFrame;

}
