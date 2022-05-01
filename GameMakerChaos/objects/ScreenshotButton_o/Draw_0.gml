/// @desc Draw screenshot with text and outline

draw_sprite_ext(
	sprite_index, 0,
	x, y,
	image_xscale, image_yscale,
	image_angle,
	image_blend,
	image_alpha
);
if (self.isHovered)
{
	DrawOutline(new Vec2(x, y), new Vec2(sprite_width, sprite_height), 2, c_white);
}

draw_set_font(DebugFont_f);
var stringSize = new Vec2(string_width(self.displayStr), string_height(self.displayStr));

//TODO: This should probably take into account the origin of the current sprite (we are assuming top left right now)
var displayStrPos = new Vec2(x + sprite_width/2 - stringSize.x/2, y - 5 - stringSize.y);
displayStrPos.AlignTo(1);

draw_set_color(self.displayStrColor);
draw_text(displayStrPos.x, displayStrPos.y, self.displayStr);

