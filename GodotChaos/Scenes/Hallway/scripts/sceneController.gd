extends Spatial

var arcadeLights = null;
var arcadeDoors = null;

func _ready():
	var arcade = find_node("Arcade")
	arcadeLights = arcade.find_node("Lights")
	arcadeDoors = arcade.find_node("Doors")
	assert(arcadeLights != null)
	assert(arcadeDoors != null)
	arcadeLights.allLightsOff()

func _on_Area8_body_entered(body):
	if body.name == "Player":
		arcadeDoors.toggleDoors(true)
