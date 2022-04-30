enum Ease
{
	Linear,
	QuadraticIn,
	QuadraticOut,
	QuadraticInOut,
	CubicIn,
	CubicOut,
	CubicInOut,
	QuarticIn,
	QuarticOut,
	QuarticInOut,
	QuinticIn,
	QuinticOut,
	QuinticInOut,
	SineIn,
	SineOut,
	SineInOut,
	CircularIn,
	CircularOut,
	CircularInOut,
	ExponentialIn,
	ExponentialOut,
	ExponentialInOut,
	ElasticIn,
	ElasticOut,
	ElasticInOut,
	BackIn,
	BackOut,
	BackInOut,
	BounceIn,
	BounceOut,
	BounceInOut,
	EarlyInOut
};

function EaseLinear(t)
{
	return t;
}

function EaseQuadraticIn(t)
{
	return t * t;
}

function EaseQuadraticOut(t)
{
	return -(t * (t - 2));
}

function EaseQuadraticInOut(t)
{
	if (t < 0.5)
	{
		return 2 * t * t;
	}
	else
	{
		return (-2 * t * t) + (4 * t) - 1;
	}
}

function EaseCubicIn(t)
{
	return t * t * t;
}
function EaseCubicOut(t)
{
	var f = (t - 1);
	return f * f * f + 1;
}
function EaseCubicInOut(t)
{
	if(t < 0.5)
	{
		return 4 * t * t * t;
	}
	else
	{
		var f = ((2 * t) - 2);
		return 0.5 * f * f * f + 1;
	}
}
function EaseQuarticIn(t)
{
	return t * t * t * t;
}
function EaseQuarticOut(t)
{
	var f = (t - 1);
	return f * f * f * (1 - t) + 1;
}
function EaseQuarticInOut(t) 
{
	if(t < 0.5)
	{
		return 8 * t * t * t * t;
	}
	else
	{
		var f = (t - 1);
		return -8 * f * f * f * f + 1;
	}
}
function EaseQuinticIn(t) 
{
	return t * t * t * t * t;
}
function EaseQuinticOut(t) 
{
	var f = (t - 1);
	return f * f * f * f * f + 1;
}
function EaseQuinticInOut(t) 
{
	if(t < 0.5)
	{
		return 16 * t * t * t * t * t;
	}
	else
	{
		var f = ((2 * t) - 2);
		return  0.5 * f * f * f * f * f + 1;
	}
}
function EaseSineIn(t)
{
	return sin((t - 1) * (pi*2)) + 1;
}
function EaseSineOut(t)
{
	return sin(t * (pi*2));
}
function EaseSineInOut(t)
{
	return 0.5 * (1 - cos(t * pi));
}
function EaseCircularIn(t)
{
	return 1 - sqrt(1 - (t * t));
}
function EaseCircularOut(t)
{
	return sqrt((2 - t) * t);
}
function EaseCircularInOut(t)
{
	if (t < 0.5)
	{
		return 0.5 * (1 - sqrt(1 - 4 * (t * t)));
	}
	else
	{
		return 0.5 * (sqrt(-((2 * t) - 3) * ((2 * t) - 1)) + 1);
	}
}
function EaseExponentialIn(t)
{
	return (t == 0.0) ? t : pow(2, 10 * (t - 1));
}
function EaseExponentialOut(t)
{
	return (t == 1.0) ? t : 1 - pow(2, -10 * t);
}
function EaseExponentialInOut(t)
{
	if(t == 0.0 || t == 1.0) { return t; }
	
	if(t < 0.5)
	{
		return 0.5 * pow(2, (20 * t) - 10);
	}
	else
	{
		return -0.5 * pow(2, (-20 * t) + 10) + 1;
	}
}
function EaseElasticIn(t)
{
	return sin(13 * (pi*2) * t) * pow(2, 10 * (t - 1));
}
function EaseElasticOut(t)
{
	return sin(-13 * (pi*2) * (t + 1)) * pow(2, -10 * t) + 1;
}
function EaseElasticInOut(t)
{
	if (t < 0.5)
	{
		return 0.5 * sin(13 * (pi*2) * (2 * t)) * pow(2, 10 * ((2 * t) - 1));
	}
	else
	{
		return 0.5 * (sin(-13 * (pi*2) * ((2 * t - 1) + 1)) * pow(2, -10 * (2 * t - 1)) + 2);
	}
}
function EaseBackIn(t)
{
	return t * t * t - t * sin(t * pi);
}
function EaseBackOut(t)
{
	var f = (1 - t);
	return 1 - (f * f * f - f * sin(f * pi));
}
function EaseBackInOut(t)
{
	if (t < 0.5)
	{
		var f = 2 * t;
		return 0.5 * (f * f * f - f * sin(f * pi));
	}
	else
	{
		var f = (1 - (2*t - 1));
		return 0.5 * (1 - (f * f * f - f * sin(f * pi))) + 0.5;
	}
}
function EaseBounceOut(t)
{
	if (t < 4/11.0)
	{
		return (121 * t * t)/16.0;
	}
	else if (t < 8/11.0)
	{
		return (363/40.0 * t * t) - (99/10.0 * t) + 17/5.0;
	}
	else if (t < 9/10.0)
	{
		return (4356/361.0 * t * t) - (35442/1805.0 * t) + 16061/1805.0;
	}
	else
	{
		return (54/5.0 * t * t) - (513/25.0 * t) + 268/25.0;
	}
}
function EaseBounceIn(t)
{
	return 1 - EaseBounceOut(1 - t);
}
function EaseBounceInOut(t)
{
	if (t < 0.5)
	{
		return 0.5 * EaseBounceIn(t*2);
	}
	else
	{
		return 0.5 * EaseBounceOut(t * 2 - 1) + 0.5;
	}
}
function EaseEarlyInOut(t)
{
	var t2 = (1.2 * t);
	if (t < 0.418)
	{
		return 2 * t2 * t2;
	}
	else if (t < 0.833)
	{
		return (-2 * t2 * t2) + (4 * t2) - 1;
	}
	else
	{
		return 1;
	}
}

function Ease(easingMode, t)
{
	switch (easingMode)
	{
		case Ease.Linear:         return EaseLinear(t);
		case Ease.QuadraticIn:    return EaseQuadraticIn(t);
		case Ease.QuadraticOut:   return EaseQuadraticOut(t);
		case Ease.QuadraticInOut: return EaseQuadraticInOut(t);
	}
	return t;
}
