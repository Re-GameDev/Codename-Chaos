extends KinematicBody

onready var camera = $Pivot/Camera

var gravity = -30
var max_speed_walk = 8
var max_speed_run = 13
var max_speed_lerp_percent = 0.1
export var max_speed = 0
var reset_timer = 0
var resetting = false
var mouse_sensitivity = 0.002  # radians/pixel
var reset_transform = null
var reset_anim_duration = 0.5 # seconds

export var velocity = Vector3()

func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
	max_speed = max_speed_walk
	reset_transform = self.transform

func get_run_percent():
	return ((max_speed - max_speed_walk) / (max_speed_run - max_speed_walk));

func get_reset_timer():
	return reset_timer

func get_input():
	var input_dir = Vector3()
	# desired move in camera direction
	if Input.is_action_pressed("forward"):
		input_dir += -global_transform.basis.z
	if Input.is_action_pressed("right"):
		input_dir += global_transform.basis.x
	if Input.is_action_pressed("back"):
		input_dir += global_transform.basis.z
	if Input.is_action_pressed("left"):
		input_dir += -global_transform.basis.x
	if Input.is_action_just_pressed("exit"):
		Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
	if Input.is_action_just_pressed("reset"):
		resetting = !resetting
		
	input_dir = input_dir.normalized()
	return input_dir

func _unhandled_input(event):
	if event is InputEventMouseMotion and Input.get_mouse_mode() == Input.MOUSE_MODE_CAPTURED:
		rotate_y(-event.relative.x * mouse_sensitivity)
		$Pivot.rotate_x(-event.relative.y * mouse_sensitivity)
		$Pivot.rotation.x = clamp($Pivot.rotation.x, -1.2, 1.2)
	if event is InputEventMouseButton and event.get_button_index() == 1:
		Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)

func _physics_process(delta):
	velocity.y += gravity * delta
	var target_max_speed = max_speed_walk
	if (Input.is_action_pressed("run")): target_max_speed = max_speed_run
	max_speed = lerp(max_speed, target_max_speed, max_speed_lerp_percent)
	var desired_velocity = get_input() * max_speed

	velocity.x = desired_velocity.x
	velocity.z = desired_velocity.z
	velocity = move_and_slide(velocity, Vector3.UP, true)
	
	find_node("Camera").fov = lerp(70, 80, get_run_percent())
	
	if (resetting):
		reset_timer += delta / reset_anim_duration
		if (reset_timer >= 1.0):
			self.transform = reset_transform
			var arcade_lights_node = get_parent().find_node("ArcadeLights")
			arcade_lights_node.hallwayToggle(true)
			arcade_lights_node.allLightsOff()
			reset_timer = 1.0
			resetting = false
	elif (reset_timer > 0.0):
		reset_timer -= delta / reset_anim_duration
		if (reset_timer <= 0.0): reset_timer = 0.0
