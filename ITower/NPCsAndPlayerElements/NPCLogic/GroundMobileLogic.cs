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
    

    

    NPCStats stats;
    NavigationAgent2D navAgent;
    double visionWidthInRadions;
    int rangeOfView = 200;
    bool isMobile;
    List<String> Enemies;
    private string npcName;
    private Vector2 npcLocation;
    private List<RayCast2D> visionCasts;
    string targetNPC;
    string npcType;
    private bool isDebug = false;
    float fastestVelocity;
    private bool isLocationReached;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        visionCasts = new List<RayCast2D>();
        
        navAgent = GetNode<NavigationAgent2D>("NavAgent");
    }
    public string ImportNpc(string npcName, bool isMobile, float visionWidth, RID rID)
    {
        npcType = Regex.Replace(npcName, @"[\d-]", string.Empty);
        this.npcName = npcName + this.GetInstanceId().ToString();
        LevelInfo.npcNames.Add(this.npcName);
        
        stats = new NPCStats();
        
        var multipliers = NPCWieghting.npcMultipliers[npcType];
        stats.health = (int) Math.Round(stats.health * multipliers.health);
        stats.maxHealth = (int) Math.Round(stats.maxHealth * multipliers.maxHealth);
        stats.speed = (int) Math.Round(stats.speed * multipliers.speed);
        stats.damage = (int) Math.Round(stats.damage * multipliers.damage);
        stats.accuracy = (int) Math.Round(stats.accuracy * multipliers.accuracy);
        stats.feildOfView = (int) Math.Round(stats.feildOfView * multipliers.feildOfView);
        stats.sanity = (int)Math.Round(stats.sanity * multipliers.sanity);
        stats.rangeOfView = (int) Math.Round(stats.rangeOfView * multipliers.rangeOfView);
        stats.moral = (int)Math.Round(stats.moral * multipliers.moral);
        stats.intelegent = (int)Math.Round(stats.intelegent * multipliers.intelegent);
        stats.isPlayer = multipliers.isPlayer;
        SharedStats.addStats(this.npcName, stats);
        
        this.isMobile = isMobile;
        visionWidthInRadions = ((double)stats.feildOfView / 100) * tau;
        VisionSetup(rID);
        SetNPCLocation(this.GlobalPosition);
        return this.npcName;
    }
    private void ForcePath() 
    {
        navAgent.TargetLocation = navAgent.GetFinalLocation();
        fastestVelocity = 0;
    }
    public void VisionSetup(RID rID)
    {
        RayCast2D ray;
        List<Vector2> debugVectors;
        List<Line2D> visionLine;
        for (int casts = 0; casts < 20; casts++)
        {
            ray = new RayCast2D();
            ray.CollisionMask = 2;
            ray.Enabled = true;
            ray.AddExceptionRid(rID);
            debugVectors = new List<Vector2>();
            visionLine = new List<Line2D>();
            var sweepPoint = visionWidthInRadions / 20;
            var sweepAngle = sweepPoint * casts;
            sweepAngle = sweepAngle - visionWidthInRadions / 2;
            ray.CastTo = new Vector2()
            {
                x = stats.rangeOfView * (float)Math.Cos(sweepAngle) + this.Position.x,
                y = stats.rangeOfView * (float)Math.Sin(sweepAngle) + this.Position.y
            };
            this.AddChild(ray);
            visionCasts.Add(ray);
            
            debugVectors.Clear();
            debugVectors.Add(
                new Vector2()
                {
                    x = this.Position.x,
                    y = this.Position.y
                }
                );
            debugVectors.Add(
                ray.CastTo
                );
            if (npcType != "Slithem")
            {
                Line2D line = new Line2D();
                line.Position = ray.Position;
                line.Points = debugVectors.ToArray();
                line.Width = 6;
                if (stats.isPlayer)
                    line.DefaultColor = new Color(0.6f, 1f, 0.6f, 0.05f);
                else
                    line.DefaultColor = new Color(1f, 0.6f, 0.6f, 0.05f);
                this.AddChild(line);
                visionLine.Add(line);
            }
            
        }
        
    }
    
    public void SetVisionRange(int range)
    {
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
    public bool IsTargetInsight() 
    {
        foreach (var cast in visionCasts)
        {
            cast.ForceRaycastUpdate();
            if (cast.GetCollider() != null)
            {
                var collider = cast.GetCollider();
                if (collider is Node2D entity )
                {
                    if(entity.Name == npcName || stats.isPlayer == SharedStats.getStats(entity.Name).isPlayer)
                    {
                        return false;
                    }
                    targetNPC = entity.Name;
                }
                return true;
            }
        }
        return false;
    }
    public void SetMovementSpeed() 
    {
        
    }
    public bool Attack()
    {
        
        if(IsTargetInsight() && stats.isPlayer != SharedStats.getStats(targetNPC).isPlayer)
        {
            
            LevelInfo.attackTarget(targetNPC, stats.damage, stats.accuracy);
            return true;
        }
        return false;
    }

    public void SetModifiersAndWieghting() 
    { 
        
    }
    public float GetRotation() 
    {
        return rotation;
    }
    public Vector2 GetNewLocation(Vector2 getLocation)
    {
        
        if (npcLocation.DistanceTo(navAgent.GetFinalLocation()) > 2f)
        {
            var newlocation = navAgent.GetNextLocation();
            var direction = GlobalPosition.DirectionTo(newlocation);
            


                
            var velocity = direction * stats.speed * 2;
            rotation = velocity.Angle();
            if (getLocation.DistanceTo(npcLocation) < 2f)
            {
                ForcePath();
            }
            if(GlobalPosition.DistanceTo(velocity) > fastestVelocity)
                fastestVelocity = GlobalPosition.DistanceTo(velocity);
            return velocity;
        }
        return npcLocation;
    }
    public void TargetLocation(Vector2 location)
    {
        navAgent.TargetLocation = location;
    }
    public void SetNPCLocation(Vector2 location) 
    {
        npcLocation = location;
        LevelInfo.AddPosition(npcName, location);
    }

    internal bool ReachedLocation()
    {
        return npcLocation.DistanceTo(navAgent.GetFinalLocation()) > 2f;
    }
}
