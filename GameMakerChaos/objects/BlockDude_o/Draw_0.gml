/// @desc Draw the dude

draw_sprite_ext(
	sprite_index, (global.hasPortalGun ? 1 : 0),
	(self.isFacingLeft ? x + sprite_width : x),
	y,
	(self.isFacingLeft ? -image_xscale : image_xscale),
	image_yscale,
	image_angle,
	image_blend,
	image_alpha
);

