extends Node2D

var SIZE = 64
export (NodePath) var wallMapNode
var wallMap

func _ready():
	wallMap = get_node(wallMapNode)

func isWall(pos):
	var cellCoords = (pos - Vector2.ONE * SIZE/2) / SIZE
	return wallMap.get_cellv(cellCoords) != -1

func move(direction):
	var nextPosition = get_position() + direction * SIZE
	if not isWall(nextPosition):
		set_position(nextPosition)

func _process(delta):
	if Input.is_action_just_pressed("forward"):
		move(Vector2.UP)
	if Input.is_action_just_pressed("right"):
		move(Vector2.RIGHT)
	if Input.is_action_just_pressed("back"):
		move(Vector2.DOWN)
	if Input.is_action_just_pressed("left"):
		move(Vector2.LEFT)
