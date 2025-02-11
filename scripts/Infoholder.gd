extends Node2D

var stategirl = "girl"
var lvl : float
var oldlvl = 1
var buttons : int
var buttonspressed : int
var lightcolor
var lightcolorhex
var playerposition
var counter = false
var mainmenu = true
var currentmusic = preload("res://Soundeffects/Music/Raindrops0_1.wav")
var songtime : float


func _ready():
	get_window().size = DisplayServer.screen_get_size()



func _physics_process(delta):
	if !$AudioStreamPlayer2.playing:
		if !mainmenu:
			if !stategirl:
				$AudioStreamPlayer2.play()
			else:
				$AudioStreamPlayer2.stop()
	if !$Song1.playing:
		if mainmenu:
			$Song1.stream = preload("res://Soundeffects/Music/Pause.wav")
			$Song1.play()
		else:
			if lvl == 1:
				$Song1.stream = preload("res://Soundeffects/Music/Raindrops0_1.wav")
			if lvl == 2:
				$Song1.stream = preload("res://Soundeffects/Music/Raindrops0_2.wav")
			if lvl == 3:
				$Song1.stream = preload("res://Soundeffects/Music/Raindrops0_3.wav")
			if lvl == 3.5:
				$Song1.stream = preload("res://Soundeffects/Music/Raindrops0_35.wav")
			if lvl == 4:
				$Song1.stream = preload("res://Soundeffects/Music/Raindrops0_4.wav")
			if lvl == 5:
				$Song1.stream = preload("res://Soundeffects/Music/Raindrops0_5.wav")
			if lvl == 6:
				$Song1.stream = preload("res://Soundeffects/Music/Raindrops0_3.wav")
			if lvl == 7:
				$Song1.stream = preload("res://Soundeffects/Music/Raindrops0_2.wav")
			if lvl == 8:
				$Song1.stream = null
			$Song1.play(songtime)
			songtime = 0
	if $Song1.stream == preload("res://Soundeffects/Music/Pause.wav"):
		if !mainmenu:
			$Song1.play(3.994)
	else:
		if mainmenu:
			$Song1.play(7.855)
