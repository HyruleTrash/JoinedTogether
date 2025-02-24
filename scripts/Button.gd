extends Area2D

#signal buttonpress
#@export var buttonlvl : int = 0
#
#func _ready():
	#$StaticBody2D/AnimatedSprite2D.play("default")
#
#
#func _on_Area2D_body_entered(body):
	#var bodys
	#bodys = get_overlapping_bodies()
	#bodys = bodys.count(0)
	#if body.is_in_group("player"):
		#bodys -= 1
	#if body.is_in_group("box"):
		#$StaticBody2D/AnimatedSprite2D.play("pressed")
		#if buttonlvl == Infoholder.lvl:
			#$AudioStreamPlayer2D.stream = preload("res://Soundeffects/Buttons.wav")
			#$AudioStreamPlayer2D.play()
		#Infoholder.buttonspressed = bodys + 1
		#emit_signal("buttonpress")
#
#
#func _on_Area2D_body_exited(body):
	#var bodys
	#bodys = get_overlapping_bodies()
	#bodys = bodys.count(0)
	#if body.is_in_group("box"):
		#$AudioStreamPlayer2D.stream = preload("res://Soundeffects/Buttons2.wav")
		#if buttonlvl == Infoholder.lvl:
			#$AudioStreamPlayer2D.play()
			#$StaticBody2D/AnimatedSprite2D.play("default")
		#Infoholder.buttonspressed = bodys
