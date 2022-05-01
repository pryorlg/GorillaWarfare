using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour // public function for pressing play button
{

     public void PlayGame()
     {
          SceneManager.LoadScene(1); // load level 1
     }


}