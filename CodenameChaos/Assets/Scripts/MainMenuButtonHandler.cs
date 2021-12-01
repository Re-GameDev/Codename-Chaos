
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    public int SceneIndex_3dPlatformer = 0;
    public int SceneIndex_2dRunner = 0; 
    public int SceneIndex_Planet2d = 0; 
    const int SceneIndex_Rageball = 4; 

    public void On3dPlatformerButtonClick()
    {
        Debug.Log("Clicked on 3D Platformer");
        SceneManager.LoadScene(SceneIndex_3dPlatformer, LoadSceneMode.Single);
    }

    public void On2dRunnerButtonClick()
    {
        Debug.Log("Clicked on 2D Runner");
        SceneManager.LoadScene(SceneIndex_2dRunner, LoadSceneMode.Single);
    }

    public void OnPlanet2dButtonClick()
    {
        Debug.Log("Clicked on Planet 2D");
        SceneManager.LoadScene(SceneIndex_Planet2d, LoadSceneMode.Single);
    }

    public void OnRageBallButtonClick()
    {
        SceneManager.LoadScene( SceneIndex_Rageball );
    }

    /// <summary>
    /// Load the scene by build index
    /// </summary>
    /// <param name="sceneIndex"></param>
    public void UILoadScene( int sceneIndex )
    {
        SceneManager.LoadScene( sceneIndex );
    }
}
