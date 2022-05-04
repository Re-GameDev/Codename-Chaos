/// @desc Draw debug info

if (global.debugModeEnabled)
{
	var displayStr = "Collected Instances:";
	for (var iIndex = 0; iIndex < array_length(global.collectedInstances); iIndex++)
	{
		var roomInstance = global.collectedInstances(iIndex);
		displayStr += "\n" + string(roomInstance.roomId) + ": " + string(roomInstance.instanceId);
	}
	draw_set_font(DebugFont_f);
	draw_set_color(c_black);
	draw_text(5, 5, displayStr);
}
