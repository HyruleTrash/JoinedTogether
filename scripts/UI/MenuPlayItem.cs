using Godot;
using System;

public partial class MenuPlayItem : MenuItem
{
	[Export]
	private MainMenu MainMenu;

	public override void TriggerItem()
	{
		GlobalData globalData = GetNode<GlobalData>("/root/GlobalData");
		globalData.Level = globalData.OldLevel;

		GetNode<GlobalSoundManager>("/root/GlobalSoundManager").MusicPlayer.Play(3.994f);

		MainMenu.LevelOneInstance = MainMenu.LevelOne.Instantiate();
		MainMenu.AddChild(MainMenu.LevelOneInstance);
		// Infoholder.buttonspressed = 0
		// lvl1.instantiate().get_node("Player").respawn2()

		MainMenu.IsActive = false;
	}
}
