extends AnimatedSprite2D


func _physics_process(delta):
	play(Infoholder.lightcolor)
	$PointLight2D.color = Infoholder.lightcolorhex
	
