using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxLives, currLives; // Variables to store the maximum and current player health.

    private PlayerMovement playerMovement; // Variable that allows access to the PlayerMovement script functions.

    private Vector2 respawnPos; // Variable to store the respawn position of the player.
    
    private Animator anim; // Varaible to store the Animator component information.

    void Start()
    {
        anim = GetComponent<Animator>(); // Stores the Animator component from the game object.
        playerMovement = GetComponent<PlayerMovement>(); // Stores the PlayerMovement components from the PlayerMovement script 

        respawnPos = transform.position; // Store the starting position of the player as the respawn postion.
        
        currLives = maxLives; // Set the current player lives to the maximum player lives.
    }

    void Update()
    {
        if(currLives <= 0)
        {
            removePlayer();
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
        Invoke(nameof(startMove), 0.2f);
    }

    // Removes the game object from the scene using Destroy()
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
