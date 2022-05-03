
enum VerticalAlign
{
	Top,
	Center,
	Bottom,
};

function PromptInfo(_str, _imageIndex, _width) constructor
{
	str = _str; //should be lowercase
	imageIndex = _imageIndex;
	width = _width; //num frames (can be float which will be ceil'd to get true number of frames but float value used for sizing calcs)
}

function InitButtonPromptGlobals()
{
	global.promptInfos = [
		new PromptInfo("a",  0, 1),
		new PromptInfo("b",  2, 1),
		new PromptInfo("c",  4, 1),
		new PromptInfo("d",  6, 1),
		new PromptInfo("e",  8, 1),
		new PromptInfo("f", 10, 1),
		new PromptInfo("g", 12, 1),
		new PromptInfo("h", 14, 1),
		new PromptInfo("i", 16, 1),
		new PromptInfo("j", 18, 1),
		new PromptInfo("k", 20, 1),
		new PromptInfo("l", 22, 1),
		new PromptInfo("m", 24, 1),
		new PromptInfo("n", 26, 1),
		new PromptInfo("o", 28, 1),
		new PromptInfo("p", 30, 1),
		new PromptInfo("q", 32, 1),
		new PromptInfo("r", 34, 1),
		new PromptInfo("s", 36, 1),
		new PromptInfo("t", 38, 1),
		new PromptInfo("u", 40, 1),
		new PromptInfo("v", 42, 1),
		new PromptInfo("w", 44, 1),
		new PromptInfo("x", 46, 1),
		new PromptInfo("y", 48, 1),
		new PromptInfo("z", 50, 1),
		new PromptInfo("0", 52, 1),
		new PromptInfo("1", 54, 1),
		new PromptInfo("2", 56, 1),
		new PromptInfo("3", 58, 1),
		new PromptInfo("4", 60, 1),
		new PromptInfo("5", 62, 1),
		new PromptInfo("6", 64, 1),
		new PromptInfo("7", 66, 1),
		new PromptInfo("8", 68, 1),
		new PromptInfo("9", 70, 1),
		
		new PromptInfo("blank",        72, 1),
		new PromptInfo("exclamation",  74, 1),
		new PromptInfo("quote",        76, 1),
		new PromptInfo("pound",        78, 1),
		new PromptInfo("dollar",       80, 1),
		new PromptInfo("percent",      82, 1),
		new PromptInfo("ampersand",    84, 1),
		new PromptInfo("apostrophe",   86, 1),
		new PromptInfo("openparens",   88, 1),
		new PromptInfo("closeparens",  90, 1),
		new PromptInfo("multiply",     92, 1),
		new PromptInfo("plus",         94, 1),
		new PromptInfo("comma",        96, 1),
		new PromptInfo("hyphen",       98, 1),
		new PromptInfo("period",      100, 1),
		new PromptInfo("slash",       102, 1),
		new PromptInfo("colon",       104, 1),
		new PromptInfo("semicolon",   106, 1),
		new PromptInfo("less",        108, 1),
		new PromptInfo("equals",      110, 1),
		new PromptInfo("greater",     112, 1),
		new PromptInfo("question",    114, 1),
		new PromptInfo("opensquare",  116, 1),
		new PromptInfo("closesquare", 118, 1),
		new PromptInfo("backslash",   120, 1),
		new PromptInfo("caret",       122, 1),
		new PromptInfo("underscore",  124, 1),
		new PromptInfo("grave",       126, 1),
		new PromptInfo("opencurly",   128, 1),
		new PromptInfo("closecurly",  130, 1),
		new PromptInfo("pipe",        132, 1),
		new PromptInfo("tilde",       134, 1),
		new PromptInfo("at",          136, 1),
		
		new PromptInfo("left",  138, 1),
		new PromptInfo("down",  140, 1),
		new PromptInfo("right", 142, 1),
		new PromptInfo("up",    144, 1),
		
		new PromptInfo("f1",  146, 1),
		new PromptInfo("f2",  148, 1),
		new PromptInfo("f3",  150, 1),
		new PromptInfo("f4",  152, 1),
		new PromptInfo("f5",  154, 1),
		new PromptInfo("f6",  156, 1),
		new PromptInfo("f7",  158, 1),
		new PromptInfo("f8",  160, 1),
		new PromptInfo("f9",  162, 1),
		new PromptInfo("f10", 164, 1),
		new PromptInfo("f11", 166, 1),
		new PromptInfo("f12", 168, 1),
		
		new PromptInfo("ctrl",       170, 2),
		new PromptInfo("back",       174, 2),
		new PromptInfo("caps",       178, 2),
		new PromptInfo("esc",        182, 1.5),
		new PromptInfo("tab",        186, 1.5),
		new PromptInfo("alt",        190, 1.5),
		new PromptInfo("menu",       194, 1.5),
		new PromptInfo("windows",    198, 1.5),
		new PromptInfo("end",        202, 2),
		new PromptInfo("home",       206, 2),
		new PromptInfo("pageup",     210, 2),
		new PromptInfo("pagedown",   214, 2),
		new PromptInfo("pause",      218, 2),
		new PromptInfo("delete",     222, 2.25),
		new PromptInfo("insert",     228, 2.25),
		new PromptInfo("print",      234, 2.25),
		new PromptInfo("scrolllock", 240, 2.25),
		new PromptInfo("shift",      246, 2.25),
		new PromptInfo("enter",      252, 2.25),
		new PromptInfo("space",      258, 2.25),
		new PromptInfo("backwide",   264, 2.25),
		new PromptInfo("spacewide",  270, 2.75),
	];
	global.numPromptInfos = array_length(global.promptInfos);
}

