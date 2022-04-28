// Creation Date: April 27 2022
// Author(s): Shawn Featherly (Moved to separate file by Taylor Robbins)

using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// copied from http://answers.unity3d.com/questions/242794/inspector-field-for-scene-asset.html#answer-1204071
/// Alternative is https://docs.unity3d.com/ScriptReference/SceneAsset.html
/// </summary>
[System.Serializable]
public class SceneField
{
#if UNITY_EDITOR
#pragma warning disable 414 // Supresses "The private field `SceneField.m_SceneAsset' is assigned but its value is never used." Is used by the PropertyDrawer using string reference
	[SerializeField]
	private Object _sceneAsset;
#pragma warning restore 414
#endif
	[SerializeField]
	private string _sceneName = "";
	public string SceneName
	{
		get { return _sceneName; }
		set { SetByName(value); }
	}

	// makes it work with the existing Unity methods (LoadLevel/LoadScene)
	public static implicit operator string(SceneField sceneField)
	{
		return ((sceneField == null) ? null : sceneField.SceneName);
	}

	private void SetByName(string sceneName)
	{
#if UNITY_EDITOR
		string scenePath = UnityEditor.EditorBuildSettings.scenes
			.Select(s => s.path)
			.FirstOrDefault(s => s.EndsWith(sceneName + ".unity"));
		_sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
#endif
		_sceneName = sceneName;
	}
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SceneField))]
public class SceneFieldPropertyDrawer : PropertyDrawer
{
	public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
	{
		EditorGUI.BeginProperty(_position, GUIContent.none, _property);
		SerializedProperty sceneAsset = _property.FindPropertyRelative("_sceneAsset");
		SerializedProperty sceneName = _property.FindPropertyRelative("_sceneName");
		_position = EditorGUI.PrefixLabel(_position, GUIUtility.GetControlID(FocusType.Passive), _label);
		if (sceneAsset != null)
		{
			EditorGUI.BeginChangeCheck();
			Object value = EditorGUI.ObjectField(_position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false);
			if (EditorGUI.EndChangeCheck())
			{
				sceneAsset.objectReferenceValue = value;
				if (sceneAsset.objectReferenceValue != null)
				{
					sceneName.stringValue = (sceneAsset.objectReferenceValue as SceneAsset).name;
				}
				else
				{
					sceneName.stringValue = "";
				}
			}
		}
		EditorGUI.EndProperty();
	}
}
#endif
