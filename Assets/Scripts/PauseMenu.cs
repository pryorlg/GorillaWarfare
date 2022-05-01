using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{


     public static bool GameIsPaused = false;

     public GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
         {
               if (GameIsPaused == true)
               {
                    Resume();
               }
               else
               {
                    Pause();
               }
         }
    }


     public void Resume()
     {
          PauseMenuUI.SetActive(false);
          Time.timeScale = 1;
          GameIsPaused = false;
     }

     void Pause()
     {
          PauseMenuUI.SetActive(true);
          Time.timeScale = 0;
          GameIsPaused = true;
     }

     public void LoadMenu()
     {
          Debug.Log("Loading Game...");
          Time.timeScale = 1;
          SceneManager.LoadScene(0);
     }

     public void QuitGame()
     {
          Debug.Log("Quit Game....");
          Application.Quit();
     }


}
