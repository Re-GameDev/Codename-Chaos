using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
#endif

public class LoadsScene : MonoBehaviour
{
    [ContextMenuItem("Now()", "LoadScene Now")]
    public SceneField Scene;

    [Tooltip("When set, automatically updates this text field and this game objects name to match the filename of the Scene")]
    public TMPro.TMP_Text DisplayOnUi;

    private UnityEngine.UI.Button button
    {
        get
        {
            if (_button == null)
            {
                _button = this.GetComponent<UnityEngine.UI.Button>();
            }
            return _button;
        }
    }
    private UnityEngine.UI.Button _button;

    /// <summary> Immediately loads the Unity scene </summary>
    public void Now()
    {
        if (string.IsNullOrEmpty(Scene))
        {
            Debug.LogError(this.name + " has no Scene for LoadsScene component");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(Scene);
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (false == string.IsNullOrEmpty(Scene))
        {
            // add new OnClick event
            if (button.onClick.GetPersistentEventCount() == 0)
            {
                UnityEventTools.AddPersistentListener(button.onClick, Now);
            }

            // update existing OnClick event
            else if (button.onClick.GetPersistentTarget(0) == this)
            {
                UnityEventTools.RegisterPersistentListener(button.onClick, 0, Now);
            }

            // set button text
            if (DisplayOnUi)
            {
                DisplayOnUi.text = ObjectNames.NicifyVariableName(Scene.SceneName);
                gameObject.name = ObjectNames.NicifyVariableName(Scene.SceneName);
            }
        }
    }
#endif
}

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
    private Object m_SceneAsset;
#pragma warning restore 414
#endif
    [SerializeField]
    private string m_SceneName = "";
    public string SceneName
    {
        get { return m_SceneName; }
        set { setByName(value); }
    }

    // makes it work with the existing Unity methods (LoadLevel/LoadScene)
    public static implicit operator string(SceneField sceneField)
    {
        return sceneField == null ? null : sceneField.SceneName;
    }

    private void setByName(string sceneName)
    {
#if UNITY_EDITOR
        var scenePath = UnityEditor.EditorBuildSettings.scenes
            .Select(s => s.path)
            .FirstOrDefault(s => s.EndsWith(sceneName + ".unity"));
        m_SceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
#endif
        m_SceneName = sceneName;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SceneField))]
public class SceneFieldPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
    {
        EditorGUI.BeginProperty(_position, GUIContent.none, _property);
        SerializedProperty sceneAsset = _property.FindPropertyRelative("m_SceneAsset");
        SerializedProperty sceneName = _property.FindPropertyRelative("m_SceneName");
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
