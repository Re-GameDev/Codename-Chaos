/// @desc Change to hover sprite

if (!self.isHovered)
{
	self.isHovered = true;
	PlaySoundEffect(UiClick1_a);
}

if (!self.isPressed)
{
	image_index = 1;
}

