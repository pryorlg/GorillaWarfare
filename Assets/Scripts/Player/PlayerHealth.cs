using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxLives, currLives; // Variables to store the maximum and current player health.

    public Image[] playerHearts; // An image array to store player 1 hearts remaining
    public Image[] player2Hearts; // An image array to store player 2 hearts remaining

    private PlayerMovement playerMovement; // Variable that allows access to the PlayerMovement script functions.

    private Vector2 respawnPos; // Variable to store the respawn position of the player.
    
    private Animator anim; // Varaible to store the Animator component information.

    void Start()
    {
        anim = GetComponent<Animator>(); // Stores the Animator component from the game object.
        playerMovement = GetComponent<PlayerMovement>(); // Stores the PlayerMovement components from the PlayerMovement script 

        respawnPos = transform.position; // Store the starting position of the player as the respawn postion.
        
        currLives = maxLives; // Set the current player lives to the maximum player lives.

        heartInit();
    }

    void Update()
    {
        // If the player is not alive, trigger the death animation and freeze player movement.
        if(!isAlive())
        {
            anim.SetTrigger("death");
            playerMovement.canMove = false;
        }
    }

    // Returns whether the player is alive by checking the current lives.
    public bool isAlive()
    {
        return currLives > 0;
    }

    // Function to allow the player to take damage with an integer input called
    // damage. We set the current lives equal to the current lives minus the damage.
    // We also set the animator boolean trigger "hit", and stop player movement momentarily.
    public void takeDamage(int damage)
    {
        anim.SetTrigger("hit");
        
        stopMove();

        currLives -= damage;

        if(currLives > 0 && gameObject.name == "Player")
        {
            playerHearts[currLives-1].enabled = false;
        }

        if(currLives > 0 && gameObject.name == "Player2")
        {
            player2Hearts[currLives-1].enabled = false;
        }
        
        Invoke(nameof(startMove), 0.2f);
    }

    private void heartInit()
    {
        if(GameObject.Find("Player2") == null)
        {
            for (int i = 0; i < player2Hearts.Length; i++)
            {
                player2Hearts[i].enabled = false;
            }
        }
    }

    // Sets the game object to inactive in the scene using SetActive(false)
    private void removePlayer()
    {
        gameObject.SetActive(false);
    }

    private void stopMove()
    {
        playerMovement.canMove = false;
    }

    private void startMove()
    {
        playerMovement.canMove = true;
    }
}
