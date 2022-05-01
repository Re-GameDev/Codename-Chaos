/// @desc Init vars
event_inherited();

self.isMasterSlider = false;
self.isMusicSlider = false;
self.isSoundEffectSlider = false;

self.barSprite = sprite_index;
self.handleSprite = VolumeBarHandle_s;

self.currVolume = 0.5;
self.isHandleGrabbed = false;
self.handleGrabOffset = new Vec2(0, 0);

self.firstUpdate = true;

function UpdateRecs()
{
	self.spriteSize = new Vec2(sprite_get_width(self.barSprite), sprite_get_height(self.barSprite));
	self.totalSize = Vec2Mult(self.spriteSize, new Vec2(image_xscale, image_yscale));
	var gutterLeftRightPercent = 0.123;
	var gutterWidthPercent = (1 - (gutterLeftRightPercent*2));
	self.barTotalWidth = self.spriteSize.x * gutterWidthPercent;
	self.barOffsetX = (self.spriteSize.x * gutterLeftRightPercent);
	self.barDrawWidth = self.barOffsetX + (self.currVolume * self.barTotalWidth);
	self.barEndPos = new Vec2(x + (self.barDrawWidth * image_xscale), y + self.totalSize.y/2);
	self.barEndPos.AlignTo(1);
	self.handleSize = new Vec2(sprite_get_width(self.handleSprite) * image_xscale, sprite_get_height(self.handleSprite) * image_yscale);
	self.handlePos = Vec2Sub(self.barEndPos, Vec2Scale(self.handleSize, 1/2));
}
