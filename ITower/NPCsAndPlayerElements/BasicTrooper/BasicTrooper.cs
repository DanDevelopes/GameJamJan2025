using Godot;
using System;
public class BasicTrooper : KinematicBody2D
{
    GroundMobileLogic ai;
	NavigationAgent2D navAgent;
	public BasicTrooper() 
	{
	
	}
	public override void _Ready()
	{
        ai = GetNode<GroundMobileLogic>("GroundMobileAI");
        GD.Print(this.Name);
        ai.ImportNpc(this.Name, true, 1.5f);
		navAgent = GetNode<NavigationAgent2D>("NavAgent");
		navAgent.Connect("velocity_computed", this, nameof(_OnVelocityComputed));
    }

    public override void _PhysicsProcess(float delta)
	{
		base._Process(delta);
		ai.SetNPCLocation(this.Position);
			
			//var newlocation = navAgent.GetNextLocation();
			//var direction = GlobalPosition.DirectionTo(newlocation);
			//var velocity = direction * navAgent.MaxSpeed;
			MoveAndSlide(ai.GetNewLocation());
		this.Rotation = ai.GetRotation();
			//GD.Print(GlobalPosition.DirectionTo(newlocation));
        
        //
        //GD.Print(ai.GetNewLocation());
		
    }
	private void Shoot() 
	{
	
	}
	
	private void Alert() 
	{
	
	}

	private int shootReadyInSamples;
	private int shotFinishedInSamples;
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event is InputEventMouseButton eventMouseButton)
		{ 
			if (Input.IsMouseButtonPressed(1))
			{
				var location = GetViewport().GetMousePosition();

                GD.Print($"Reaching mouseclick {location}");
				//navAgent.TargetLocation = location;
				ai.TargetLocation(location);
				
			}
		}
    }
	private void _OnVelocityComputed(Vector3 volocity)
	{

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
