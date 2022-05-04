/// @desc Move the camera and Light

var viewSize = new Vec2(camera_get_view_width(view_camera[0]), camera_get_view_height(view_camera[0]));
var halfViewSize = Vec2Scale(viewSize, 1/2);
var cameraPos = new Vec2(
	x + sprite_width/2 - halfViewSize.x,
	y + sprite_height/2 - halfViewSize.y
);

//Clamp to room bounderies
cameraPos.x = clamp(cameraPos.x, 0, room_width - viewSize.x);
cameraPos.y = clamp(cameraPos.y, 0, room_height - viewSize.y);

camera_set_view_pos(view_camera[0], cameraPos.x, cameraPos.y);

// Move light
if (self.light != noone)
{
	self.light.radius = global.hasLantern ? 50 : 8;
	self.light.x = self.x + sprite_width/2;
	self.light.y = self.y + sprite_height/2;
}

