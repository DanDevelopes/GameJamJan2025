using Godot;
using ITower.NPCsAndPlayerElements.NPCLogic.StatsAndWieghting;
using System;
public class BasicTrooper : KinematicBody2D
{
    GroundMobileLogic ai;
	bool init = false;
	AnimatedSprite animatedSprite;
	NPCStats npcStats;
	public BasicTrooper() 
	{
	
	}
	public override void _Ready()
	{
        ai = GetNode<GroundMobileLogic>("GroundMobileAI");
        ai.ImportNpc(this.Name, true, 0.2f);
		animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		npcStats = SharedStats.getStats(this.Name);
    }

    public override void _PhysicsProcess(float delta)
	{
        npcStats = SharedStats.getStats(this.Name);
		if (npcStats.health < 1)
		{
			this.QueueFree();
		}
        if (isShooting)
        {
            isShooting = IsStillShoot(delta);
            animatedSprite.Frame = animationFrame;
            return;
        }
        if (ai.IsTargetInsight())
        {
            isShooting = true;
            animationFrame = 0;

        }
        base._Process(delta);
		Initualize();
		
        ai.SetNPCLocation(this.Position);
		
        MoveAndSlide(ai.GetNewLocation());
		this.Rotation = ai.GetRotation();
		
    }
    private bool IsStillShoot(float delta)
    {
        shootTimer += delta;
        if (ai.Shoot() && animationFrame > 0)
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
			shootTimer = 4;
			animationFrame = 4;
			return false;
		}
		
        return false;
    }
    private void Shoot() 
	{
		
	}
	
	private void Alert() 
	{
	
	}
	private void Initualize()
	{
		if(!init)
		{
            ai.VisionSetup();
			init = true;
        }

	}
	private int shootReadyInSamples;
	private int shotFinishedInSamples;
    private float shootTimer;
    private bool isShooting;
    private int animationFrame;

    public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event is InputEventMouseButton eventMouseButton)
		{ 
			if (Input.IsMouseButtonPressed(1))
			{
				var location = GetViewport().GetMousePosition();
				ai.TargetLocation(location);
				
			}
		}
    }
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
