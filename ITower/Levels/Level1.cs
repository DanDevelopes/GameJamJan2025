using Godot;
using ITower.Level_Assets;
using ITower.NPCsAndPlayerElements.NPCLogic;
using ITower.NPCsAndPlayerElements.NPCLogic.StatsAndWieghting;
using System;
using System.Collections.Generic;
using System.Linq;

public class Level1 : Node2D
{
    RichTextLabel gameoverScreen;
    bool isGameOver;
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        gameoverScreen = GetNode<RichTextLabel>("PlayerUIController/FailedMessage");
        gameoverScreen.Visible = false;
        LevelInfo.level = 1;
        isGameOver = false;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        base._Process(delta);
        string mainTowerName = LevelInfo.npcNames.Where(x => x.Contains("MainTower")).FirstOrDefault();
        if (SharedStats.getStats(mainTowerName).health < 0)
        {
            gameoverScreen.Visible = true;
            isGameOver = true;
        }
    }
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event is InputEventMouseButton eventMouseButton)
        {
            if (isGameOver)
            {
                SharedStats.ClearStats();
                LevelInfo.npcNames.Clear();
                GetTree().ChangeScene(GetTree().CurrentScene.Filename);
            }
        }
    }
}
