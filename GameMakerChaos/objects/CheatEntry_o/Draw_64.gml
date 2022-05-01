/// @desc Draw entry box

if (self.showingEntryBox)
{
	draw_set_font(DebugFont_f);
	var displayStr = self.enteredStr + (((global.ProgramTime % 500) >= 250) ? "|" : " ");
	var enteredStrSize = new Vec2(string_width(displayStr), string_height(displayStr));
	var boxTopLeft = new Vec2(10, 10);
	var boxSize = new Vec2(enteredStrSize.x + 5*2, enteredStrSize.y + 5*2);
	var textDrawPos = Vec2Add(boxTopLeft, new Vec2(5, 5));
	
	draw_set_color(c_white);
	draw_rectangle(boxTopLeft.x, boxTopLeft.y, boxTopLeft.x + boxSize.x, boxTopLeft.y + boxSize.y, false);
	DrawOutline(boxTopLeft, boxSize, 1, c_black);
	draw_set_color(c_black);
	draw_text(textDrawPos.x, textDrawPos.y, displayStr);
}

