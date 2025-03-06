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
                MainMenuItems[_currentSelectedIndex].TriggerItem();
            }
        }
        else
        {
            MainMenuUI.Visible = false;
        }
    }

    private void _UpdateChangeSelector(int index)
    {
        if (MainMenuItems.Count == 0)
            return;
        // Looping back logic, through the selectors
        if (index > MainMenuItems.Count - 1)
            index = 0;
        if (index < 0)
            index = MainMenuItems.Count - 1;

        // Empty the current selector
        MainMenuItems[_currentSelectedIndex].SetSelector("");

        // Set the new selector
        _currentSelectedIndex = index;
        MainMenuItems[_currentSelectedIndex].SetSelector("-");
    }

    private void _TempEndOfDemoProcess()
    {
        if (TempIsOnEndScreen)
        {
            TempEndOfDemoUI.Visible = true;
            if (Input.IsActionJustPressed("ESC"))
                GetTree().Quit();

            LevelOneInstance.GetNode<AudioStreamPlayer2D>("Player/DeathSound").Finished += () =>
            {
                if (Input.IsActionJustPressed("ui_accept"))
                {
                    _acceptSelectionSound.Play();
                    _currentSelectedIndex = 0;
                    _UpdateChangeSelector(_currentSelectedIndex);
                    TempIsOnEndScreen = false;
                    IsActive = true;
                }
            };
        }
        else
        {
            TempEndOfDemoUI.Visible = false;
        }
    }

    // IMPLEMENT LATER
    // func quit(why):
    // if why == "lastlvl":
    // 	endscreen = true
    // elif why == "ESC":
    // 	mainmenu = true
    // 	Infoholder.mainmenu = true
    // else:
    // 	mainmenu = true
    // 	Infoholder.mainmenu = true

    // if Infoholder.oldlvl > 6:
    // 	Infoholder.oldlvl = 1
    // else:
    // 	Infoholder.oldlvl += 1
}