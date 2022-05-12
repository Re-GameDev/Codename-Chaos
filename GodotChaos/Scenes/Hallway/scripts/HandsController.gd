extends Spatial

var animTime = 0;
var player = null;
var passedHalfAnimTime = false;
var sawResetting = false;
var rightTransformBeforeResetting = null;
var leftTransformBeforeResetting = null;

export var WalkingOscillationPeriod = 1; #seconds
export var RunningOscillationPeriod = 0.5; #seconds

func _ready():
#
	player = get_parent().get_parent();
#

func EaseQuadraticInOut(p):
#
	if (p < 0.5):
	#
		return 2 * p * p;
	#
	else:
	#
		return (-2 * p * p) + (4 * p) - 1;
	#
#

func _process(delta):
#
	if (player.get_reset_timer() > 0):
	#
		if (!sawResetting):
		#
			rightTransformBeforeResetting = $RightHand.transform;
			leftTransformBeforeResetting = $LeftHand.transform;
			sawResetting = true;
		#
		$RightHand.transform = rightTransformBeforeResetting.interpolate_with($RightHandPos3.transform, EaseQuadraticInOut(player.get_reset_timer()));
		$LeftHand.transform = leftTransformBeforeResetting.interpolate_with($LeftHandPos3.transform, EaseQuadraticInOut(player.get_reset_timer()));
	#
	else:
	#
		var playerVelocity = player.get("velocity");
		var isWalking = (playerVelocity.length() > 0.1);
		var runPercent = player.get_run_percent()
		if (isWalking):
		#
			var oscillationPeriod = lerp(WalkingOscillationPeriod, RunningOscillationPeriod, runPercent);
			animTime += delta / oscillationPeriod;
			if (animTime >= 0.5): passedHalfAnimTime = true;
			if (animTime >= 1.0): animTime -= 1.0;
			var leftAnimTime = 0;
			if (passedHalfAnimTime): leftAnimTime = animTime + 0.5;
			if (leftAnimTime >= 1.0): leftAnimTime -= 1.0;
			var lerpValue = 0;
			if (animTime < 0.5): lerpValue = EaseQuadraticInOut(animTime * 2);
			else: lerpValue = EaseQuadraticInOut(1 - ((animTime-0.5) * 2));
			var leftLerpValue = 0;
			if (leftAnimTime < 0.5): leftLerpValue = EaseQuadraticInOut(leftAnimTime * 2);
			else: leftLerpValue = EaseQuadraticInOut(1 - ((leftAnimTime-0.5) * 2));
			$RightHand.transform = $RightHandPos1.transform.interpolate_with($RightHandPos2.transform, lerpValue * (0.5 + runPercent*0.5));
			$LeftHand.transform = $LeftHandPos1.transform.interpolate_with($LeftHandPos2.transform, leftLerpValue * (0.5 + runPercent*0.5));
		#
		else:
		#
			animTime = 0;
			passedHalfAnimTime = false;
			$RightHand.transform = $RightHand.transform.interpolate_with($RightHandPos1.transform, 0.1);
			$LeftHand.transform = $LeftHand.transform.interpolate_with($LeftHandPos1.transform, 0.1);
		#
	#
#
