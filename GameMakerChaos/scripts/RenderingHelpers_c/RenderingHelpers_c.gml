function DrawOutline(topLeft, size, thickness, color)
{
	draw_set_color(color);
	draw_rectangle(topLeft.x, topLeft.y, topLeft.x + size.x, topLeft.y + thickness, false);
	draw_rectangle(topLeft.x, topLeft.y, topLeft.x + thickness, topLeft.y + size.y, false);
	draw_rectangle(topLeft.x + size.x-thickness, topLeft.y, topLeft.x + size.x, topLeft.y + size.y, false);
	draw_rectangle(topLeft.x, topLeft.y + size.y-thickness, topLeft.x + size.x, topLeft.y + size.y, false);
}
