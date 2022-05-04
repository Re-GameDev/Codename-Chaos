/// @desc Draw a quad with lighting shader

//TODO: Only draw the lights that intersect with the view. Allowing more total lights in the area (as long as < 8 are on screen at a time)
var lightPositionFloats = [ 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 ];
var lightRadiusFloats = [ 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 ];
var allLights = FindAllInstancesOf(RpgLight_o);
for (var lIndex = 0; lIndex < array_length(allLights) && lIndex < 8; lIndex++)
{
	lightPositionFloats[lIndex*2 + 0] = allLights[lIndex].x;
	lightPositionFloats[lIndex*2 + 1] = allLights[lIndex].y;
	lightRadiusFloats[lIndex] = allLights[lIndex].radius;
}

shader_set(RpgLighting_v);
var lightPositionUniform = shader_get_uniform(RpgLighting_v, "LightPosition");
var lightRadiusUniform = shader_get_uniform(RpgLighting_v, "LightRadius");
shader_set_uniform_f_array(lightPositionUniform, lightPositionFloats);
shader_set_uniform_f_array(lightRadiusUniform, lightRadiusFloats);
draw_sprite_ext(
	Pixel_s, 0,
	x, y,
	sprite_width, sprite_height,
	0, c_white, 1
);
shader_reset();

