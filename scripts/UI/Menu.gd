extends Node

var selection: int = 0

var lvl1 = preload("res://scenes/Levels/Level-1.tscn")

var selector1 = ""
var selector2 = ""
var selector3 = ""
var mainmenu: bool = true
var endscreen: bool = false


func _physics_process(delta):
	Infoholder.mainmenu = mainmenu
	if mainmenu:
		if Input.is_action_just_pressed("ESC"):
			get_tree().quit()
		$Node2D.visible = true
		if Input.is_action_just_pressed("DOWN"):
			$AudioStreamPlayer.play()
			if selection == 2:
				selection = 0
			else:
				selection += 1
		if Input.is_action_just_pressed("ui_up"):
			$AudioStreamPlayer.play()
			if selection < 1:
				selection = 2
			else:
				selection -= 1
		$Node2D/HBoxContainer2/Options2/VBoxContainer/CenterContainer/HBoxContainer/VboxSelectorLeft/Selector.text = selector1
		$Node2D/HBoxContainer2/Options2/VBoxContainer/CenterContainer/HBoxContainer/VboxSelectorLeft/Selector2.text = selector2
		$Node2D/HBoxContainer2/Options2/VBoxContainer/CenterContainer/HBoxContainer/VboxSelectorLeft/Selector3.text = selector3
		$Node2D/HBoxContainer2/Options2/VBoxContainer/CenterContainer/HBoxContainer/VboxSelectorRight/Selector.text = selector1
		$Node2D/HBoxContainer2/Options2/VBoxContainer/CenterContainer/HBoxContainer/VboxSelectorRight/Selector2.text = selector2
		$Node2D/HBoxContainer2/Options2/VBoxContainer/CenterContainer/HBoxContainer/VboxSelectorRight/Selector3.text = selector3
		if selection == 0:
			selector1 = "-"
			selector2 = ""
			selector3 = ""
		if selection == 1:
			selector1 = ""
			selector2 = "-"
			selector3 = ""
		if selection == 2:
			selector1 = ""
			selector2 = ""
			selector3 = "-"
		if selection > 2 or selection < 0:
			selector1 = ""
			selector2 = ""
			selector3 = ""
		if Input.is_action_just_pressed("ui_accept"):
			$AudioStreamPlayer2.play()
			if selection == 0:
				Infoholder.lvl = Infoholder.oldlvl
				Infoholder.get_node("Song1").play(3.994)
				add_child(lvl1.instantiate())
				Infoholder.buttonspressed = 0
				lvl1.instantiate().get_node("Player").respawn2()
				mainmenu = false
			elif selection == 1:
				$Node2D/Label.visible = true
				var t = Timer.new()
				t.set_wait_time(1)
				t.set_one_shot(true)
				self.add_child(t)
				t.start()
				await t.timeout
				t.queue_free()
				$Node2D/Label.visible = false
			elif selection == 2:
				get_tree().quit()
	else:
		$Node2D.visible = false
	if endscreen:
		if Input.is_action_just_pressed("ESC"):
			get_tree().quit()
		Infoholder.mainmenu = true
		$Node2D2.visible = true
		await lvl1.instantiate().get_node("Player/DeathSound").finished
		remove_child(lvl1.instantiate())
		if Input.is_action_pressed("ui_accept"):
			$AudioStreamPlayer2.play()
			selection = -1
			$Node2D2.visible = false
			endscreen = false
			mainmenu = true


func quit(why):
	if why == "lastlvl":
		endscreen = true
	elif why == "ESC":
		mainmenu = true
		Infoholder.mainmenu = true
	else:
		mainmenu = true
		Infoholder.mainmenu = true

	if Infoholder.oldlvl > 6:
		Infoholder.oldlvl = 1
	else:
		Infoholder.oldlvl += 1
