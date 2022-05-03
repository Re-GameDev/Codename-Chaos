/// @desc Draw sprite

draw_sprite_ext(
	sprite_index, image_index,
	x, y + (self.onPedestals ? -3 : 0),
	image_xscale, image_yscale,
	image_angle,
	image_blend,
	image_alpha
);

