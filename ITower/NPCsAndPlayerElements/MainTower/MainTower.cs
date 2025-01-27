using Godot;
using ITower.GlobalSetting;
using ITower.Level_Assets;
using ITower.NPCsAndPlayerElements.NPCLogic;
using ITower.NPCsAndPlayerElements.NPCLogic.StatsAndWieghting;
using System;
using static ITower.GlobalSetting.GlobalSettings.KeyBindings;

public class MainTower : KinematicBody2D
{

    Vector2 dropPoint;
    int resourceCounter;

    GroundMobileLogic ai;
    private float resourcePerciseCounter;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ai = GetNode<GroundMobileLogic>("AI");
        this.Name = ai.ImportNpc(this.Name, false, 0f, this.GetRid());
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        OtherElement.pointAvialable = resourceCounter;
        if(resourcePerciseCounter < 32)
        resourcePerciseCounter += delta / 2;
        
        base._Process(delta);
        resourceCounter = (int)Math.Round(resourcePerciseCounter);
        if (SharedStats.getStats(this.Name).health < 1) 
        {
            this.QueueFree();
            //Mission Failed State here
        }
    }
    private void DropBomb()
    {
        if (resourceCounter > 16)
        {
            resourceCounter -= 16;
            resourcePerciseCounter -= 16;
            PackedScene bombScene = (PackedScene)ResourceLoader.Load("res://NPCsAndPlayerElements/Bomb/Bomb.tscn");
            Bomb bomb = (Bomb)bombScene.Instance();
            bomb.GlobalPosition = SharedMapLogic.trueMousePosition - this.GlobalPosition;
            AddChild(bomb);
        }
    }
    private void SpawnTroop() 
    {
        if (resourceCounter > 6)
        {

            resourceCounter -= 6;
            resourcePerciseCounter -= 6;
            PackedScene troopScene = (PackedScene)ResourceLoader.Load("res://NPCsAndPlayerElements/BasicTrooper/BasicTrooper.tscn");
            BasicTrooper basicTrooper = (BasicTrooper)troopScene.Instance();
            basicTrooper.SetInitailPosition(new Vector2() { y = -200, x = 0 });
            AddChild(basicTrooper);
        }
    }
    private void SpawnSmallTurret() 
    {
        if (resourceCounter > 12)
        {
            resourceCounter -= 12;
            resourcePerciseCounter -= 12;
            PackedScene turretScene = (PackedScene)ResourceLoader.Load("res://NPCsAndPlayerElements/SmallTurret/SmallTurret.tscn");
            SmallTurret smallTurret = (SmallTurret)turretScene.Instance();
            smallTurret.GlobalPosition = SharedMapLogic.trueMousePosition - this.GlobalPosition;
            AddChild(smallTurret);
        }
        else
        {
            GD.Print($"{resourceCounter} {resourcePerciseCounter}");
        }
    }
    private void GetDropPoint() 
    {
        dropPoint = SharedMapLogic.trueMousePosition;
    }
    public override void _Input(InputEvent @event)
    {

        base._Input(@event);
        if (@event is InputEventKey keyPress)
        {
            if(keyPress.Scancode == GetKeyBindings()[Actions.DropSmallTurretAtLocation] && keyPress.Pressed)
            {
                GetDropPoint();
                SpawnSmallTurret();
            }
            if (keyPress.Scancode == GetKeyBindings()[Actions.RequestBasicTroop] && keyPress.Pressed)
            {
                SpawnTroop();
            }
            if (keyPress.Scancode == GetKeyBindings()[Actions.DropBombAtLocation] && keyPress.Pressed)
            {
                DropBomb();
            }
        }
    }
}
