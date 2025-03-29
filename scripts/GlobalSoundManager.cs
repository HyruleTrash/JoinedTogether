using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// A global system that handles all music players and their logic. such as game music or ambience tracks.
/// </summary>
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
		this.GlobalData = GetNode<GlobalData>("/root/GlobalData");

		this.Raindrops.Add("Raindrops0_1", GD.Load<AudioStream>("res://Art/Soundeffects/Music/Raindrops0_1.wav"));
		this.Raindrops.Add("Raindrops0_2", GD.Load<AudioStream>("res://Art/Soundeffects/Music/Raindrops0_2.wav"));
		this.Raindrops.Add("Raindrops0_3", GD.Load<AudioStream>("res://Art/Soundeffects/Music/Raindrops0_3.wav"));
		this.Raindrops.Add("Raindrops0_4", GD.Load<AudioStream>("res://Art/Soundeffects/Music/Raindrops0_4.wav"));
		this.Raindrops.Add("Raindrops0_5", GD.Load<AudioStream>("res://Art/Soundeffects/Music/Raindrops0_5.wav"));
		this.Raindrops.Add("Raindrops0_6", GD.Load<AudioStream>("res://Art/Soundeffects/Music/Raindrops0_6.wav"));
		this.Raindrops.Add("Raindrops0_7", this.Raindrops["Raindrops0_3"]);
		this.Raindrops.Add("Raindrops0_8", this.Raindrops["Raindrops0_2"]);
		this.Raindrops.Add("Raindrops0_9", null);

		this.Raindrops.Add("Pause", GD.Load<AudioStream>("res://Art/Soundeffects/Music/Pause.wav"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// main music
		if (this.MusicPlayer.Playing == false && this.GlobalData.MainMenu.IsActive == false)
		{
			if (this.GlobalData.Player.IsInGirlState)
			{
				this.MusicPlayer.Play();
			}
			else
			{
				this.MusicPlayer.Stop();
			}
		}

		// ambient raindrops, an
		if (!this.RaindropsPlayer.Playing)
		{
			if (this.GlobalData.MainMenu.IsActive)
			{
				this.RaindropsPlayer.Stream = this.Raindrops["Pause"];
				this.RaindropsPlayer.Play();
			}
			else
			{
				this.RaindropsPlayer.Stream = this.Raindrops["Raindrops0_" + this.GlobalData.Level];
				this.RaindropsPlayer.Play(this.SongTime);
				this.SongTime = 0;
			}
		}
	}
}
