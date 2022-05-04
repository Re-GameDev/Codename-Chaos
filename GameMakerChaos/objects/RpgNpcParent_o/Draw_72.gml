/// @desc Update image_index for facingDir + walking anim

if (sprite_index != noone && sprite_get_number(sprite_index) >= 8)
{
	switch (self.facingDir)
	{
		case Dir.Down:  image_index = 0; break;
		case Dir.Right: image_index = 2; break;
		case Dir.Left:  image_index = 4; break;
		case Dir.Up:    image_index = 6; break;
	}
	if (self.isMoving)
	{
		image_index += ((global.ProgramTime % 500) >= 250 ? 1 : 0);
	}
}

