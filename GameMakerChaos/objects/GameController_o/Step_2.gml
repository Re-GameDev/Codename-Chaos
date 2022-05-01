
if (!global.cursorSet && window_get_cursor() != cr_default)
{
	window_set_cursor(cr_default);
}

if (!global.mouseClickHandled && mouse_check_button_pressed(mb_left))
{
	show_debug_message("Unhandled mouse click! Deselecting all...");
	var allSelectables = FindAllInstancesOf(SelectableUiParent_o);
	for (var sIndex = 0; sIndex < array_length(allSelectables); sIndex++)
	{
		allSelectables[sIndex].isSelected = false;
	}
}

