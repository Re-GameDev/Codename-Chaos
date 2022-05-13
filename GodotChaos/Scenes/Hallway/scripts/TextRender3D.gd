extends Spatial

enum HoriTextAlign {
	Left,
	Center,
	Right
}
enum VertTextAlign {
	Top,
	Center,
	Bottom
}

export var Text = "Hello Godot!";
export var DrawScale = 1.0;
export var CharSpacing = 0.1
export(Material) var DrawMaterial = null;
export var HorizontalAlign = HoriTextAlign.Left;
export var VerticalAlign = VertTextAlign.Top;

var textModel = null;
var textNodes = [];

func _ready():
#
	textModel = ResourceLoader.load("Models/Text1.obj").instance(PackedScene.GEN_EDIT_STATE_DISABLED);
	textModel.transform = self.transform;
	Text = Text.replace("\\n", "\n");
	#print(textModel);
	$PlaceholderMesh.hide();
	CreateText();
#

func GetCharacter3D(character):
#
	assert(typeof(character) == TYPE_INT);
	var charHexStr = ("%02X" % character);
	#print("Format Result for ", character, ": ", charHexStr);
	for node in textModel.get_children():
	#
		#print(node);
		if (node.name.ends_with(charHexStr)):
		#
			return node;
		#
	#
	return null;
#

func DestroyText():
#
	for node in textNodes:
	#
		node.destroy();
	#
	textNodes.clear();
#

func CreateText():
#
	if (textNodes.size() > 0): DestroyText();
	var textPos = Vector3(0, 0, 0);
	var stepVector = Vector3(1, 0, 0); #self.transform.basis.y;
	for cIndex in Text.length():
	#
		var character = Text.ord_at(cIndex);
		if (character == ord(' ')):
		#
			textPos += 0.5 * DrawScale * stepVector;
		#
		elif (character == ord('\n')):
		#
			textPos.x = 0;
			textPos.z += 1;
		#
		else:
		#
			var character3D = GetCharacter3D(character);
			assert(character3D != null);
			var newNode = character3D.duplicate();
			newNode.transform.origin = textPos;
			newNode.transform = newNode.transform.scaled(Vector3(DrawScale, DrawScale, DrawScale));
			newNode.set_surface_material(0, DrawMaterial);
			add_child(newNode);
			textNodes.append(newNode);
			var charWidth = newNode.mesh.get_aabb().size.x;
			var stepSize = charWidth + (CharSpacing * DrawScale);
			textPos += stepSize * stepVector;
		#
	#
#

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
