/// @desc Update position

x = (self.gridPos.x * global.bdTileSize.x);
y = (self.gridPos.y * global.bdTileSize.y);

if (self.backInstance != noone)
{
	self.backInstance.x = x;
	self.backInstance.y = y;
	self.backInstance.gridPos = self.gridPos.Copy();
	self.backInstance.inDir = self.inDir;
	self.backInstance.sprite_index = self.sprite_index;
}

