
uniform vec2 LightPosition[8];
uniform float LightRadius[8];

varying vec2 v_vTexcoord;
varying vec4 v_vColour;
varying vec3 v_RoomPos;

void main()
{
	float brightness = 0.0;
	for (int lIndex = 0; lIndex < 8; lIndex++)
	{
		float lightDist = length(LightPosition[lIndex] - v_RoomPos.xy);
		if (lightDist < LightRadius[lIndex])
		{
			brightness += 0.5; //TODO: Make this calculation better
		}
	}
	if (brightness > 1.0) { brightness = 1.0; }
    gl_FragColor = vec4(0, 0, 0, 1.0 - brightness);
}
