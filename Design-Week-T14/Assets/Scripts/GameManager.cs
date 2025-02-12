using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public void RandomLevel()
    {
        if ((Random.Range(1, 3)) == 1)
        {
            LoadSceneLevel();
        }
        else if ((Random.Range(1, 3)) == 2)
        {
            LoadSceneLevel2();
        }
    }

    public void LoadSceneLowFidPrototype()
    {
        SceneManager.LoadScene("Scenes/LowFidelityPrototype");
        Time.timeScale = 1;
    }
    public void LoadSceneHomeScreen()
    {
        SceneManager.LoadScene("Scenes/HomeScreen");
    }
    public void LoadSceneLevel()
    {
        SceneManager.LoadScene("Scenes/Level");
        Time.timeScale = 1;
    }
    public void LoadSceneLevel2()
    {
        SceneManager.LoadScene("Scenes/Level 2");
        Time.timeScale = 1;
    }
    public void LoadSceneLevelSelect()
    {
        SceneManager.LoadScene("Scenes/Level Select");
        Time.timeScale = 1;
    }
    public void Quitgame()
    {
        Application.Quit();
    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadSceneLowFidPrototype();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadSceneHomeScreen();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LoadSceneLevel();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            LoadSceneLevel2();
        }

        if(Input.GetKeyDown(KeyCode.Delete))
        {
            Quitgame();
        }

        
    }

}
