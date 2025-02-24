using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class MainMenu : Node
{
    public Resource LevelOne;
    [Export]
    public bool IsActive = true;
    [Export]
    public Control MainMenuUI;
    [Export]
    public Array<Array<NodePath>> MainMenuSelectors;
    private int _currentSelectorIndex = 0;

    // Temp Variables
    [Export]
    public bool TempIsOnEndScreen = false;
    [Export]
    public Node2D TempEndOfDemoUI;

    // Sound Variables
    [Export]
    private AudioStreamPlayer _changeSelectionSound;
    [Export]
    private AudioStreamPlayer _acceptSelectionSound;

    public override void _Ready()
    {
        base._Ready();

        GetNode<GlobalData>("/root/GlobalData").MainMenu = this;
        LevelOne = GD.Load<PackedScene>("res://Scenes/Levels/Level-1.tscn");
        _UpdateChangeSelector(_currentSelectorIndex);
    }

    public override void _Process(double delta)
    {
        _MainMenuProcess();
    }

    private void _MainMenuProcess()
    {
        if (IsActive)
        {
            MainMenuUI.Visible = true;
            if (Input.IsActionJustPressed("ESC"))
                GetTree().Quit();
            if (Input.IsActionJustPressed("DOWN"))
            {
                _changeSelectionSound.Play();
                _UpdateChangeSelector(_currentSelectorIndex + 1);
            }
            if (Input.IsActionJustPressed("ui_up"))
            {
                _changeSelectionSound.Play();
                _UpdateChangeSelector(_currentSelectorIndex - 1);
            }
            if (Input.IsActionJustPressed("ui_accept"))
            {
                _acceptSelectionSound.Play();
            }
        }
        else
        {
            MainMenuUI.Visible = false;
        }
    }

    private void _UpdateChangeSelector(int index){
        if (MainMenuSelectors.Count == 0)
            return;
        // Looping back logic, through the selectors
        if (index > MainMenuSelectors.Count - 1)
            index = 0;
        if (index < 0)
            index = MainMenuSelectors.Count - 1;
        
        // Empty the current selector
        _SetSelector(_currentSelectorIndex, "");

        // Set the new selector
        _currentSelectorIndex = index;
        _SetSelector(_currentSelectorIndex, "-");
    }

    private void _SetSelector(int index, string newText){
        for (int i = 0; i < MainMenuSelectors[index].Count; i++)
        {
            Label label = GetNode<Label>(MainMenuSelectors[index][i]);
            if (label != null)
                label.Text = newText;
        }
    }
}