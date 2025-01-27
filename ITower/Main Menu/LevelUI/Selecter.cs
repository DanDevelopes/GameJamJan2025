using Godot;
using ITower.Level_Assets;
using System;

public class Selecter : Node2D
{
    private bool isSelected;
    bool isHovered;
    private Position2D topRight;
    private Position2D bottomLeft;
    private AnimatedSprite animatedSprite;

    internal bool Selected()
    {
        return isSelected;
    }

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        topRight = GetNode<Position2D>("TopRight");
        bottomLeft = GetNode<Position2D>("BottomLeft");
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (isSelected)
        {
            animatedSprite.Frame = 0;
            animatedSprite.Visible = true;
            return;
        }
        if (isHovered)
        {
            animatedSprite.Frame = 1;
            animatedSprite.Visible = true;
            return;
        }
        animatedSprite.Visible = false;
    }
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        var location = SharedMapLogic.trueMousePosition;
        if (
            location.x > topRight.GlobalPosition.x && location.y > topRight.GlobalPosition.y
            &&
            location.y < bottomLeft.GlobalPosition.y && location.x < bottomLeft.GlobalPosition.x
            )
        {
            isHovered = true;
            if (@event is InputEventMouseButton)
            {
                if (Input.IsMouseButtonPressed(1))
                {
                    isSelected = true;
                }
            }
        }
        else 
        {
            isHovered = false;
        }
        if (@event is InputEventMouseButton)
        {
            if (Input.IsMouseButtonPressed(2))
            {
                isSelected = false;
            }
        }
    }
}
