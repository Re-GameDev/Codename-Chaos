function DrawOutline(topLeft, size, thickness, color)
{
	draw_set_color(color);
	draw_rectangle(topLeft.x, topLeft.y, topLeft.x + size.x, topLeft.y + thickness, false);
	draw_rectangle(topLeft.x, topLeft.y, topLeft.x + thickness, topLeft.y + size.y, false);
	draw_rectangle(topLeft.x + size.x-thickness, topLeft.y, topLeft.x + size.x, topLeft.y + size.y, false);
	draw_rectangle(topLeft.x, topLeft.y + size.y-thickness, topLeft.x + size.x, topLeft.y + size.y, false);
}

function DrawNineTile(sprite, topLeft, size, color, alpha = 1, scale = 1)
{
	var spriteSize = new Vec2(sprite_get_width(sprite), sprite_get_height(sprite));
	var tileSize = Vec2Scale(spriteSize, scale);
	if (tileSize.x <= 0 || tileSize.y <= 0) { return; } //would hit infinite loops, we don't handle this right now
	
	// Start cutting into the corners and edges if the box gets smaller than 2 tiles wide/tall
	var cutWidth = min(tileSize.x, size.x / 2);
	var cutSpriteWidth = (cutWidth / scale);
	var cutHeight = min(tileSize.y, size.y / 2);
	var cutSpriteHeight = (cutHeight / scale);
	
	// Draw Centers
	for (var yOffset = tileSize.y; yOffset < size.y - tileSize.y; yOffset += tileSize.y)
	{
		for (var xOffset = tileSize.x; xOffset < size.x - tileSize.x; xOffset += tileSize.x)
		{
			var tileWidth = min(tileSize.x, (size.x - tileSize.x) - xOffset);
			var spriteWidth = (tileWidth / scale);
			var tileHeight = min(tileSize.y, (size.y - tileSize.y) - yOffset);
			var spriteHeight = (tileHeight / scale);
			draw_sprite_part_ext(
				sprite, 0,
				0, 0, spriteWidth, spriteHeight,
				topLeft.x + xOffset, topLeft.y + yOffset,
				scale, scale,
				color,
				alpha
			);
		}
	}
	
	// Draw Top and Bottom Sides
	for (var xOffset = tileSize.x; xOffset < size.x - tileSize.x; xOffset += tileSize.x)
	{
		var tileWidth = min(tileSize.x, (size.x - tileSize.x) - xOffset);
		var spriteWidth = (tileWidth / scale);
		draw_sprite_part_ext(
			sprite, 2,
			0, 0, spriteWidth, cutSpriteHeight,
			topLeft.x + xOffset, topLeft.y,
			scale, scale,
			color,
			alpha
		);
		draw_sprite_part_ext(
			sprite, 6,
			0, spriteSize.y - cutSpriteHeight, spriteWidth, cutSpriteHeight,
			topLeft.x + xOffset, topLeft.y + size.y - cutHeight,
			scale, scale,
			color,
			alpha
		);
	}
	
	// Draw Left and Right Sides
	for (var yOffset = tileSize.y; yOffset < size.y - tileSize.y; yOffset += tileSize.y)
	{
		var tileHeight = min(tileSize.y, (size.y - tileSize.y) - yOffset);
		var spriteHeight = (tileHeight / scale);
		draw_sprite_part_ext(
			sprite, 8,
			0, 0, cutSpriteWidth, spriteHeight,
			topLeft.x, topLeft.y + yOffset,
			scale, scale,
			color,
			alpha
		);
		draw_sprite_part_ext(
			sprite, 4,
			spriteSize.x - cutSpriteWidth, 0, cutSpriteWidth, spriteHeight,
			topLeft.x + size.x - cutWidth, topLeft.y + yOffset,
			scale, scale,
			color,
			alpha
		);
	}
	
	//Draw Corners
	draw_sprite_part_ext( //top left corner
		sprite, 1,
		0, 0, cutSpriteWidth, cutSpriteHeight,
		topLeft.x, topLeft.y,
		scale, scale,
		color,
		alpha
	);
	draw_sprite_part_ext( //top right corner
		sprite, 3,
		spriteSize.x - cutSpriteWidth, 0, cutSpriteWidth, cutSpriteHeight,
		topLeft.x + size.x - cutWidth, topLeft.y,
		scale, scale,
		color,
		alpha
	);
	draw_sprite_part_ext( //bottom right corner
		sprite, 5,
		spriteSize.x - cutSpriteWidth, spriteSize.y - cutSpriteHeight, cutSpriteWidth, cutSpriteHeight,
		topLeft.x + size.x - cutWidth, topLeft.y + size.y - cutHeight,
		scale, scale,
		color,
		alpha
	);
	draw_sprite_part_ext( //bottom left corner
		sprite, 7,
		0, spriteSize.y - cutSpriteHeight, cutSpriteWidth, cutSpriteHeight,
		topLeft.x, topLeft.y + size.y - cutHeight,
		scale, scale,
		color,
		alpha
	);
}

