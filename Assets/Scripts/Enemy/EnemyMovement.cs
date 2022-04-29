using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private bool isPatrolling; // Boolean variable to store whether the enemy is currently patrolling.

    [SerializeField] private float patrolDist; // Variable to store the distance the enemy patrols.

    private Vector2 startingPos; // Variable to store the starting position of the enemy.

    [SerializeField] private float moveSpeed; // Variable to store the movement speed of the enemy.

    private Rigidbody2D body; // A reference to the RigidBody2D component in Unity which stores this information into variable body.

    void Start()
    {
        body = GetComponent<Rigidbody2D>(); // Stores the RigidBody2D component from the game object.

        startingPos = transform.position;
        isPatrolling = true;
    }

    void Update()
    {

        if(isPatrolling)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if(Mathf.Abs(startingPos.x - transform.position.x) > patrolDist)
        {
            Flip();
        }

        body.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, body.velocity.y);
    }

    private void Flip()
    {
        isPatrolling = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        moveSpeed *= -1;
        isPatrolling = true;
    }
}
