using Godot;
using ITower.Level_Assets;
using ITower.NPCsAndPlayerElements.NPCLogic.StatsAndWieghting;
using System;
public class BasicTrooper : KinematicBody2D
{
    GroundMobileLogic ai;
	bool init = false;
	AnimatedSprite animatedSprite;
	NPCStats npcStats;
	Selecter selecter;
    bool isSelected;
	Vector2 initailPosition;
	bool initGone = false;
	public BasicTrooper()
	{

	}
	public void SetInitailPosition(Vector2 sentTo) 
	{
        initailPosition = sentTo;
    }
	public override void _Ready()
	{
		selecter = GetNode<Selecter>("Selecter");
        ai = GetNode<GroundMobileLogic>("GroundMobileAI");
        this.Name = ai.ImportNpc(this.Name, true, 0.2f, GetRid());
		animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		npcStats = SharedStats.getStats(this.Name);
		if(initailPosition != null) 
		{
			ai.TargetLocation(initailPosition);
		}
		else
		{
			ai.TargetLocation(GlobalPosition);
		}
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
		if (ai.ReachedLocation())
		{
			var moveTo = ai.GetNewLocation(this.GlobalPosition);
			ai.SetNPCLocation(this.GlobalPosition);

			MoveAndSlide(moveTo);
			var rot = ai.GetRotation();
			ai.Rotation = rot;
			animatedSprite.Rotation = rot;
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

    public override void _Input(InputEvent @event)
	{
		
		base._Input(@event);
		if (selecter.Selected())
		{
			if (@event is InputEventMouseButton eventMouseButton)
			{
				if (Input.IsMouseButtonPressed(1))
				{
					var location = SharedMapLogic.trueMousePosition;
					ai.TargetLocation(location);
				}
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