function GetButtonPromptInfoByString(promptStr)
{
	var lowerPromptStr = string_lower(promptStr);
	for (var pIndex = 0; pIndex < global.numPromptInfos; pIndex++)
	{
		if (lowerPromptStr == global.promptInfos[pIndex].str)
		{
			return global.promptInfos[pIndex];
		}
	}
	return 0;
}

//NOTE: This function doesn't support line breaks properly right now
function DrawTextWithButtonPrompts(font, startPos, color, promptsScale, formatStr, animated = true, align = VerticalAlign.Center, colorPrompts = false)
{
	var formatStrLength = string_length(formatStr);
	var stringParts = [];
	var buttonPromptParts = [];
	var lastSplitIndex = 0;
	for (var cIndex = 0; cIndex < formatStrLength; cIndex++)
	{
		var thisChar = StringCharAt(formatStr, cIndex);
		if (thisChar == "{")
		{
			var closeBracketIndex = cIndex+1;
			while (closeBracketIndex < formatStrLength && StringCharAt(formatStr, closeBracketIndex) != "}")
			{
				closeBracketIndex++;
			}
			if (closeBracketIndex < formatStrLength)
			{
				if (cIndex > lastSplitIndex)
				{
					array_push(stringParts, StringPart(formatStr, lastSplitIndex, cIndex - lastSplitIndex));
				}
				else
				{
					array_push(stringParts, "");
				}
				array_push(buttonPromptParts, StringPart(formatStr, cIndex+1, closeBracketIndex - (cIndex+1)));
				lastSplitIndex = closeBracketIndex+1;
				cIndex = closeBracketIndex;
			}
		}
	}
	if (lastSplitIndex < formatStrLength)
	{
		array_push(stringParts, StringPart(formatStr, lastSplitIndex, formatStrLength - lastSplitIndex));
	}
	
	draw_set_font(font);
	draw_set_color(color);
	var drawPos = startPos.Copy();
	for (var sIndex = 0; sIndex < array_length(stringParts); sIndex++)
	{
		var text = stringParts[sIndex];
		var textSize = new Vec2(string_width(text), string_height(text));
		//if (textSize.y <= 0)
		{
			textSize.y = string_height("W!ygp[]|^`'");
		}
		
		draw_text(drawPos.x, drawPos.y, stringParts[sIndex]);
		drawPos.x += textSize.x;
		
		if (sIndex < array_length(buttonPromptParts))
		{
			var promptStr = buttonPromptParts[sIndex];
			var promptInfo = GetButtonPromptInfoByString(promptStr);
			if (promptInfo == 0)
			{
				promptInfo = GetButtonPromptInfoByString("Blank");
			}
			var promptSize = new Vec2(sprite_get_width(Keyboard16_s) * promptsScale * promptInfo.width, sprite_get_height(Keyboard16_s) * promptsScale);
			var promptPos = new Vec2(drawPos.x, drawPos.y);
			if (align == VerticalAlign.Bottom)
			{
				promptPos.y += textSize.y - promptSize.y;
			}
			else if (align == VerticalAlign.Center)
			{
				promptPos.y += textSize.y/2 - promptSize.y/2;
			}
			for (var frameIndex = 0; frameIndex < promptInfo.width; frameIndex++)
			{
				draw_sprite_ext(
					Keyboard16_s,
					promptInfo.imageIndex + (frameIndex*2) + (animated ? ((global.ProgramTime % 1000) >= 500 ? 1 : 0) : 0),
					promptPos.x, promptPos.y,
					promptsScale, promptsScale,
					0,
					(colorPrompts ? color : c_white),
					1.0
				);
				promptPos.x += sprite_get_width(Keyboard16_s) * promptsScale;
			}
			
			drawPos.x += promptSize.x;
		}
	}
}
