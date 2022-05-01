/// @desc Change to default sprite

if (self.isHovered)
{
	self.isHovered = false;
	//PlaySoundEffect(UiClick1_a); //Too many sound effects if we do this imo
}

if (image_index == 1)
{
	image_index = 0;
}

