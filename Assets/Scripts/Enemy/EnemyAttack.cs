using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Animator anim; // Varaible to store the Animator component information.
    private EnemyMovement enemyMovement; // Variable that allows access to the EnemyMovement script functions.
    private BoxCollider2D boxCollider; // Variable to store the boxCollider component information.

    [SerializeField] private LayerMask playerLayer; // Variable to store the playerLayer layer information.

    private void Awake()
    {
        anim = GetComponent<Animator>(); // Stores the Animator component from the game object.
        enemyMovement = GetComponent<EnemyMovement>(); // Stores the EnemyMovement components from the EnemyMovement script
        boxCollider = GetComponent<BoxCollider2D>(); // Stores the BoxCollider2D component from the game object.
    }

    void Update()
    {


        
        Debug.DrawRay(new Vector2(boxCollider.bounds.center.x + (boxCollider.bounds.extents.x * -transform.localScale.x), boxCollider.bounds.center.y), (Vector2.right*-transform.localScale.x) * 0.5f, Color.red);
    }

    private bool inRange()
    {
        RaycastHit2D raycastHitPlayer = Physics2D.Raycast(
            new Vector2(boxCollider.bounds.center.x + (boxCollider.bounds.extents.x * -transform.localScale.x), 
                        boxCollider.bounds.center.y), 
                        (Vector2.right*-transform.localScale.x), 
                        0.5f, 
                        playerLayer);

        return raycastHitPlayer.collider != null;
    }
}
