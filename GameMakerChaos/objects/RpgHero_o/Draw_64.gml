/// @desc Draw debug stuff

if (!global.showingDialogue && !global.enteringCheatCode)
{
	var targetDirVec = Vec2Mult(Vec2FromDir(self.facingDir), new Vec2(sprite_width, sprite_height));
	var targetPos = new Vec2(x + sprite_width/2 + targetDirVec.x, y + sprite_height/2 + targetDirVec.y);
	var targetInstance = instance_position(targetPos.x, targetPos.y, RpgInteractableParent_o);
	if (targetInstance != noone && targetInstance.hintText != "")
	{
		DrawTextWithButtonPrompts(
			DebugFont_f,
			new Vec2(5, window_get_height() - 5 - 24),
			c_white,
			2,
			"{Enter} " + targetInstance.hintText
		);
	}
	
	draw_set_font(DebugFont_f);
	var coinGuiScale = 3;
	var coinGuiPadding = 2;
	var coinGuiSize = new Vec2((sprite_get_width(RpgCoin_s)-2) * coinGuiScale, (sprite_get_height(RpgCoin_s)-2) * coinGuiScale);
	var coinGuiPos = new Vec2(window_get_width() - coinGuiPadding - coinGuiSize.x, window_get_height() - coinGuiPadding - coinGuiSize.y);
	var coinGuiText = string(global.numCoins);
	var coinGuiTextSize = new Vec2(string_width(coinGuiText), string_height(coinGuiText));
	var coinGuiTextPos = new Vec2(coinGuiPos.x - coinGuiTextSize.x, coinGuiPos.y + coinGuiSize.y/2 - coinGuiTextSize.y/2);
	coinGuiTextPos.AlignTo(1);
	var coinGuiBackPos = new Vec2(coinGuiTextPos.x - 12*coinGuiScale, min(coinGuiTextPos.y, coinGuiPos.y) - 2*coinGuiScale);
	var coinGuiBackSize = new Vec2(window_get_width() - coinGuiBackPos.x + 100, window_get_height() - coinGuiBackPos.y + 100);
	//draw_set_color(c_black);
	//draw_set_alpha(0.4);
	//draw_rectangle(coinGuiBackPos.x, coinGuiBackPos.y, window_get_width(), window_get_height(), false);
	//draw_set_alpha(1);
	DrawNineTile(RpgDialogueBack2_s, coinGuiBackPos, coinGuiBackSize, c_white, 0.8, coinGuiScale);
	draw_sprite_ext(RpgCoin_s, 0, coinGuiPos.x - 1*coinGuiScale, coinGuiPos.y - 1*coinGuiScale, coinGuiScale, coinGuiScale, 0, c_white, 1);
	draw_set_color(c_white);
	draw_text(coinGuiTextPos.x, coinGuiTextPos.y, coinGuiText);
}

if (global.debugModeEnabled)
{
	//DrawNineTile(RpgDialogueBack1_s, new Vec2(0, 0), new Vec2(window_mouse_get_x(), window_mouse_get_y()), c_white, 1, 2);
}

