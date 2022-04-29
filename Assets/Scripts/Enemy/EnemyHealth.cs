using UnityEngine;

public class EnemyHealth : MonoBehaviour
{    
    [SerializeField] private int enemyMaxLives; // Variable to store the maximum enemy health.
    private int enemyCurrLives; // Variable to store the current enemy health.

    void Start()
    {
        // Set the current enemy lives to the maximum enemy lives.
        enemyCurrLives = enemyMaxLives;
    }

    void Update()
    {
        // If the enemy object's current lives is less than or equal to 0
        // then destory the enemy game object.
        if(enemyCurrLives <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Function to allow the enemy to take damage with an integer input called
    // damage. We take away the damage integer from the current lives of the enemy.
    public void takeDamage(int damage)
    {
        enemyCurrLives -= damage;
    }
}
