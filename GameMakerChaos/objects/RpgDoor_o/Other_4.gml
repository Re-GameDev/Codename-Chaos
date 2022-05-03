/// @desc Teleport player to this door if it was the target

if (global.targetDoorNum == self.doorNum)
{
	var hero = instance_find(RpgHero_o, 0);
	if (hero != noone)
	{
		hero.x = self.x + sprite_width/2 - hero.sprite_width/2;
		hero.y = self.y + sprite_height/2 - hero.sprite_height/2;
		self.heroJustCameThrough = true; //gets set to false by RpgHero_o door collision checks (in Step)
	}
}
