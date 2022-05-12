extends CSGMesh

var time = 0;

func _ready():
#
	var newMat = SpatialMaterial.new();
	var loadedTexture = ResourceLoader.load("res://Sprites/OttersLoveItSheet.png");
	newMat.set_texture(SpatialMaterial.TEXTURE_ALBEDO, loadedTexture);
	self.material = newMat;
	self.material.emission_enabled = true;
	self.material.emission_energy = 0.8;
	self.material.emission_operator = SpatialMaterial.EMISSION_OP_ADD;
	self.material.emission_texture = loadedTexture;
	var oneThird = 1.0 / 3.0;
	self.material.uv1_offset = Vector3(1*oneThird, 2*oneThird, 0);
	self.material.uv1_scale = Vector3(oneThird, oneThird, 0);
	
	$OverlayScreen.material = newMat.duplicate();
	$OverlayScreen.material.uv1_offset.y = 0*oneThird;
	$OverlayScreen.material.flags_transparent = true;
	#$OverlayScreen.transform.origin.x += 1;
	
#

func _process(delta):
#
	time += delta;
	var playerDistance = (get_parent().get_parent().find_node("Player").transform.origin - self.transform.origin).length();
	var playerDistanceNormalized = clamp((15.0 - playerDistance) / 15.0, 0.0, 1.0);
	#self.material.flags_transparent = true;
	#self.material.albedo_color.a = playerDistanceNormalized;
	var animProgress = playerDistanceNormalized;#(cos(1*time) + 1) / 2;
	var frameIndex = int(floor(animProgress * 8));
	var invFrameNum = 1.0 / 8.0;
	var frameProgress = (animProgress - (frameIndex * invFrameNum)) / invFrameNum;
	if (frameIndex >= 9): frameIndex = 8;
	self.material.uv1_offset = Vector3((frameIndex % 3)/3.0, (frameIndex / 3)/3.0, 0);
	$OverlayScreen.material.uv1_offset = Vector3(((frameIndex+1) % 3)/3.0, ((frameIndex+1) / 3)/3.0, 0);
	$OverlayScreen.material.albedo_color.a = frameProgress;
#
