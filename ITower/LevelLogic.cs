using Godot;
using ITower.NPCsAndPlayerElements.NPCLogic;
using ITower.NPCsAndPlayerElements.NPCLogic.StatsAndWieghting;
using System;
using System.Linq;

public class LevelLogic : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        var listOfEnemies = OtherElement.npcNames.Where(x => SharedStats.getStats(x).isPlayer == false && SharedStats.getStats(x).health > 1).ToList();
        if (listOfEnemies.Count() < 1)
        {
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        GD.Print("Level Complete");
    }
}
