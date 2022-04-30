using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   bool gameHasEnded = false; // see if game has ended

   public float restartDelay = 1f; // delay load of scene by 1

   private PlayerMovement playerMovement; // Variable that allows access to the PlayerMovement script functions.
   private PlayerHealth playerHealth;     // Variable that allows access to the PlayerHealth script functions.
   
   public GameObject CompleteLevelUI;



 

   void Start()
    {
         playerHealth = GetComponent<PlayerHealth>();
         playerMovement = GetComponent<PlayerMovement>();
         
         
    }

    private void OnCollisionEnter2D(Collision2D col) // built in unity function that checks collision
    {
        Debug.Log(col.gameObject);  
        // if(col.collider.CompareTag("Player")) // checks player tag
        // {
          
        // }
    }
  public void CompleteLevel() {
       CompleteLevelUI.SetActive(true); 
   }

   public void Death_to_Scene() { // if player is dead transitions to new scene
       if (!playerHealth.isAlive())
       {
           SceneManager.LoadScene(2); // Loads the Game Over screen
       }
   }

   public void EndGame() {
       if (gameHasEnded == false) {
           gameHasEnded = true;

           playerMovement.canMove = false; // player cant move when game is over
           Invoke("Restart", restartDelay); //call the function Restart
       }
   }

   void Restart () {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name); // initalize a new scene
   }

   
}
