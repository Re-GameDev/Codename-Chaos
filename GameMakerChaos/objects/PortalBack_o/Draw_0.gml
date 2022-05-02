/// @desc Draw rotated portal back

var drawPos = Vec2Mult(self.gridPos, global.bdTileSize);
var halfTileSize = Vec2Scale(global.bdTileSize, 1/2);
drawPos.Add(halfTileSize);
drawPos.Add(Vec2Mult(Vec2FromDir(self.inDir), halfTileSize));

var frameIndex = floor((global.ProgramTime % 250) / 125);

draw_sprite_ext(
	sprite_index, 2 + frameIndex,
	drawPos.x, drawPos.y,
	image_xscale, image_yscale,
	GetAngleFromDir(self.inDir),
	image_blend,
	image_alpha
);
