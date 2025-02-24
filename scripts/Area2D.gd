extends Area2D

#@export var kind : String
#
#
#func _on_Area2D_body_entered(body):
	#if body.is_in_group("player"):
		#if kind == "deathplane":
			#body.get_node("DeathSound").stream = preload("res://Soundeffects/DeathSound.wav")
			#body.respawn()
		#if kind == "goal":
			#if Infoholder.lvl == 6:
				#body.get_node("DeathSound").stream = null
			#else:
				#body.get_node("DeathSound").stream = preload("res://Soundeffects/Door.wav")
				#Infoholder.songtime = Infoholder.get_node("Song1").get_playback_position()
				#Infoholder.get_node("Song1").play(7.855)
			#if Infoholder.buttonspressed == Infoholder.buttons:
				#get_node("../").play("open")
				#body.nextlevel()
	#if body.is_in_group("box"):
		#if Infoholder.lvl > 2:
			#if kind == "deathplane":
				#body.get_node("AudioStreamPlayer2D").stream = preload("res://Soundeffects/DeathSound.wav")
				#body.ParticlesBox = preload("res://scenes/BoxParticles2D.tscn")
				#body.respawn()
#
#func _physics_process(delta):
	#if kind == "goal":
		#if Infoholder.buttonspressed == Infoholder.buttons:
			#get_node("../").play("open")
			#Infoholder.lightcolor = "green"
			#Infoholder.lightcolorhex = Color(50,255,0)
		#else:
			#Infoholder.lightcolor = "red"
			#Infoholder.lightcolorhex = Color(255,0,0)
			#get_node("../").play("closed")
