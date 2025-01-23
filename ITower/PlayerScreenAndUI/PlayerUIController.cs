using Godot;
using ITower.GlobalSetting;

public class PlayerUIController : Node2D
{
    Camera2D pOVCam;
    public override void _Ready()
    {
        pOVCam = GetNode<Camera2D>("POVCam");
    }

    public override void _Process(float delta)
    {

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
                var zoom = pOVCam.Zoom;
                zoom.x = zoom.x * 0.8f;
                zoom.y = zoom.y * 0.8f;
                pOVCam.Zoom = zoom;
            }
            if (eventKey.Pressed && eventKey.Scancode == GlobalSettings.KeyBindings.GetKeyBindings()[GlobalSettings.KeyBindings.Actions.zoomout])
            {
                var zoom = pOVCam.Zoom;
                zoom.x = zoom.x * 1.2f;
                zoom.y = zoom.y * 1.2f;
                pOVCam.Zoom = zoom;
                
            }
        }
        this.Position = pos;
    }
}