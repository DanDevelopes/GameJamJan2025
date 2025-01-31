using Godot;
using ITower.NPCsAndPlayerElements.NPCLogic;
using ITower.NPCsAndPlayerElements.NPCLogic.StatsAndWieghting;
using System;
using System.Diagnostics;

public class Bomb : Area2D
{
    // Declare member variables here. Examples:
    AnimatedSprite animatedSprite;
    // private string b = "text";
    int counter;
    float timer = -16;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        Visible = false;
    }


    public override void _Process(float delta)
    {
        Connect("body_entered", this, nameof(_FindVictim));
        Connect("body_exited", this, nameof(_FindVictim));
        timer += delta * 8;
        if (timer >= 0)
        {
            counter = (int)Math.Round(timer);
            Visible = true;
        }
        animatedSprite.Frame = counter;
        if (counter > 4)
            this.QueueFree();
    }
    private void _FindVictim(Node2D body) 
    {
        if(LevelInfo.npcNames.Contains(body.Name) && timer >= 0)
            SharedStats.getStats(body.Name).health = 0;
    }
}
