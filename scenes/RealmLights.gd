extends PointLight2D


func _physics_process(delta):
	if Infoholder.stategirl:
		color = Color(0, 0, 1)
	else:
		color = Color(0.34902, 0.019608, 0.396078)
