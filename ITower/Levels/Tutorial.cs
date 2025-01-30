using Godot;
using ITower.GlobalSetting;
using ITower.Level_Assets;
using ITower.NPCsAndPlayerElements.NPCLogic;
using ITower.NPCsAndPlayerElements.NPCLogic.StatsAndWieghting;
using System;
using System.Linq;

public class Tutorial : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    RichTextLabel pressTAndClick;
    RichTextLabel pressClickTwice;
    RichTextLabel attackTarget;
    RichTextLabel pressEOrX;
    RichTextLabel pressWASD;
    LevelLogic levelLogic;
    int rushedCounter = -1;
    int enemyCount;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        levelLogic = GetNode<LevelLogic>("LevelLogic");
        pressTAndClick = GetNode<RichTextLabel>("PressTAndClick");
        attackTarget = GetNode<RichTextLabel>("AttackTarget");
        pressClickTwice = GetNode<RichTextLabel>("Movement");
        pressEOrX = GetNode<RichTextLabel>("PressEOrX");
        pressWASD = GetNode<RichTextLabel>("PressWASD");
        pressWASD.Visible = false;
        pressTAndClick.Visible = true;
        pressEOrX.Visible = false;
        attackTarget.Visible = false;
        pressClickTwice.Visible = false;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (rushedCounter < 0)
        {
            var listOfEnemies = LevelInfo.npcNames.Where(x => SharedStats.getStats(x).isPlayer == false && SharedStats.getStats(x).health > 1).ToList();
            enemyCount = listOfEnemies.Count();
        }
        if(rushedCounter == 2)
        {
            
            if (LevelInfo.npcNames.Where(x => SharedStats.getStats(x).isPlayer == false && SharedStats.getStats(x).health > 1).ToList().Count() < enemyCount)
            {
                attackTarget.Visible = false;
                pressClickTwice.Visible = false;
                pressEOrX.Visible = true;
                rushedCounter = 3;
            }
        }
    }
    public override void _Input(InputEvent @event)
    {

        base._Input(@event);
        if (@event is InputEventMouseButton eventMouseButton)
        {
            if (Input.IsMouseButtonPressed(1) && rushedCounter == 0)
            {
                pressTAndClick.Visible = false;
                pressWASD.Visible = true;
                pressClickTwice.Visible = true;
                rushedCounter = 1;
            }
        }
        if (@event is InputEventKey eventKey)
        {
            if (eventKey.Pressed && eventKey.Scancode == GlobalSettings.KeyBindings.GetKeyBindings()[GlobalSettings.KeyBindings.Actions.RequestBasicTroop] 
                &&
                rushedCounter == -1
                )
                rushedCounter = 0;
            if (rushedCounter == 1)
            {
                if (eventKey.Pressed && eventKey.Scancode == GlobalSettings.KeyBindings.GetKeyBindings()[GlobalSettings.KeyBindings.Actions.scrollup])
                    pressWASD.Visible = false;
                if (eventKey.Pressed && eventKey.Scancode == GlobalSettings.KeyBindings.GetKeyBindings()[GlobalSettings.KeyBindings.Actions.scrolldown])
                    pressWASD.Visible = false;
                if (eventKey.Pressed && eventKey.Scancode == GlobalSettings.KeyBindings.GetKeyBindings()[GlobalSettings.KeyBindings.Actions.scrollright])
                    pressWASD.Visible = false;
                if (eventKey.Pressed && eventKey.Scancode == GlobalSettings.KeyBindings.GetKeyBindings()[GlobalSettings.KeyBindings.Actions.scrollleft])
                    pressWASD.Visible = false;
                var listOfEnemies = LevelInfo.npcNames.Where(x => SharedStats.getStats(x).isPlayer == false && SharedStats.getStats(x).health > 1).ToList();
                attackTarget.Visible = true;
                rushedCounter = 2;
            }
            
            if (rushedCounter == 3)
            {

                if (eventKey.Pressed && eventKey.Scancode == GlobalSettings.KeyBindings.GetKeyBindings()[GlobalSettings.KeyBindings.Actions.DropSmallTurretAtLocation] 
                    ||
                    eventKey.Pressed && eventKey.Scancode == GlobalSettings.KeyBindings.GetKeyBindings()[GlobalSettings.KeyBindings.Actions.DropBombAtLocation]
                    )
                {
                    pressEOrX.Visible = false;
                }
            }
        }
    }
}
