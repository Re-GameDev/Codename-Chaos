function ColorLerp(color1, color2, amount)
{
	var red1   = color_get_red(color1);
	var green1 = color_get_green(color1);
	var blue1  = color_get_blue(color1);
	var red2   = color_get_red(color2);
	var green2 = color_get_green(color2);
	var blue2  = color_get_blue(color2);
	return make_color_rgb(
		lerp(red1,   red2,   amount),
		lerp(green1, green2, amount),
		lerp(blue1,  blue2,  amount)
	);
}

function ColorMultiply(color1, color2)
{
	var red1   = color_get_red(color1);
	var green1 = color_get_green(color1);
	var blue1  = color_get_blue(color1);
	var red2   = color_get_red(color2);
	var green2 = color_get_green(color2);
	var blue2  = color_get_blue(color2);
	return make_color_rgb(
		(red1   / 255) * (red2   / 255) * 255,
		(green1 / 255) * (green2 / 255) * 255,
		(blue1  / 255) * (blue2  / 255) * 255
	);
}

