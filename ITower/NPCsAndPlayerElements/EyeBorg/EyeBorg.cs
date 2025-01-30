using Godot;
using ITower.NPCsAndPlayerElements.NPCLogic;
using ITower.NPCsAndPlayerElements.NPCLogic.StatsAndWieghting;
using System;

public class EyeBorg : KinematicBody2D
{
    GroundMobileLogic ai;
    bool init = false;
    AnimatedSprite animatedSprite;
    NPCStats npcStats;
    LevelLogic levelLogic;
    public EyeBorg()
    {

    }
    public override void _Ready()
    {
        levelLogic = new LevelLogic();
        ai = GetNode<GroundMobileLogic>("GroundMobileAI");
        this.Name = ai.ImportNpc(this.Name, true, 0.2f, GetRid());
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        npcStats = SharedStats.getStats(this.Name);
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        npcStats = SharedStats.getStats(this.Name);
        if (npcStats.health < 1)
        {
            this.QueueFree();
        }
        
        GetTargetLocation();
        
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
        var moveTo = ai.GetNewLocation(this.GlobalPosition);
        ai.SetNPCLocation(this.GlobalPosition);
        if (levelLogic.GetLevel() > 0)
        {
            MoveAndSlide(moveTo);
            this.Rotation = ai.GetRotation();
        }
        
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
    private string GetTarget()
    {
        foreach (var npcName in LevelInfo.npcNames)
        {
            if (SharedStats.getStats(npcName).health > 0 && npcName.Contains("Slithem"))
            {
                return npcName;
            }
            else if (SharedStats.getStats(npcName).health > 0 && SharedStats.getStats(npcName).isPlayer == true)
            {
                return npcName;
            }

        }
        foreach (var npcName in LevelInfo.npcNames)
        {
            if (SharedStats.getStats(npcName).health > 0 && SharedStats.getStats(npcName).isPlayer == true)
            {
                return npcName;
            }

        }
        return string.Empty;
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
        var myPositions = LevelInfo.GetPosition(targetName);
        ai.TargetLocation(myPositions[targetName]);
    }

    private int shootReadyInSamples;
    private int shotFinishedInSamples;
    private float shootTimer;
    private bool isShooting;
    bool shootMode;
    private int animationFrame;
    private string targetName = string.Empty; //string.Empty does not equal "" or string myString as default/null took me ages with no debug to find out

    private static class Animate
    {
        static Animate()
        {
            frame = 0;
        }
        private static int frame;
        private static int shootProcess;
        private static int fireReady;
        public static void FireReady()
        {

        }
        public static void Shoot()
        {

        }

    }
}
