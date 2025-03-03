using Godot;
using System;
using System.Collections.Generic;

public partial class GlobalSoundManager : Node2D
{
	public GlobalData GlobalData;

	[Export]
	public AudioStreamPlayer MusicPlayer;

	public Dictionary<string, AudioStream> Raindrops = new Dictionary<string, AudioStream>();
	[Export]
	public AudioStreamPlayer RaindropsPlayer;
	public float SongTime;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GlobalData = GetNode<GlobalData>("/root/GlobalData");

		Raindrops.Add("Raindrops0_1", GD.Load<AudioStream>("res://Art/Soundeffects/Music/Raindrops0_1.wav"));
		Raindrops.Add("Raindrops0_2", GD.Load<AudioStream>("res://Art/Soundeffects/Music/Raindrops0_2.wav"));
		Raindrops.Add("Raindrops0_3", GD.Load<AudioStream>("res://Art/Soundeffects/Music/Raindrops0_3.wav"));
		Raindrops.Add("Raindrops0_4", GD.Load<AudioStream>("res://Art/Soundeffects/Music/Raindrops0_4.wav"));
		Raindrops.Add("Raindrops0_5", GD.Load<AudioStream>("res://Art/Soundeffects/Music/Raindrops0_5.wav"));
		Raindrops.Add("Raindrops0_6", GD.Load<AudioStream>("res://Art/Soundeffects/Music/Raindrops0_6.wav"));
		Raindrops.Add("Raindrops0_7", Raindrops["Raindrops0_3"]);
		Raindrops.Add("Raindrops0_8", Raindrops["Raindrops0_2"]);
		Raindrops.Add("Raindrops0_9", null);

		Raindrops.Add("Pause", GD.Load<AudioStream>("res://Art/Soundeffects/Music/Pause.wav"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// main music
		if (MusicPlayer.Playing == false && GlobalData.MainMenu.IsActive == false)
		{
			if (GlobalData.Player.IsInGirlState)
			{
				MusicPlayer.Play();
			}
			else
			{
				MusicPlayer.Stop();
			}
		}

		// ambient raindrops, an
		if (!RaindropsPlayer.Playing)
		{
			if (GlobalData.MainMenu.IsActive)
			{
				RaindropsPlayer.Stream = Raindrops["Pause"];
				RaindropsPlayer.Play();
			}
			else
			{
				RaindropsPlayer.Stream = Raindrops["Raindrops0_" + GlobalData.Level];
				RaindropsPlayer.Play(SongTime);
				SongTime = 0;
			}
		}
	}
}
