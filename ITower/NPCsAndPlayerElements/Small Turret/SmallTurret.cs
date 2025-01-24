using Godot;
using System;
using System.ComponentModel;

public class SmallTurret : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    
    float rotationStage = 0;
    // Called when the node enters the scene tree for the first time.
    GroundMobileLogic groundMobileLogic;
    AnimatedSprite turret;
    bool isShooting;
    float shootTimer;
    float turnBy = 0.02f;
    bool directionSwitch;
    float tau = (float)Math.PI * 2;
    private int animationFrame;

    public override void _Ready()
    {
        groundMobileLogic = GetNode<GroundMobileLogic>("GroundMobileAI");
        groundMobileLogic.ImportNpc(Name, false, 0.44f);
        turret = GetNode<AnimatedSprite>("GroundMobileAI/AnimatedBody");
        turret.Frame = 0;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
         
        if (isShooting)
        {
            isShooting = IsStillShoot(delta);
            turret.Frame = animationFrame;
            return;
        }
        if(groundMobileLogic.IsTargetInsight())
        {
            isShooting = true;
            animationFrame = 0;
            
        }
        if (directionSwitch)
        {
            rotationStage += turnBy;
        }
        else
        {
            rotationStage -= turnBy;
        }
        if (rotationStage > (tau * 2))
        {
            directionSwitch = false;
        }
        if (rotationStage < (tau / 50))
        {
            directionSwitch = true;
        }
        groundMobileLogic.Rotation = rotationStage;
        turret.Frame = animationFrame;
    }

    private bool IsStillShoot(float delta)
    {
        shootTimer += delta * 5;
        if ( animationFrame == 1 && groundMobileLogic.Shoot())
        {

            animationFrame = (int)shootTimer;
            return true;
        }
        if (animationFrame <= 3)
        {
            animationFrame = (int)shootTimer;
            return true;
        }
        shootTimer = 0;
        animationFrame = 0;
        
        return false;
    }
}