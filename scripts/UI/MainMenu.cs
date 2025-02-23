using Godot;
using System;
using System.Collections.Generic;

public partial class MainMenu : Node
{
    public Resource LevelOne;
    [Export]
    public bool IsActive = true;
    [Export]
    public bool TempIsOnEndScreen = false;
    [Export]
    public Node2D MainMenuUI;
    [Export]
    public Node2D TempEndOfDemoUI;
    [Export]
    private AudioStreamPlayer _changeSelectionSound;
    [Export]
    private AudioStreamPlayer _acceptSelectionSound;

    public override void _Ready()
    {
        base._Ready();

        GetNode<GlobalData>("/root/GlobalData").MainMenu = this;
        LevelOne = GD.Load<PackedScene>("res://Scenes/Levels/Level-1.tscn");
    }

    public override void _Process(double delta)
    {
        if (IsActive)
        {
            MainMenuUI.Visible = true;
            if (Input.IsActionJustPressed("ESC"))
                GetTree().Quit();
            if (Input.IsActionJustPressed("DOWN"))
            {
                _changeSelectionSound.Play();
            }
        }
        else
        {
            MainMenuUI.Visible = false;
        }
    }
}