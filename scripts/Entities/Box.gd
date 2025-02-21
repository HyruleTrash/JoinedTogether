extends RigidBody2D

var spawnposition
@export var boxlvl : float
var ParticlesBox = preload("res://scenes/BoxParticles2D.tscn")

func _ready():
	spawnposition = position

func respawn():
	if boxlvl == Infoholder.lvl:
		CheckSpawn()
		if boxlvl == Infoholder.lvl:
			$AudioStreamPlayer2D.play()
		linear_velocity = Vector2(0,0)
		angular_velocity = 0
		applied_force = Vector2(0,0)
		rotation_degrees = 0
		position = spawnposition

func CheckSpawn():
	if boxlvl == Infoholder.lvl:
		if ParticlesBox != null:
			var e = ParticlesBox.instantiate()
			if boxlvl == Infoholder.lvl:
				get_node("../../").add_child(e)
				e.global_position = global_position
				e.emitting = true
			var t = Timer.new()
			t.set_wait_time(1.3)
			t.set_one_shot(true)
			self.add_child(t)
			t.start()
			await t.timeout
			e.queue_free()
			get_node("../../../").remove_child(e)
			t.queue_free()

func _on_Player_nextlvl():
	$AudioStreamPlayer2D.stream = null
	ParticlesBox = null
	respawn()

func _on_Player_respawn():
	$AudioStreamPlayer2D.stream = preload("res://Soundeffects/DeathSound.wav")
	ParticlesBox = preload("res://scenes/BoxParticles2D.tscn")
	respawn()
