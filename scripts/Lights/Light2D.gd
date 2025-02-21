extends PointLight2D

var lightgreen : bool

func _physics_process(delta):
	if lightgreen:
		color = 0
	else:
		color = 1
