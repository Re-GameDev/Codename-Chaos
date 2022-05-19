extends Node2D

var gc

func _ready():
	gc = get_tree().get_root().get_node("maze")

func _on_Area2D_body_entered(body):
	print(body)
	if body.name == "player":
		gc.coinCollected()
		visible = false
		queue_free()
