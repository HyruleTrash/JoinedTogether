using Godot;
using System;

/// <summary>
/// Triggers the 2d game to start
/// </summary>
public partial class MenuPlayItem : MenuItem, IMenuItemTriggerable
{
	[Export]
	private MainMenu _MainMenu;

	public void TriggerItem()
	{
		GlobalData globalData = GetNode<GlobalData>("/root/GlobalData");
		globalData.Level = globalData.OldLevel;

		GetNode<GlobalSoundManager>("/root/GlobalSoundManager").MusicPlayer.Play(3.994f);

		this._MainMenu.LevelOneInstance = (Level)this._MainMenu.LevelOne.Instantiate();
		this._MainMenu.AddChild(this._MainMenu.LevelOneInstance);
		// Infoholder.buttonspressed = 0
		// lvl1.instantiate().get_node("Player").respawn2()

		this._MainMenu.IsActive = false;
	}
}
