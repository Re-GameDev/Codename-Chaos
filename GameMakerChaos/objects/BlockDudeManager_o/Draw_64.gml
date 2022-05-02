/// @desc Debug draw mouse grid pos

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
			drawColor = c_blue;
			if (mouseGridSpace.IsSolid())
			{
				if (mouseGridSpace.objectId == Block_o)
				{
					drawColor = c_green;
				}
				else if (mouseGridSpace.objectId == Brick_o)
				{
					drawColor = c_yellow;
				}
				else
				{
					drawColor = c_orange;
				}
			}
			else if (mouseGridSpace.IsDude())
			{
				drawColor = c_lime;
			}
		}

		var viewPos = new Vec2(camera_get_view_x(view_camera[0]), camera_get_view_y(view_camera[0]));
		var viewZoom = new Vec2(view_wport[0] / camera_get_view_width(view_camera[0]), view_hport[0] / camera_get_view_height(view_camera[0]));
		var mouseScreenPos = new Vec2(
			(mouseWorldPos.x - viewPos.x) * viewZoom.x,
			(mouseWorldPos.y - viewPos.y) * viewZoom.y
		);
		draw_sprite_ext(
			DebugSquare8x8_s, 0,
			mouseScreenPos.x, mouseScreenPos.y,
			viewZoom.x, viewZoom.y,
			0, drawColor,
			0.5
		);
		
		var displayStr = "Press HOME to skip level\n" +
			"MIDDLE CLICK to place and RIGHT CLICK to delete\n" +
			"(" + string(mouseGridPos.x) + ", " + string(mouseGridPos.y) + ")\n" +
			"(" + string(mouseScreenPos.x) + ", " + string(mouseScreenPos.y) + ")\n" +
			string(mouseGridSpace);
		if (mouseGridSpace != 0)
		{
			if (mouseGridSpace.instance != noone)
			{
				displayStr += "\n(" + string(mouseGridSpace.instance.gridPos.x) + ", " + string(mouseGridSpace.instance.gridPos.y) + ")";
			}
			for (var dIndex = 0; dIndex < 4; dIndex++)
			{
				var side = DirFromIndex(dIndex);
				var portal = mouseGridSpace.GetPortalOnSide(side);
				if (portal != 0 && portal.instance != noone)
				{
					displayStr += "\n" + GetDirStr(side) + " portal: " + string(portal.instance) + " other: " + string(portal.otherInstance);
				}
			}
			if (mouseGridSpace.IsDude())
			{
				displayStr += "\nDude Orange: " + string(mouseGridSpace.instance.orangePortal) + " Blue: " + string(mouseGridSpace.instance.bluePortal);
			}
		}
		
		draw_set_font(DebugFont_f);
		draw_text(10, 10, displayStr);
	}
}


