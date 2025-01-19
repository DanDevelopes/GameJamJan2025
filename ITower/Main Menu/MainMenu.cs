using Godot;
using System;

public class MainMenu : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Button buttonStart;
    private String levelSelected;
    private Button buttonSettings;
    private Button buttonSurvival;
    private Button buttonExit;
    private delegate void ExitButtonDelegate();
    private event ExitButtonDelegate OnExitButtonRelease;
    private delegate void StartButtonDelegate();
    private event StartButtonDelegate OnStartButtonRelease;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        buttonStart = GetNode<Button>("Start_Botton");
        buttonSettings = GetNode<Button>("Setting_Button");
        buttonSurvival = GetNode<Button>("Survival_Button");
        buttonExit = GetNode<Button>("Exit_Button");
        
        buttonExit.Connect("pressed", this, nameof(OnExitButtonReleaseHandler));
        buttonStart.Connect("pressed", this, nameof(OnStartButtonReleaseHandler));
        OnExitButtonRelease += ExitGame;
        OnStartButtonRelease += LevelOneStart;

    }
    private void OnExitButtonReleaseHandler()
    {
        OnExitButtonRelease.Invoke();
    }
    private void OnStartButtonReleaseHandler()
    {
        OnStartButtonRelease.Invoke();
    }
    private void ExitGame()
    {
        GetTree().Quit();
    }
    private void LevelOneStart()
    {
        
        GetTree().ChangeScene("res://Levels/Level1-Tutorial.tscn");
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }
}
