extends Spatial

func _ready():
	$ArcadeLights.allLightsOff()

func _on_Area8_body_entered(body):
	if body.name == "Player":
		$ArcadeDoors.toggleDoors(true)
