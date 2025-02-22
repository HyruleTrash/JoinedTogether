extends CharacterBody2D

# const UP = Vector2(0, -1)
# const GRAVITY = 23
# const MAXFALLSPEED = 400
# const SPEED = 2
# const JUMPFORCE = 420
# const inertia = 100

# var motion = Vector2()

# var jumpstate: String = "JumpBoy"
# var idlestate: String = "IdleBoy"
# var fallstate: String = "FallBoy"
# var walkstate: String = "WalkBoy"
# var isonfloor: bool
# var cojotietime = true

# signal nextlvl
# signal respawn

# func _ready():
# 	respawn2()

# func switchstates():
# 	if !Infoholder.stategirl:
# 		jumpstate = "JumpGirl"
# 		idlestate = "IdleGirl"
# 		fallstate = "FallGirl"
# 		walkstate = "WalkGirl"
# 	elif Infoholder.stategirl:
# 		jumpstate = "JumpBoy"
# 		idlestate = "IdleBoy"
# 		fallstate = "FallBoy"
# 		walkstate = "WalkBoy"

# func _physics_process(delta):
# 	Infoholder.playerposition = position
# 	motion.y += GRAVITY
# 	if motion.y > MAXFALLSPEED:
# 		motion.y = MAXFALLSPEED
	
# 	if Input.is_action_pressed("RIGHT"):
# 		position.x += SPEED
# 		$AnimatedSprite2D.play(walkstate)
# 		$AnimatedSprite2D.flip_h = false
# 	elif Input.is_action_pressed("LEFT"):
# 		position.x += -SPEED
# 		$AnimatedSprite2D.play(walkstate)
# 		$AnimatedSprite2D.flip_h = true
# 	else:
# 		$AnimatedSprite2D.play(idlestate)
# 		motion.x = 0
	
# 	if Input.is_action_just_pressed("R"):
# 		$DeathSound.stream = preload("res://Soundeffects/Door.wav")
# 		respawn()
# 	if Input.is_action_just_pressed("DOWN"):
# 		$Change_sound.play()
	
# 	if is_on_floor():
# 		cojotietime = true
# 		isonfloor == true
# 		if Input.is_action_just_pressed("UP"):
# 			motion.y = -JUMPFORCE
# 			$JumpSound.play()
# 		if Input.is_action_pressed("LEFT") or Input.is_action_pressed("RIGHT"):
# 			if !$Stepsound2D.playing:
# 				$Stepsound2D.play()
# 	if !is_on_floor():
# 		coiotitime()
# 		if cojotietime:
# 			if Input.is_action_just_pressed("UP"):
# 				motion.y = -JUMPFORCE
# 				$JumpSound.play()
# 				cojotietime = false
# 		isonfloor == false
# 		if motion.y > 0:
# 			$AnimatedSprite2D.play(fallstate)
# 		else:
# 			$AnimatedSprite2D.play(jumpstate)
	
# 	set_velocity(motion)
# 	set_up_direction(UP)
# 	set_floor_stop_on_slope_enabled(true)
# 	set_max_slides(4)
# 	set_floor_max_angle(PI / 4)
# 	# TODOConverter3To4 infinite_inertia were removed in Godot 4 - previous value `false`
# 	move_and_slide()
# 	motion.y = velocity.y
# 	for index in get_slide_collision_count():
# 		var collision = get_slide_collision(index)
# 		if collision.collider.is_in_group("box"):
# 			var box = collision.collider
# 			box.apply_central_impulse(-collision.normal * inertia)
# 			if global_position.y == box.global_position.y:
# 				if global_position.x > box.global_position.x:
# 					box.rotation_degrees -= 1
# 				if global_position.x < box.global_position.x:
# 					box.rotation_degrees += 1
# 		if is_on_floor() && collision.normal.y < 1.0 && motion.x != 0.0:
# 			motion.y = collision.normal.y


# func nextlevel():
# 	$DeathSound.stream = preload("res://Soundeffects/Door.wav")
# 	$DeathSound.play()
# 	if Infoholder.lvl == 3:
# 		Infoholder.lvl = 3.5
# 	elif Infoholder.lvl == 3.5:
# 		Infoholder.lvl = 4
# 	else:
# 		Infoholder.lvl += 1
# 	Infoholder.buttons = 0
# 	Infoholder.buttonspressed = 0
# 	emit_signal("nextlvl")
# 	respawn2()

# func respawn():
# 	get_node("../").respawn()
# 	$DeathSound.play()
# 	emit_signal("respawn")
# 	Infoholder.buttonspressed = 0
# 	if Infoholder.lvl == 1:
# 		position = Vector2(0, 328)
# 		Infoholder.buttons = 0
# 	elif Infoholder.lvl == 2:
# 		position = Vector2(896, 360)
# 		Infoholder.buttons = 0
# 	elif Infoholder.lvl == 3:
# 		position = Vector2(1792, 360)
# 		Infoholder.buttons = 1
# 	elif Infoholder.lvl == 3.5:
# 		position = Vector2(1792, 585.5)
# 		Infoholder.buttons = 0
# 	elif Infoholder.lvl == 4:
# 		position = Vector2(2688, 328)
# 		Infoholder.buttons = 1
# 	elif Infoholder.lvl == 5:
# 		position = Vector2(3584, 360)
# 		Infoholder.buttons = 1
# 	elif Infoholder.lvl == 6:
# 		position = Vector2(4496, 240)
# 		Infoholder.buttons = 0
# 	elif Infoholder.lvl == 7:
# 		position = Vector2(5360, 208)
# 		Infoholder.buttons = 0

# func respawn2():
# 	Infoholder.buttonspressed = int("0")
# 	if Infoholder.lvl == 1:
# 		position = Vector2(0, 328)
# 		Infoholder.buttons = 0
# 	elif Infoholder.lvl == 2:
# 		position = Vector2(896, 360)
# 		Infoholder.buttons = 0
# 	elif Infoholder.lvl == 3:
# 		position = Vector2(1792, 360)
# 		Infoholder.buttons = 1
# 		Infoholder.buttonspressed = 0
# 	elif Infoholder.lvl == 3.5:
# 		position = Vector2(1792, 585.5)
# 		Infoholder.buttons = 0
# 	elif Infoholder.lvl == 4:
# 		position = Vector2(2688, 328)
# 		Infoholder.buttons = 1
# 	elif Infoholder.lvl == 5:
# 		position = Vector2(3584, 360)
# 		Infoholder.buttons = 1
# 	elif Infoholder.lvl == 6:
# 		position = Vector2(4496, 240)
# 		Infoholder.buttons = 0
# 	elif Infoholder.lvl == 7:
# 		position = Vector2(5360, 208)
# 		Infoholder.buttons = 0
# 	elif Infoholder.lvl == 8:
# 		get_node("../").respawn()

# func _on_AnimatedSprite_animation_finished():
# 	if isonfloor:
# 		if Input.is_action_pressed("LEFT") or Input.is_action_pressed("RIGHT"):
# 			if !$Stepsound2D.playing:
# 				if $AnimatedSprite2D.frame == 1:
# 					$Stepsound2D.play()

# func coiotitime():
# 	var t = Timer.new()
# 	t.set_wait_time(0.1)
# 	t.set_one_shot(true)
# 	self.add_child(t)
# 	t.start()
# 	await t.timeout
# 	cojotietime = false
# 	t.queue_free()
