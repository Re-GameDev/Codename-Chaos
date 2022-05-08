extends Spatial

var brightLight
var darkLight

func _ready():
	var children = get_children()
	brightLight = children[0]
	darkLight = children[1]
	
func toggle(isOn):
	brightLight.set_visible(isOn)
	darkLight.set_visible(not isOn)
