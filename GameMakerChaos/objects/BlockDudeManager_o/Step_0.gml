/// @desc Debug place boxes and cheat entry

if (global.debugModeEnabled)
{
	var deviceMousePos = new Vec2(display_mouse_get_x(), display_mouse_get_y());
	if (deviceMousePos.x >= window_get_x() && deviceMousePos.y >= window_get_y() &&
		deviceMousePos.x < window_get_x() + window_get_width() && deviceMousePos.y < window_get_y() + window_get_height())
	{
		var mouseGridPos = new Vec2(floor(mouse_x / global.bdTileSize.x), floor(mouse_y / global.bdTileSize.y));
		var mouseWorldPos = Vec2Mult(mouseGridPos, global.bdTileSize);

		var drawColor = c_red;
		var mouseGridSpace = GetBdGridSpace(mouseGridPos);
		if (mouseGridSpace != 0)
		{
			if (mouse_check_button_pressed(mb_middle) && !mouseGridSpace.IsDude() && !mouseGridSpace.IsSolid())
			{
				mouseGridSpace.SetInstance(instance_create_layer(mouseWorldPos.x, mouseWorldPos.y, "BricksAndBlocks", Block_o));
			}
			else if (mouse_check_button_pressed(mb_right) && mouseGridSpace.IsBlock())
			{
				mouseGridSpace.Clear();
			}
		}
	}
}

