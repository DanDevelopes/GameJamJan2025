using Godot;
using ITower.NPCsAndPlayerElements.NPCLogic;
using ITower.NPCsAndPlayerElements.NPCLogic.StatsAndWieghting;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class GroundMobileLogic : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    double tau = Math.PI * 2;
    int sweepStage;
    float rotation;
    Vector2 visionEnd;
    NPCStats stats;
    RayCast2D entityVison;
    Timer shootTimer;
    Timer readyToFireTimer;
    NavigationAgent2D navAgent;
    double visionWidthInRadions;
    int rangeOfView;
    bool isMobile;
    List<String> Enemies;
    private string npcName;
    private Vector2 NPCLocation;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
        entityVison = GetNode<RayCast2D>("EntityVison");
        shootTimer = GetNode<Timer>("ShootTimer");
        readyToFireTimer = GetNode<Timer>("ReadyToFireTimer"); ;
        navAgent = GetNode<NavigationAgent2D>("NavAgent");
        GD.Print($"{this.Name} is init");
        if (navAgent == null) ;
    }
    public void ImportNpc(string npcName, bool isMobile, float visionWidth)
    {
        stats = new NPCStats();
        this.npcName = npcName;

        GD.Print(npcName);
        var npcType = Regex.Replace(npcName, @"[\d-]", string.Empty);
        GD.Print($"troop type:{npcType}");
        var multipliers = NPCWieghting.npcMultipliers[npcType];
        stats.health = (int) Math.Round(stats.health * multipliers.health);
        stats.maxHealth = (int) Math.Round(stats.maxHealth * multipliers.maxHealth);
        stats.speed = (int) Math.Round(stats.speed * multipliers.speed);
        stats.damage = (int) Math.Round(stats.damage * multipliers.damage);
        stats.accuracy = (int) Math.Round(stats.accuracy * multipliers.accuracy);
        stats.feildOfView = (int) Math.Round(stats.feildOfView * multipliers.feildOfView);
        stats.sanity = (int)Math.Round(stats.sanity * multipliers.sanity);
        stats.moral = (int)Math.Round(stats.moral * multipliers.moral);
        stats.intelegent = (int)Math.Round(stats.intelegent * multipliers.intelegent);
        GD.Print("Inintialized stats");
        SharedStats.addStats(npcName, stats);
        this.isMobile = isMobile;
        visionWidthInRadions = visionWidth;
        
    }
    public void VisionSweep()
    {
        const int totalSweepStages = 20;
        var sweepPoint = (tau - visionWidthInRadions) / totalSweepStages;

        if (sweepStage >= totalSweepStages)
            sweepStage = 0;

        var sweepAngle = sweepPoint * sweepStage + (visionWidthInRadions / 2);

        visionEnd = new Vector2() { 
            x = rangeOfView * (float)Math.Cos(sweepAngle), 
            y = rangeOfView * (float)Math.Sin(sweepAngle)
        };
        
        sweepStage++;
        entityVison.CastTo = visionEnd;
        entityVison.ForceRaycastUpdate();
    }
    
    public void SetVisionRange(int range)
    {
        if(isMobile)
        rangeOfView = range;
    }
    public Dictionary<string, bool> GetVisibleEntities()
    {
        Dictionary<string, bool> visibleEntities = new Dictionary<string, bool>();
        
        return null;
    }
    public void SetEnemyTypes(List<string> Enemies) 
    {
        this.Enemies = Enemies;
    }
    public void SetMovementSpeed() 
    {
        
    }
    public void SetReadyToFire()
    {
        
    }

    public void SetModifiersAndWieghting() 
    { 
        
    }
    public float GetRotation() 
    {
        return rotation;
    }
    public Vector2 GetNewLocation()
    {
        GD.Print(stats.speed);
        var newlocation = navAgent.GetNextLocation();
        var direction = GlobalPosition.DirectionTo(newlocation);

        
        
        rotation = NPCLocation.y - newlocation.y / NPCLocation.x - newlocation.x; 
        var velocity = direction * stats.speed * 2;
        rotation = velocity.Angle();
        GD.Print(direction);
        return velocity;
        
    }
    public void TargetLocation(Vector2 location)
    {
        navAgent.TargetLocation = location;
    }
    public void SetNPCLocation(Vector2 location) 
    {
        NPCLocation = location;
    }
    public NPCStats transmitStats()
    {
        stats = SharedStats.getStats(npcName);
        return stats;
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.

}
