using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class MainMenu : Node
{
    [Export]
    public PackedScene LevelOne;
    public Level LevelOneInstance;
    [Export]
    public bool IsActive = true;
    [Export]
    public Control MainMenuUI;
    [Export]
    public Array<MenuItem> MainMenuItems;
    private int _currentSelectedIndex = 0;

    // Temp Variables
    [Export]
    public bool TempIsOnEndScreen = false;
    [Export]
    public Control TempEndOfDemoUI;

    // Sound Variables
    [Export]
    private AudioStreamPlayer _changeSelectionSound;
    [Export]
    private AudioStreamPlayer _acceptSelectionSound;

    public override void _Ready()
    {
        base._Ready();

        GetNode<GlobalData>("/root/GlobalData").MainMenu = this;
        _UpdateChangeSelector(_currentSelectedIndex);
    }

    public override void _Process(double delta)
    {
        _MainMenuProcess();
        _TempEndOfDemoProcess();

        if (Input.IsActionJustPressed("ESC"))
            GetTree().Quit();
    }

    private void _MainMenuProcess()
    {
        if (this.IsActive)
        {
            this.MainMenuUI.Visible = true;
            if (Input.IsActionJustPressed("ESC"))
                GetTree().Quit();
            if (Input.IsActionJustPressed("DOWN"))
            {
                _changeSelectionSound.Play();
                _UpdateChangeSelector(_currentSelectedIndex + 1);
            }
            if (Input.IsActionJustPressed("ui_up"))
            {
                _changeSelectionSound.Play();
                _UpdateChangeSelector(_currentSelectedIndex - 1);
            }
            if (Input.IsActionJustPressed("ui_accept"))
            {
                _acceptSelectionSound.Play();
                this.MainMenuItems[_currentSelectedIndex].TriggerItem();
            }
        }
        else
        {
            this.MainMenuUI.Visible = false;
        }
    }

    private void _UpdateChangeSelector(int index)
    {
        if (this.MainMenuItems.Count == 0)
            return;
        // Looping back logic, through the selectors
        if (index > this.MainMenuItems.Count - 1)
            index = 0;
        if (index < 0)
            index = this.MainMenuItems.Count - 1;

        // Empty the current selector
        this.MainMenuItems[_currentSelectedIndex].SetSelector("");

        // Set the new selector
        _currentSelectedIndex = index;
        this.MainMenuItems[_currentSelectedIndex].SetSelector("-");
    }

    private void _TempEndOfDemoProcess()
    {
        if (TempIsOnEndScreen)
        {
            if (this.LevelOneInstance != null)
            {
                this.LevelOneInstance.QueueFree();
                this.LevelOneInstance = null;
            }
            TempEndOfDemoUI.Visible = true;

            if (Input.IsActionJustPressed("ui_accept"))
            {
                _acceptSelectionSound.Play();
                _currentSelectedIndex = 0;
                _UpdateChangeSelector(_currentSelectedIndex);
                TempIsOnEndScreen = false;
                this.IsActive = true;
            }
        }
        else
        {
            TempEndOfDemoUI.Visible = false;
        }
    }
}