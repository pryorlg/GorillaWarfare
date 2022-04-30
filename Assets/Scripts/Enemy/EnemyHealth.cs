using UnityEngine;

public class EnemyHealth : MonoBehaviour
{    
    [SerializeField] private int maxLives; // Variable to store the maximum enemy health.
    private Animator anim; // Varaible to store the Animator component information.
    [SerializeField] private int currLives; // Variable to store the current enemy health.

    void Start()
    {
        anim = GetComponent<Animator>(); // Stores the Animator component from the game object.

        currLives = maxLives; // Set the current enemy lives to the maximum enemy lives.
    }

    void Update()
    {
        // If the enemy object's current lives is less than or equal to 0
        // trigger the death animation and call the removeEnemy function
        // after 1 second.
        if(currLives <= 0)
        {
            anim.SetTrigger("death");
            Invoke(nameof(removeEnemy), 1f);
        }
    }

    // Function to allow the enemy to take damage with an integer input called
    // damage. We set the current lives equal to the current lives minus the damage.
    public void takeDamage(int damage)
    {
        currLives -= damage;
    }

    // Returns whether the enemy is alive by checking the current lives.
    public bool isAlive()
    {
        return currLives > 0;
    }

    // Removes the game object from the scene using Destroy()
    private void removeEnemy()
    {
        Destroy(gameObject);
    }
}
