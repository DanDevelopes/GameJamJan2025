using Godot;
using ITower.GlobalSetting;
using ITower.Level_Assets;
using ITower.NPCsAndPlayerElements.NPCLogic;
using ITower.NPCsAndPlayerElements.NPCLogic.StatsAndWieghting;
using System;
using System.Data.Odbc;
using System.Linq;
using static ITower.GlobalSetting.GlobalSettings.KeyBindings;

public class PlayerUIController : Node2D
{
    Camera2D povCam;
    string TowerUniqueName;
    string controlDisplay;
    RichTextLabel hudControlText;
    RichTextLabel hudInfoText;
    public override void _Ready()
    {
        povCam = GetNode<Camera2D>("POVCam");
        hudControlText = GetNode<RichTextLabel>("Hud/HudControlPanel/HudControlText");
        hudInfoText = GetNode<RichTextLabel>("Hud/HudInfoPanel/HudInfoText");
        controlDisplay = WriteControlSettings();
        povCam.ResetSmoothing();
    }
    private string WriteControlSettings() 
    {
        string controls = "Controls";
        try
        {
            foreach (var action in Enum.GetNames(typeof(Actions)))
            {
                Actions a;
                Actions.TryParse(action, out a);
                var key = (KeyList)GetKeyBindings()[a];
                controls += $"\n {action}:{Enum.GetName(typeof(KeyList),key)}";
            }
            controls += $"\n left click to move and assign troop\n right click to disengage";
        }
        catch(Exception ex)
        {
            controls = $"{ex}";
        }
        return controls;
    }
    public string GetInformation() 
    {
        var towerName = LevelInfo.npcNames.Where(x => x.Contains("MainTower")).FirstOrDefault(); // question can you or can you not use linq?
        var towerHealth = SharedStats.getStats(towerName).health;
        string message = $"Tower Health: {towerHealth}";
        message += $"\nRequest Points: {LevelInfo.pointAvialable}";
        message += $"\nTroop Cost: 6rp";
        message += $"\nTurret Cost: 12rp";
        message += $"\nBomb Cost: 16";
        return message;
    }
    public override void _Process(float delta)
    {
        hudControlText.Text = GetInformation();
        hudInfoText.Text = WriteControlSettings();
        var viewportMousePos = GetViewport().GetMousePosition();
        var cameraZoom = povCam.Zoom;
        var cameraPosition = povCam.GlobalPosition;
        var viewportSize = GetViewport().Size;
        var worldMousePos = (viewportMousePos - viewportSize / 2) * cameraZoom + cameraPosition;
        SharedMapLogic.trueMousePosition = worldMousePos;
    }
    public override void _UnhandledInput(InputEvent @event)
    {
        Vector2 pos = this.Position;
        if (@event is InputEventKey eventKey)
        {
            if (eventKey.Pressed && eventKey.Scancode == GlobalSettings.KeyBindings.GetKeyBindings()[GlobalSettings.KeyBindings.Actions.scrollup])
                pos.y -= 5;
            if (eventKey.Pressed && eventKey.Scancode == GlobalSettings.KeyBindings.GetKeyBindings()[GlobalSettings.KeyBindings.Actions.scrolldown])
                pos.y += 5;
            if (eventKey.Pressed && eventKey.Scancode == GlobalSettings.KeyBindings.GetKeyBindings()[GlobalSettings.KeyBindings.Actions.scrollright])
                pos.x += 5;
            if (eventKey.Pressed && eventKey.Scancode == GlobalSettings.KeyBindings.GetKeyBindings()[GlobalSettings.KeyBindings.Actions.scrollleft])
                pos.x -= 5;
            if (eventKey.Pressed && eventKey.Scancode == GlobalSettings.KeyBindings.GetKeyBindings()[GlobalSettings.KeyBindings.Actions.zoomin])
            {
                var zoom = povCam.Zoom;
                zoom.x = zoom.x * 0.8f;
                zoom.y = zoom.y * 0.8f;
                povCam.Zoom = zoom;
            }
            if (eventKey.Pressed && eventKey.Scancode == GlobalSettings.KeyBindings.GetKeyBindings()[GlobalSettings.KeyBindings.Actions.zoomout])
            {
                var zoom = povCam.Zoom;
                zoom.x = zoom.x * 1.2f;
                zoom.y = zoom.y * 1.2f;
                povCam.Zoom = zoom;
                
            }
        }
        this.Position = pos;
    }
}