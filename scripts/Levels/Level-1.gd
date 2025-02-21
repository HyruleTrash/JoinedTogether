extends Node2D

func _ready():
	Infoholder.lvl = Infoholder.oldlvl
	$Camera2D.position.x += ( 896 * ( Infoholder.oldlvl - 1 ))
	Infoholder.buttons = 0
	switchfix()


func switchstates():
	Infoholder.get_node("AudioStreamPlayer2").stop()
	if Infoholder.stategirl:
		$"Level-1-boy".visible = true
		$"level-1-girl".visible = false
		Infoholder.stategirl = false
	elif !Infoholder.stategirl:
		$"Level-1-boy".visible = false
		$"level-1-girl".visible = true
		Infoholder.stategirl = true

func switchfix():
	Infoholder.get_node("AudioStreamPlayer2").stop()
	if Infoholder.stategirl:
		$"Level-1-boy".visible = false
		$"level-1-girl".visible = true
	elif !Infoholder.stategirl:
		$"Level-1-boy".visible = true
		$"level-1-girl".visible = false

func _physics_process(delta):
	if Input.is_action_just_pressed("DOWN"):
		$Camera2D/CanvasLayer/ColorRect.visible = true
		Glitch()
		switchstates()
		$Player.switchstates()
	if Input.is_action_just_pressed("ESC"):
		Infoholder.mainmenu = true
		Infoholder.oldlvl = Infoholder.lvl
		Infoholder.get_node("Song1").play(7.855)
		get_parent().quit("ESC")
		queue_free()

func Glitch():
	var t = Timer.new()
	t.set_wait_time(0.5)
	t.set_one_shot(true)
	self.add_child(t)
	t.start()
	await t.timeout
	$Camera2D/CanvasLayer/ColorRect.visible = false
	t.queue_free()

func _on_Player_nextlvl():
	if Infoholder.lvl == 3.5:
		$Camera2D.position.y += 512
	else:
		$Camera2D.position.x += 896
		$Camera2D.position.y = 208
	$"Level-1-boy".visible = false
	$"level-1-girl".visible = true
	Infoholder.stategirl = true
	$Player.switchstates()

func respawn():
	if Infoholder.lvl == 8:
		get_parent().quit("lastlvl")
		Infoholder.get_node("AudioStreamPlayer2").stream = null
		Infoholder.get_node("AudioStreamPlayer2").stop()
		queue_free()
	else:
		if Infoholder.lvl > 2:
			get_node("boxes").get_child(Infoholder.lvl - 3).respawn()

func _on_Area2D_buttonpress():
	if Infoholder.buttonspressed == Infoholder.buttons:
		pass
