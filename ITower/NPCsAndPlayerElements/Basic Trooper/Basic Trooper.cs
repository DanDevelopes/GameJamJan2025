using Godot;
using System;
using ITower.NPCs;
public class Basic_Trooper : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	NPCStats stats;
	private bool targetInSight = false;
	private bool targetInRange = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public override void _Process(float delta)
	{
		base._Process(delta);

	}
	private void Shoot() 
	{
	
	}
	
	private void Alert() 
	{
	
	}

	private int shootReadyInSamples;
	private int shotFinishedInSamples;
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
