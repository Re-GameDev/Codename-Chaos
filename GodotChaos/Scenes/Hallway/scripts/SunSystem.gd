extends Spatial

#tool

var planetoids = [];

func _ready():
#
	for childNode in get_children():
	#
		if (childNode.name.begins_with("Planetoid")): planetoids.append(childNode);
	#
	#print("Found %d planetoids" % planetoids.size());
#

func _process(_delta):
#
	for planetoid in planetoids:
	#
		var planetoidPos = planetoid.global_transform.origin;
		#DebugDraw.draw_line_3d(planetoidPos, planetoidPos + Vector3(0, 5, 0), Color(1, 0, 0));
	#
#
