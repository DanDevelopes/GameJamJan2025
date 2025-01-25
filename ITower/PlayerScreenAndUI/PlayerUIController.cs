using Godot;
using ITower.GlobalSetting;
using ITower.Level_Assets;
using System;

public class PlayerUIController : Node2D
{
    Camera2D povCam;
    public override void _Ready()
    {
        povCam = GetNode<Camera2D>("POVCam");
    }

    public override void _Process(float delta)
    {

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