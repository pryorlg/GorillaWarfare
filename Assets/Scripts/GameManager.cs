using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   bool gameHasEnded = false; // see if game has ended
   public float restartDelay = 1f; // delay load of scene by 1
   
   public GameObject victoryUI; // Game object to store the Victory UI

    void Update()
    {
        // Function to load game over screen after
        // player death.
        GameOver();
    }

    private void OnCollisionEnter2D(Collision2D col) // built in unity function that checks collision
    {
        // If the object colliding with the box collider has the tag "Player"
        // then we stop player movement and call the EndGame() function.
        if(col.collider.CompareTag("Player"))
        {
            col.collider.GetComponent<PlayerMovement>().canMove = false;
            EndGame();
        }
    }
    public void GameOver() { 
        // If there are two players
        if(GameObject.Find("Player2") != null)
        {
           // And if both the players are not alive
           if(!GameObject.Find("Player2").GetComponent<PlayerHealth>().isAlive()
            && !GameObject.Find("Player").GetComponent<PlayerHealth>().isAlive())
            {
                SceneManager.LoadScene(2); // Loads the Game Over screen
            }
        }

        // Else if there is only one player
        else{
            // If the player is not alive
           if(!GameObject.Find("Player").GetComponent<PlayerHealth>().isAlive())
           {
               SceneManager.LoadScene(2); // Loads the Game Over screen
           }
        }
    }

    // Here we handle the ending of a level.
    public void EndGame() {
        if (gameHasEnded == false) {
            gameHasEnded = true;

            SceneManager.LoadScene(3);
        }
    }
}
