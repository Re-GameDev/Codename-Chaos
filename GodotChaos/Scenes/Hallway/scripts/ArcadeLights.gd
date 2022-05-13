extends Spatial

var lights
var dark_black_material = preload("res://Scenes/Hallway/materials/dark_black.tres")
var bright_white_material = preload("res://Scenes/Hallway/materials/dark_black.tres")

var sc
func _ready():
	sc = get_tree().get_root().get_node("Hallway")
	lights = get_children()

func openArcadeDoors():
	sc.openArcadeDoors()

func hallwayToggle(isOn):
	var root = get_tree().get_root()
	root.get_node("Hallway/hallway/arch1/OmniLight").set_visible(isOn)
	root.get_node("Hallway/hallway/arch2/OmniLight2").set_visible(isOn)
	root.get_node("Hallway/hallway/arch3/OmniLight3").set_visible(isOn)
	root.get_node("Hallway/hallway/arch4/OmniLight4").set_visible(isOn)
	root.get_node("Hallway/hallway/arch5/OmniLight5").set_visible(isOn)
	root.get_node("Hallway/hallway/arch6/BlueLight").set_visible(isOn)
	root.get_node("Hallway/hallway/arch6/RedLight").set_visible(isOn)

func allLightsOff():
	for light in get_children():
		light.toggle(false)

func lightsOn(lst):
	for index in lst:
		lights[index].toggle(true)

func lightsOff(lst):
	for index in lst:
		lights[index].toggle(false)

func _on_Area0_body_entered(body):
	if body.name == "Player":
		allLightsOff()
		lightsOn([0, 1])

func _on_Area0_body_exited(_body):
	pass
		
func _on_Area1_body_entered(body):
	if body.name == "Player":
		allLightsOff()
		lightsOn([0, 1, 2])
		
func _on_Area1_body_exited(_body):
	pass

func _on_Area2_body_entered(body):
	if body.name == "Player":
		allLightsOff()
		hallwayToggle(true)
		lightsOn([1, 2, 3])

func _on_Area2_body_exited(_body):
	pass

func _on_Area3_body_entered(body):
	if body.name == "Player":
		allLightsOff()
		hallwayToggle(false)
		lightsOn([2, 3, 4])

func _on_Area3_body_exited(_body):
	pass

func _on_Area4_body_entered(body):
	if body.name == "Player":
		allLightsOff()
		lightsOn([3, 4, 5])

func _on_Area4_body_exited(_body):
	pass

func _on_Area5_body_entered(body):
	if body.name == "Player":
		allLightsOff()
		lightsOn([4, 5, 6])

func _on_Area5_body_exited(_body):
	pass

func _on_Area6_body_entered(body):
	if body.name == "Player":
		allLightsOff()
		lightsOn([5, 6, 7])

func _on_Area6_body_exited(_body):
	pass # Replace with function body.

func _on_Area7_body_entered(body):
	if body.name == "Player":
		allLightsOff()
		lightsOn([6, 7])

func _on_Area7_body_exited(_body):
	pass

func _on_Area8_body_entered(_body):
	pass
func _on_Area8_body_exited(_body):
	pass # Replace with function body.
