extends Node

var coins = 0

func _ready():
	pass

func coinCollected():
	coins += 1
	if coins == 3:
		get_tree().change_scene("res://Scenes/Hallway/hallway.tscn")
