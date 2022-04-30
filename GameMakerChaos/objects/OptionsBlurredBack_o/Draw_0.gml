/// @desc Draw background with blur over time

var blurAnimTime = clamp((global.ProgramTime - self.creationTime) / 500, 0, 1);

shader_set(GaussianBlur_v);
var kernelSize = 5; //round(Oscillate(3, 8, 10000, 0));
var kernelScale = lerp(1, 4, blurAnimTime);
var textureSizeUniform = shader_get_uniform(GaussianBlur_v, "TextureSize");
var kernelSizeUniform = shader_get_uniform(GaussianBlur_v, "KernelSize");
shader_set_uniform_f(textureSizeUniform, sprite_get_width(sprite_index) / kernelScale, sprite_get_height(sprite_index) / kernelScale);
shader_set_uniform_i(kernelSizeUniform, kernelSize);

draw_sprite_ext(
	sprite_index, image_index,
	x, y,
	image_xscale, image_yscale,
	image_angle,
	ColorLerp(c_white, make_color_rgb(200, 200, 200), blurAnimTime),
	image_alpha
);

shader_reset();
