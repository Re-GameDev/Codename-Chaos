extends Button

func _on_Button_pressed():
	#warning-ignore:RETURN_VALUE_DISCARDED
	get_tree().change_scene("res://Scenes/Grocery/grocery.tscn")
