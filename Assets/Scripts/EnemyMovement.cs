using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool isMoving; // Boolean variable to store whether the enemy is currently moving.

    [SerializeField] private float moveDist; // Variable to store the distance the enemy moves.

    private EnemyHealth enemyHealth; // Variable allowing access to EnemyHealth script functions.

    private Vector2 startingPos; // Variable to store the starting position of the enemy.

    [SerializeField] private float moveSpeed; // Variable to store the movement speed of the enemy.

    private Rigidbody2D body; // A reference to the RigidBody2D component in Unity which stores this information into variable body.

    private Animator anim; // Varaible to store the Animator component information.

    void Start()
    {
        body = GetComponent<Rigidbody2D>(); // Stores the RigidBody2D component from the game object.
        enemyHealth = GetComponent<EnemyHealth>(); // Stores the EnemyHealth components from the script
        anim = GetComponent<Animator>(); // Stores the Animator component from the game object.

        startingPos = transform.position; // Stores the starting position of the enemy
        isMoving = true; // Initialize isMoving to true as all enemies start the scene moving. 
    }

    void Update()
    {
        // If the enemy is currently moving and is alive, call the Move() function.
        if(isMoving && enemyHealth.isAlive())
        {
            Move();
        }
    }

    private void Move()
    {
        // Set the boolean animator value to the value of isMoving()
        anim.SetBool("move", isMoving);
        
        // If the enemy has moved further from their starting position than the desired
        // move distance, then call the flip function.
        if(Mathf.Abs(startingPos.x - transform.position.x) > moveDist)
        {
            Flip();
        }

        // Set the velocity of the enemy by assigning a moveSpeed multiplied by 
        // Time.fixedDeltaTime to ensure physics calculations happen independent of
        // framerate.
        body.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, body.velocity.y);
    }

    private void Flip()
    {
        isMoving = false;
        
        // Flip the enemy by multiplying the scale by -1
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

        // Invert the speed by multiplying it by -1
        moveSpeed *= -1;

        isMoving = true;
    }
}
