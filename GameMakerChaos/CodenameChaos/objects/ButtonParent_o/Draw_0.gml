/// @desc Draw button and text

draw_sprite_ext(
	sprite_index, image_index,
	x, y,
	image_xscale, image_yscale,
	image_angle,
	image_blend,
	image_alpha
);

//Hand coded values to make the text move slightly when using the button hover/pressed sprites
var textOffset = (self.isPressed ? 3 : (self.image_index == 1 ? -7 : -10));

draw_set_font(DebugFont_f);
var stringSize = new Vec2(string_width(self.displayStr), string_height(self.displayStr));

//TODO: This should probably take into account the origin of the current sprite (we are assuming top left right now)
var displayStrPos = new Vec2(x + sprite_width/2 - stringSize.x/2, y + sprite_height/2 - stringSize.y/2 + textOffset);
displayStrPos.AlignTo(1);

draw_set_color(self.displayStrColor);
draw_text(displayStrPos.x, displayStrPos.y, self.displayStr);

