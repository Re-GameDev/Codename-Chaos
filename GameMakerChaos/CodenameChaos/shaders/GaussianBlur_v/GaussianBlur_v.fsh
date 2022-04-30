
uniform vec2 TextureSize;
uniform int KernelSize;

varying vec2 fTexCoord;
varying vec4 fColor;

void main()
{
	vec2 texelSize = vec2(1, 1) / TextureSize;
	vec4 sampleColor = vec4(0, 0, 0, 0);
	int numPixels = KernelSize * KernelSize;
	for (int yOffset = 0; yOffset < KernelSize; yOffset++)
	{
		for (int xOffset = 0; xOffset < KernelSize; xOffset++)
		{
			vec2 sampleCoord = fTexCoord;
			sampleCoord.x = sampleCoord.x + (float(xOffset - KernelSize/2) * (texelSize.x));
			sampleCoord.y = sampleCoord.y + (float(yOffset - KernelSize/2) * (texelSize.y));
			sampleColor += texture2D(gm_BaseTexture, sampleCoord);
		}
	}
	sampleColor = sampleColor / vec4(numPixels, numPixels, numPixels, numPixels);
	
    gl_FragColor = fColor * sampleColor;
}
