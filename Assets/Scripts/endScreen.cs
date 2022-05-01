using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endScreen : MonoBehaviour
{
   public void PlayGame()
     {
          SceneManager.LoadScene(0); // load level 1
     }
}
