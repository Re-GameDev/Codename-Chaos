
#macro DIALOGUE_DEFAULT_HEIGHT     128 //px
#macro DIALOGUE_DEFAULT_OPEN_TIME  200 //ms
#macro DIALOGUE_TEXT_FADE_TIME     0.2 //% of openAnimTime
#macro DIALOGUE_DEFAULT_CLOSE_TIME 250 //ms
#macro DIALOGUE_DEFAULT_PADDING    8 //px (scales w/ backScale)

function Dialogue(_font, _textColor, _text, _backSprite, _backScale) constructor
{
	font = _font;
	textColor = _textColor;
	backSprite = _backSprite;
	backScale = _backScale;
	text = _text;
	vertAlignment = VerticalAlign.Center;
	side = Dir.Down; //only Dir.Up and Dir.Down are supported
	leftSprite = noone;
	autoScaleLeftSprite = true;
	leftSpriteScale = 1;
	rightSprite = noone;
	autoScaleRightSprite = true;
	rightSpriteScale = 1;
	padding = DIALOGUE_DEFAULT_PADDING;
	isOpen = false;
	wasClosed = false;
	openTime = 0;
	closeTime = 0;
	openAnimTime = DIALOGUE_DEFAULT_OPEN_TIME;
	closeAnimTime = DIALOGUE_DEFAULT_CLOSE_TIME;
	
	static Copy = function()
	{
		var result = new Dialogue(self.font, self.textColor, self.text, self.backSprite, self.backScale);
		result.vertAlignment = self.vertAlignment;
		result.side = self.side;
		result.leftSprite = self.leftSprite;
		result.autoScaleLeftSprite = self.autoScaleLeftSprite;
		result.leftSpriteScale = self.leftSpriteScale;
		result.rightSprite = self.rightSprite;
		result.autoScaleRightSprite = self.autoScaleRightSprite;
		result.rightSpriteScale = self.rightSpriteScale;
		result.padding = self.padding;
		result.isOpen = self.isOpen;
		result.wasClosed = self.wasClosed;
		result.openTime = self.openTime;
		result.closeTime = self.closeTime;
		result.openAnimTime = self.openAnimTime;
		result.closeAnimTime = self.closeAnimTime;
		return result;
	}
	
	static GetOpenAmount = function()
	{
		if (self.isOpen)
		{
			if (global.ProgramTime >= self.openTime)
			{
				if (global.ProgramTime < self.openTime + self.openAnimTime)
				{
					return (global.ProgramTime - self.openTime) / self.openAnimTime;
				}
				else { return 1; }
			}
			else { return 0; }
		}
		else
		{
			if (global.ProgramTime >= self.closeTime)
			{
				if (global.ProgramTime < self.closeTime + self.closeAnimTime)
				{
					return 1 - ((global.ProgramTime - self.closeTime) / self.closeAnimTime);
				}
				else { return 0; }
			}
			else { return 1; }
		}
	}
	static GetContentAlpha = function()
	{
		var openAmount = self.GetOpenAmount();
		if (openAmount > 1.0 - DIALOGUE_TEXT_FADE_TIME)
		{
			return (openAmount - (1 - DIALOGUE_TEXT_FADE_TIME)) / DIALOGUE_TEXT_FADE_TIME;
		}
		else
		{
			return 0;
		}
	}
	static GetSize = function(includeAnimation = true)
	{
		var screenSize = new Vec2(window_get_width(), window_get_height());
		var result = new Vec2(screenSize.x, DIALOGUE_DEFAULT_HEIGHT);
		
		if (self.autoScaleLeftSprite && self.leftSprite != noone)
		{
			self.leftSpriteScale = (DIALOGUE_DEFAULT_HEIGHT - (self.padding * self.backScale * 2)) / sprite_get_height(self.leftSprite);
			self.leftSpriteScale = floor(self.leftSpriteScale);
			if (self.leftSpriteScale < 1) { self.leftSpriteScale = 1; }
		}
		if (self.autoScaleRightSprite && self.rightSprite != noone)
		{
			self.rightSpriteScale = (DIALOGUE_DEFAULT_HEIGHT - (self.padding * self.backScale * 2)) / sprite_get_height(self.rightSprite);
			self.rightSpriteScale = floor(self.rightSpriteScale);
			if (self.rightSpriteScale < 1) { self.rightSpriteScale = 1; }
		}
		
		var leftSpriteSize = (self.leftSprite != noone) ? new Vec2(sprite_get_width(self.leftSprite) * self.leftSpriteScale, sprite_get_height(self.leftSprite) * self.leftSpriteScale) : new Vec2(0, 0);
		var rightSpriteSize = (self.rightSprite != noone) ? new Vec2(sprite_get_width(self.rightSprite) * self.rightSpriteScale, sprite_get_height(self.rightSprite) * self.rightSpriteScale) : new Vec2(0, 0);
		
		var textMaxWidth = screenSize.x - self.padding*self.backScale*2;
		if (self.leftSprite != noone)
		{
			textMaxWidth -= leftSpriteSize.x + self.padding*self.backScale;
		}
		if (self.leftSprite != noone)
		{
			textMaxWidth -= rightSpriteSize.x + self.padding*self.backScale;
		}
		draw_set_font(self.font);
		var textSize = new Vec2(string_width(self.text), string_height(self.text));
		
		if (result.y < textSize.y + self.padding*self.backScale*2) { result.y = textSize.y + self.padding*self.backScale*2; }
		if (result.y < leftSpriteSize.y + self.padding*self.backScale*2) { result.y = leftSpriteSize.y + self.padding*self.backScale*2; }
		if (result.y < rightSpriteSize.y + self.padding*self.backScale*2) { result.y = rightSpriteSize.y + self.padding*self.backScale*2; }
		
		if (includeAnimation)
		{
			var openAmount = self.GetOpenAmount();
			if (openAmount < 1)
			{
				result.y *= (self.isOpen ? EaseQuadraticOut(openAmount) : EaseQuadraticIn(openAmount));
			}
		}
		
		return result;
	}
	static GetPosition = function(includeAnimation = true)
	{
		var size = self.GetSize(includeAnimation);
		return new Vec2(0, window_get_height() - size.y);
	}
	static GetTextPos = function(includeAnimation = true)
	{
		var size = self.GetSize(includeAnimation);
		var pos = self.GetPosition(includeAnimation);
		var result = new Vec2(pos.x + self.padding*self.backScale, pos.y + self.padding*self.backScale);
		if (self.leftSprite != noone)
		{
			result.x += (sprite_get_width(self.leftSprite) * self.leftSpriteScale) + self.padding*self.backScale;
		}
		if (self.vertAlignment == VerticalAlign.Center)
		{
			var textSize = new Vec2(string_width(self.text), string_height(self.text));
			result.y = pos.y + size.y/2 - textSize.y/2;
		}
		if (self.vertAlignment == VerticalAlign.Bottom)
		{
			var textSize = new Vec2(string_width(self.text), string_height(self.text));
			result.y = pos.y + size.y - self.padding*self.backScale - textSize.y;
		}
		result.AlignTo(1);
		return result;
	}
	
	static Open = function(forceReopen = false)
	{
		if (!self.isOpen || forceReopen)
		{
			self.isOpen = true;
			self.wasClosed = false;
			self.openTime = global.ProgramTime;
			self.closeTime = 0;
		}
	}
	static Close = function(forceReclose = false)
	{
		if (self.isOpen || forceReclose)
		{
			self.isOpen = false;
			self.wasClosed = true;
			self.closeTime = global.ProgramTime;
		}
	}
	static Draw = function(includeAnimation = true)
	{
		var size = self.GetSize();
		var position = self.GetPosition();
		var textPos = self.GetTextPos();
		var contentAlpha = self.GetContentAlpha();
		
		DrawNineTile(self.backSprite, position, size, c_white, 1.0, self.backScale);
		if (self.leftSprite != noone)
		{
			var leftSpritePos = new Vec2(
				position.x + self.padding*self.backScale,
				position.y + size.y/2 - (sprite_get_height(self.leftSprite)*self.leftSpriteScale)/2
			);
			draw_sprite_ext(
				self.leftSprite, 0,
				leftSpritePos.x, leftSpritePos.y,
				self.leftSpriteScale, self.leftSpriteScale,
				0, c_white, contentAlpha
			);
		}
		if (self.rightSprite != noone)
		{
			var rightSpritePos = new Vec2(
				position.x + size.x - self.padding*self.backScale - sprite_get_width(self.rightSprite)*self.rightSpriteScale,
				position.y + size.y/2 - (sprite_get_height(self.rightSprite)*self.rightSpriteScale)/2
			);
			draw_sprite_ext(
				self.rightSprite, 0,
				rightSpritePos.x, rightSpritePos.y,
				self.rightSpriteScale, self.rightSpriteScale,
				0, c_white, contentAlpha
			);
		}
		
		draw_set_font(self.font);
		draw_set_color(self.textColor);
		draw_set_alpha(contentAlpha);
		draw_text(textPos.x, textPos.y, self.text);
		draw_set_alpha(1);
		
		if (self.GetOpenAmount() >= 1)
		{
			var promptScale = 2;
			if (promptScale > self.backScale) { promptScale = self.backScale; }
			var hintTextPos = new Vec2(position.x + size.x - 42*promptScale, position.y + size.y - 17*promptScale);
			DrawTextWithButtonPrompts(DebugFont_f, hintTextPos, c_white, promptScale, "{Enter}", true, VerticalAlign.Top);
		}
	}
	
}