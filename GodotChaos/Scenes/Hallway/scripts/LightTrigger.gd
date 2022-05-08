extends MeshInstance

export (NodePath) var first
export (NodePath) var second
export (NodePath) var third

var firstLight
var secondLight
var thirdLight

func _ready():
	if first:
		firstLight = get_node(first)
	if second:
		secondLight = get_node(second)
	if third:
		thirdLight = get_node(third)

func lightsOn():
	if firstLight:
		firstLight.set_visible(true)
	if secondLight:
		secondLight.set_visible(true)
	if thirdLight:
		thirdLight.set_visible(true)
