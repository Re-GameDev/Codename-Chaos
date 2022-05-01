/// @desc Draw current room's cheat code

var currentRoomArea = FindRoomAreaAtGridPos(self.gridPos);
if (currentRoomArea != noone && currentRoomArea.cheatCode != "")
{
	var displayStr = "Level " +
		((currentRoomArea.roomNumber+1 < 10) ? "0" : "") + string(currentRoomArea.roomNumber + 1) +
		"\nCode: " + currentRoomArea.cheatCode;
	draw_set_font(DebugFont_f);
	var displayStrSize = new Vec2(string_width(displayStr), string_height(displayStr));
	var displayStrPos = new Vec2(window_get_width() - 5 - displayStrSize.x, 5);
	draw_set_color(c_black);
	draw_text(displayStrPos.x, displayStrPos.y, displayStr);
}

if (!global.enteringCheatCode && !global.debugModeEnabled)
{
	draw_set_font(DebugFont_f);
	draw_set_color(c_black);
	draw_text(5, 5, "Press INSERT to enter code\nPress R to reset");
}

