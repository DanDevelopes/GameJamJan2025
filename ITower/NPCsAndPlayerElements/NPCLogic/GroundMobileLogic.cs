using Godot;
using ITower.NPCsAndPlayerElements.NPCLogic;
using ITower.NPCsAndPlayerElements.NPCLogic.StatsAndWieghting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
    private Vector2 NPCLocation;
    private List<RayCast2D> visionCasts;
    string targetNPC;
    private bool isDebug = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        visionCasts = new List<RayCast2D>();
        visionCasts.Add(GetNode<RayCast2D>("EntityVisonRight"));
        visionCasts.Add(GetNode<RayCast2D>("EntityVisonMidRight"));
        visionCasts.Add(GetNode<RayCast2D>("EntityVisonCenter"));
        visionCasts.Add(GetNode<RayCast2D>("EntityVisonMidLeft"));
        visionCasts.Add(GetNode<RayCast2D>("EntityVisonLeft"));
        navAgent = GetNode<NavigationAgent2D>("NavAgent");
    }
    public void ImportNpc(string npcName, bool isMobile, float visionWidth)
    {
        stats = new NPCStats();
        this.npcName = npcName;
        var npcType = Regex.Replace(npcName, @"[\d-]", string.Empty);
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
        SharedStats.addStats(npcName, stats);
        this.isMobile = isMobile;
        visionWidthInRadions = ((double)stats.feildOfView / 100) * tau;
        VisionSetup();
    }
    public void VisionSetup()
    {
        int i = 1;
        List<Vector2> debugVectors = new List<Vector2>();
        List<Line2D> visionLine = new List<Line2D>();
        foreach (var cast in visionCasts)
        {
            var sweepPoint = visionWidthInRadions / visionCasts.Count;
            var sweepAngle = sweepPoint * i;
            sweepAngle = sweepAngle - visionWidthInRadions / 2;
            cast.CastTo = new Vector2()
                {
                    x = rangeOfView * (float)Math.Cos(sweepAngle) + this.Position.x,
                    y = rangeOfView * (float)Math.Sin(sweepAngle) + this.Position.y
            };
            if (isDebug) 
            {
                debugVectors.Clear();
                debugVectors.Add(
                    cast.CastTo = new Vector2()
                    {
                        x = this.Position.x,
                        y = this.Position.y
                    }
                    );
                debugVectors.Add(
                    cast.CastTo = new Vector2()
                    {
                        x = rangeOfView * (float)Math.Cos(sweepAngle) + this.Position.x,
                        y = rangeOfView * (float)Math.Sin(sweepAngle) + this.Position.y
                    }
                    );
                Line2D line = new Line2D();
                line.Position = cast.Position;
                line.Points = debugVectors.ToArray();
                line.Width = 1;
                this.AddChild(line);
                visionLine.Add(line);
            }

            i++;
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
            if (cast.GetCollider() != null)
            {
                var collider = cast.GetCollider();
                if (collider is Node2D entity )
                {
                    if(entity.Name == npcName)
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
    public bool Shoot()
    {
        if(IsTargetInsight())
        {
            CombatElement.attackTarget(targetNPC, stats.damage, stats.accuracy);
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
    public Vector2 GetNewLocation()
    {
        var newlocation = navAgent.GetNextLocation();
        var direction = GlobalPosition.DirectionTo(newlocation);

        
        
        rotation = NPCLocation.y - newlocation.y / NPCLocation.x - newlocation.x; 
        var velocity = direction * stats.speed * 2;
        rotation = velocity.Angle();
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
}
