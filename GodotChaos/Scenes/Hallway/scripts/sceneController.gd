extends Spatial

var arcadeLights = null;

func _ready():
	arcadeLights = find_node("Arcade").find_node("Lights");
	assert(arcadeLights != null);
	arcadeLights.allLightsOff()

func _on_Area8_body_entered(body):
	if body.name == "Player":
		arcadeLights.toggleDoors(true)
