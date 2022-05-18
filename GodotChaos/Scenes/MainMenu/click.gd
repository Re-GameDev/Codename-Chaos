extends Button

export (String) var pathToScene

func _on_Button_pressed():
	#warning-ignore:RETURN_VALUE_DISCARDED
	get_tree().change_scene(pathToScene)
