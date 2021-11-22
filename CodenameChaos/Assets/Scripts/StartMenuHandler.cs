using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class StartMenuHandler : MonoBehaviour
{
    public bool StartMenuOpen = false;
    public Canvas StartMenuCanvas;

    private float oldTimeScale = 0;

    void Start()
    {
        Assert.IsNotNull(StartMenuCanvas);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartMenuOpen = !StartMenuOpen;
            StartMenuCanvas.gameObject.SetActive(StartMenuOpen);
            if (StartMenuOpen)
            {
                oldTimeScale = Time.timeScale;
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = oldTimeScale;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) && StartMenuOpen)
        {
            StartMenuOpen = false;
            Time.timeScale = oldTimeScale;
            SceneManager.LoadScene(0, LoadSceneMode.Single);
            
        }
    }
}
