using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Quit : MonoBehaviour // function for clicking quit button
{

     public void PlayGame()
     {
          SceneManager.LoadScene(0); // load main menu
     }


 }