/// @desc Custom rendering

UpdateRecs();

draw_sprite_ext(
	self.barSprite,
	0,
	x, y,
	image_xscale, image_yscale,
	image_angle,
	image_blend,
	image_alpha
);

if (self.isSelected)
{
	draw_sprite_ext(
		self.barSprite,
		2,
		x, y,
		image_xscale, image_yscale,
		image_angle,
		image_blend,
		image_alpha
	);
}


draw_sprite_part_ext(
	self.barSprite,
	1,
	0, 0, self.barDrawWidth, self.spriteSize.y,
	x, y,
	image_xscale, image_yscale,
	image_blend,
	image_alpha
);

draw_sprite_ext(
	self.handleSprite, 0,
	self.handlePos.x, self.handlePos.y,
	image_xscale, image_yscale,
	image_angle,
	image_blend,
	image_alpha
);

draw_set_font(DebugFont_f);
var displayStr = "[Unset]";
if (self.isMasterSlider) { displayStr = "Master Volume"; }
else if (self.isMusicSlider) { displayStr = "Music Volume"; }
else if (self.isSoundEffectSlider) { displayStr = "Sound Effects Volume"; }
draw_set_color(make_color_rgb(129, 134, 155));
draw_text(x + (self.barOffsetX * image_xscale), y - 5 - font_get_size(DebugFont_f), displayStr + ": " + string(round(self.currVolume*100)) + "%");

