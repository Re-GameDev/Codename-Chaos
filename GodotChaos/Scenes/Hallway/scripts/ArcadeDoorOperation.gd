extends Spatial

var leftDoor
var rightDoor

func _ready():
	leftDoor = $leftDoor
	rightDoor = $rightDoor

func toggleDoors(isOpen):
	print("actual: openArcadeDoors")
	var tween = get_node("Tween")
	var leftPosition = leftDoor.get_translation()
	var rightPosition = rightDoor.get_translation()
	var offset
	if isOpen:
		offset = Vector3(0, 0, 5.5)
	else:
		offset = Vector3(0, 0, -5.5)
	tween.interpolate_property(leftDoor, "translation",
		leftPosition, leftPosition - offset, 1)
	tween.interpolate_property(rightDoor, "translation",
		rightPosition, rightPosition + offset, 1)
	tween.start()
