
uniform vec2 TextureSize;
uniform int KernelSize;
const int MAX_KERNEL_SIZE = 15;

varying vec2 fTexCoord;
varying vec4 fColor;

void main()
{
	vec2 texelSize = vec2(1, 1) / TextureSize;
	vec4 sampleColor = vec4(0, 0, 0, 0);
	int numPixels = KernelSize * KernelSize;
	for (int yOffset = 0; yOffset < MAX_KERNEL_SIZE; yOffset++)
	{
		if (yOffset >= KernelSize) { break; }
		for (int xOffset = 0; xOffset < MAX_KERNEL_SIZE; xOffset++)
		{
			if (xOffset >= KernelSize) { break; }
			vec2 sampleCoord = fTexCoord;
			sampleCoord.x = sampleCoord.x + (float(xOffset - KernelSize/2) * (texelSize.x));
			sampleCoord.y = sampleCoord.y + (float(yOffset - KernelSize/2) * (texelSize.y));
			sampleColor += texture2D(gm_BaseTexture, sampleCoord);
		}
	}
	sampleColor = sampleColor / vec4(numPixels, numPixels, numPixels, numPixels);
	
    gl_FragColor = fColor * sampleColor;
}
