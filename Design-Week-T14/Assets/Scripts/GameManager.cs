using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    

    public void LoadSceneLowFidPrototype()
    {
        SceneManager.LoadScene("Scenes/LowFidelityPrototype");
    }
    public void LoadScenePauseMenu()
    {
        SceneManager.LoadScene("Scenes/PauseMenu");
    }
    public void LoadSceneHowToPlay()
    {
        SceneManager.LoadScene("Scenes/HowToPlay");
    }
    public void LoadSceneHomeScreen()
    {
        SceneManager.LoadScene("Scenes/HomeScreen");
    }
    public void LoadSceneSampleScene()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadScenePauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadSceneLowFidPrototype();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadSceneHowToPlay();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadSceneHomeScreen();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LoadSceneSampleScene();
        }
    }

}
