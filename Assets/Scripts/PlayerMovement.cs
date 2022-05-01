using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body; // A reference to the RigidBody2D component in Unity which stores this information into variable body.
    private Animator anim; // Varaible to store the Animator component information.
    private BoxCollider2D boxCollider; // Variable to store the boxCollider component information.
    [SerializeField] private LayerMask groundLayer; // Varaible to store the groundLayer layer information.
    [SerializeField] private LayerMask wallLayer; // Variable to store the wallLayer layer information.
    [SerializeField] private float speed; // A variable to create our desired movement speed. SerializeField is used to
                                          // allow editing of this variable directly from Unity.
    public bool canMove; // Stores whether the player can move or not.
    [SerializeField] private float jumpPower; // Variable to create desired jump power.
    [SerializeField] private float wallJumpPowerX; // A serialized float to store the horizontal force applied when wall jumping.
    [SerializeField] private float wallJumpPowerY; // A serialized float to store the verticle force applied when wall jumping.

    private float horizontalInput; // Float variable to store the horizontal input of the player.
    private bool jumpInput; // Boolean variable to store whether the 'jump' input key has been pressed.

    int idleTimeSetting = 60; // Variable to check player's idle time in seconds.
    float lastIdleTime; // Variable to store the last time the player made any input.

    private float lastGroundTime ; // Stores the last game time that the player was on the ground.
    private float lastAirTime; // Stores the last game time that the player was in the air.
    private float lastWallTime; // Stores the last game time that the player was on a wall.

    private bool wallJumping; // Stores whether the player is currently wall jumping. 
    private float lastWall; // A float variable storing the last wall the character was on.

    private PlayerHealth playerHealth;

    private void Awake(){
        body = GetComponent<Rigidbody2D>(); // Stores the RigidBody2D component from the game object.
        anim = GetComponent<Animator>(); // Stores the Animator component from the game object.
        boxCollider = GetComponent<BoxCollider2D>(); // Stores the BoxCollider2D component from the game object.

        playerHealth = GetComponent<PlayerHealth>(); // Stores the PlayerHealth script componenets.

        lastIdleTime = Time.time;
        canMove = true;
    }

    // The update method is called once every frame, meaning it is dependent on frames per second. It is better
    // to check for player input and check animator conditions in Update over FixedUpdate for this reason. 
    private void Update()
    {
        // Check inputs for player 1
        if(gameObject.name == "Player" && playerHealth.isAlive())
        {
            // Store the player 1 input along the horizontal axis (A & D) into horizontalInput. Values range from -1 to 1.
            horizontalInput = Input.GetAxis("Horizontal");
            
            // Store the player 1 input of the desired jump keybind (W) into the boolean variable jumpInput.
            jumpInput = Input.GetKey(KeyCode.W);
        }

        // Check inputs for player 2
        if(gameObject.name == "Player2" && playerHealth.isAlive())
        {
            // Store the player 2 input along the horizontal axis (LeftArrow & RightArrow) into horizontalInput. Values range from -1 to 1.
            horizontalInput = Input.GetAxis("Horizontal2");

            // Store the player 2 input of the desired jump keybind (UpArrow) into the boolean variable jumpInput.
            jumpInput = Input.GetKey(KeyCode.UpArrow);
        }



        // Set the boolean parameter "run" to True when the player is not moving (horizontalInput == 0)
        // and to False when the player is moving (horizontalInput == 1).
        anim.SetBool("run", horizontalInput != 0 && !wallSliding());

        // Set our boolean parameter "grounded" to the result of isGrounded.
        anim.SetBool("grounded", isGrounded());
        
        // If the player is wallSliding, the horizontal input is to the right, and the player is not wall jumping,
        // set the animation boolean value wallSlideLeft to false and set the animation boolean value of wallSlide
        // to the value of wallSliding (true).
        if(wallSliding() && horizontalInput > 0)
        {
            anim.SetBool("wallSlide", false);
            anim.SetBool("wallSlide", wallSliding());
        }

        // Same as the above if statement but this time checking for left input and inverting the wallSlideLeft
        // and wallSlide animator boolean values.
        if(wallSliding() && horizontalInput < 0)
        {
            anim.SetBool("wallSlide", false);
            anim.SetBool("wallSlideLeft", wallSliding());
        }

        // If the player is on the wall but not wall sliding, we will set the animator trigger jump and set the
        // animator wall slide and run boolean values to false.
        if(!wallSliding() && onWall() && !isGrounded())
        {
            anim.SetBool("wallSlide", false);
            anim.SetBool("wallSlideLeft", false);
            anim.SetTrigger("jump");
        }

        // Stores the time at which the most recent input occured in LastIdleTime
        if(Input.anyKey)
        {
            lastIdleTime = Time.time;
        }

        // Set the boolean value of paramter "AFK" to the result of idleCheck
        anim.SetBool("AFK", idleCheck());

        // Check if the character is not grounded, not on a wall, and no horizontal input.
        // If true, we trigger the idle jump animation.
        if(!isGrounded() && !onWall() && horizontalInput == 0)
        {
            anim.SetTrigger("jump");
        }

        // Check if the character is not grounded, not on a wall, and there is horizontal input.
        // If true, we trigger the moving jump animation.
        if(!isGrounded() && !onWall() && horizontalInput != 0)
        {
            anim.SetTrigger("jumpMove");
        }

        // Set the animator boolean variable of wallJump to the value of wallJumping. 
        anim.SetBool("wallJump", wallJumping);

        // Draw the rays visualizing how isGrounded() works. Color set to red.
        Debug.DrawRay(new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.center.y-boxCollider.bounds.extents.y), Vector2.down * 0.1f , Color.red);
        Debug.DrawRay(new Vector2(boxCollider.bounds.center.x + boxCollider.bounds.extents.x/1.35f, boxCollider.bounds.center.y-boxCollider.bounds.extents.y), Vector2.down * 0.1f, Color.red);
        Debug.DrawRay(new Vector2(boxCollider.bounds.center.x - boxCollider.bounds.extents.x/1.35f, boxCollider.bounds.center.y-boxCollider.bounds.extents.y), Vector2.down * 0.1f, Color.red);

        // Draw the rays visualizing how onWall() works. Color set to blue.
        Debug.DrawRay(new Vector2(boxCollider.bounds.center.x + (boxCollider.bounds.extents.x * transform.localScale.x), boxCollider.bounds.center.y), (Vector2.right*transform.localScale.x) * 0.1f, Color.blue);
        Debug.DrawRay(new Vector2(boxCollider.bounds.center.x + (boxCollider.bounds.extents.x * transform.localScale.x), boxCollider.bounds.center.y-boxCollider.bounds.extents.y/1.5f), (Vector2.right*transform.localScale.x) * 0.1f, Color.blue);
    }
    
    // The fixed update method runs independent of frames per second, iterating its time based on the execution time of the
    // program (fixedTimeStep). It is for this reason that physics modifications, such as velocity and collider changes, are  
    // made here instead of update. This allows more consistent results independent of framerate which can vary based on system 
    // hardware, platform, etc. 
    private void FixedUpdate()
    {
         // We check that the player is not currently wall jumping as different forces are applied if so.
        if(!wallJumping)
        {
            // body.velocity changes how fast our player is moving.
            // A vector is used to assign this speed. The first coordinate uses input from the keyboard on the "horizontal"
            // axis. This sets the vector coordinate to "-1" whenever "A" is pressed and "1" whenever "D" is pressed. This
            // handles the movement from left to right. We then multiply this by our speed in order to achieve the desired
            // movement speed. The second coordinate is set to the default body velocity as we do not want verticle movement.
            body.velocity = new Vector2(horizontalInput * speed,body.velocity.y);
        }  
        
        // If our boolean variable canMove is set to false, we stop the player from moving by setting the velocity
        // to zero for x and y.
        if(!canMove)
        {
            body.velocity = Vector2.zero;
        }

       // If statement to check if the player has pressed the 'spacebar' key to jump. 
        if(jumpInput)
        {
            Jump();
            
            // If spacebar has been input, the player is wall sliding, and the horizontal input is to the right,
            // we set the walljumping value to true, set the boolean animation for walljump, apply an x velocity
            // to the left and apply an upwards velocity. We store the transform's x scale to get the last direction
            // the player was facing and save this as last wall. Then we flip our character's scale in order to face
            // them left. After .25 seconds, we call the wallJumpFalse function.
            if(wallSliding() && horizontalInput > 0 && lastWall != 1){
                wallJumping = true;
                
                lastWall = transform.localScale.x;

                // Below Mathf.Clamp to clamp the horizontal velocity between the original speed value and an arbitrary
                // maximum value. This helps ensure smooth transitions between x velocity applied during wall jumping
                // and the default x velocity value (speed). 
                body.velocity = new Vector2(-Mathf.Clamp(wallJumpPowerX, speed, Mathf.Infinity), wallJumpPowerY);
                transform.localScale = new Vector3(-1, 1, 1);
                
                Invoke(nameof(wallJumpFalse), 0.25f);
            }
            
            // Same as the previous if statement but this time checking if the horizontal input is to the left,
            // applying the x velocity to the right, and flipping the scale to the right.
            if(wallSliding() && horizontalInput < 0 && lastWall != -1){
                wallJumping = true;
                
                lastWall = transform.localScale.x;

                body.velocity = new Vector2(Mathf.Clamp(wallJumpPowerX, speed, Mathf.Infinity), wallJumpPowerY);
                transform.localScale = new Vector3(1, 1, 1);
                
                Invoke(nameof(wallJumpFalse), 0.25f);
            }
        }
        
        // Face the player to the right when pressing "D" and the player is not wall jumping.
        // We check for wall jumping as an inverse scale is applied when wall jumping is true.
        if(horizontalInput > 0 && !wallJumping)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Face the player to the left when pressing "A" and the player is not wall jumping.
        else if(horizontalInput < 0 && !wallJumping)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Sets the last wall value to a neutral value and stores last time the player was grounded.
        if(isGrounded())
        {
            lastWall = 0;
            lastGroundTime = Time.time;
        }

        // Stores the last time the player was in the air.
        if(!isGrounded() && !onWall())
        {
            lastAirTime = Time.time;
        }

        // Stores the last time the player was on a wall.        
        if(onWall())
        {
            lastWallTime = Time.time;
        }

        
        // Checks if the player is wall sliding. If true, we maintain the same x velocity and then we
        // invert the y velocity and divide it by 4. This ensures the player slides down and that 
        // they slide slower than they fall.
        if(wallSliding())
        {
            body.velocity = new Vector2(body.velocity.x, -speed/3);
        }
    }

    private void Jump()
    {
        if(canJump())
        {
        // This checks if the player is grounded, only allowing jump input if the player is on the ground and 
        // canJump() returns true. When true, the player is given a speed in the verticle (y-axis) direction.
        body.velocity = Vector2.up * jumpPower;
        }
            
    }

    private bool canJump()
    {
        return isGrounded() && lastGroundTime - lastAirTime > 0.1f && playerHealth.isAlive(); 
    }

    // Here we use the Physics2D Raycast method to cast 3 rays downwards to check if the player is grounded.
    // The rays are given a Vector2 origin point which here is set to the bottom of the box collider in 3 
    // different locations - the center, right edge divided by 1.35, and left edge divided by 1.35. Then the
    // rays are given a direction to be cast, in this case a Vector2 signifying down. Next is a distance
    // to cast the ray, set to 0.1f. The last argument is a layer to detect, here set to ground layer.
    // The results of these raycasts are stored into 3 RaycastHit2D variables which return 'null' when
    // no hits are detected and non-null when hits are detected.
    // The rays can be visualized inside of unity using Debug.DrawRay
    private bool isGrounded()
    {
        RaycastHit2D raycastHitM = Physics2D.Raycast(
            new Vector2(boxCollider.bounds.center.x, 
                        boxCollider.bounds.center.y - boxCollider.bounds.extents.y), 
                        Vector2.down, 
                        0.1f, 
                        groundLayer);

        RaycastHit2D raycastHitR = Physics2D.Raycast(
            new Vector2(boxCollider.bounds.center.x + boxCollider.bounds.extents.x/1.35f, 
                        boxCollider.bounds.center.y - boxCollider.bounds.extents.y), 
                        Vector2.down, 
                        0.1f, 
                        groundLayer);

            RaycastHit2D raycastHitL = Physics2D.Raycast(
            new Vector2(boxCollider.bounds.center.x - boxCollider.bounds.extents.x/1.35f, 
                        boxCollider.bounds.center.y - boxCollider.bounds.extents.y),
                        Vector2.down, 
                        0.1f, 
                        groundLayer);

        return raycastHitM.collider != null || raycastHitR.collider != null || raycastHitL.collider != null;
    }

    // Here we use the same method as before to check if the player is on a wall, only this time the direction we check
    // is on the direction the player is facing (transform.localScale.x) and we look for a wall layer instead. 
    private bool onWall()
    {
        RaycastHit2D raycastHitM = Physics2D.Raycast(
            new Vector2(boxCollider.bounds.center.x + (boxCollider.bounds.extents.x * transform.localScale.x), 
                        boxCollider.bounds.center.y), 
                        (Vector2.right*transform.localScale.x), 
                        0.1f, 
                        wallLayer);

        RaycastHit2D raycastHitB = Physics2D.Raycast(
            new Vector2(boxCollider.bounds.center.x + (boxCollider.bounds.extents.x * transform.localScale.x) - boxCollider.bounds.extents.x/1.25f, 
                        boxCollider.bounds.center.y + boxCollider.bounds.extents.y/2f),
                        (Vector2.right*transform.localScale.x), 
                        0.1f, 
                        wallLayer);

        return raycastHitM.collider != null || raycastHitB.collider != null;
    }

    private bool wallSliding()
    {
        return onWall() && !isGrounded() && horizontalInput != 0 && !wallJumping;
    }

    private void wallJumpFalse()
    {
        wallJumping = false;
    }
    
    private bool idleCheck()
    {
        return Time.time - lastIdleTime > idleTimeSetting;
    }
    
    // Checks whether the player can attack by ensuring the postion is on the ground and not on a wall.
    public bool canAttack()
    {
        return isGrounded() && !onWall() && playerHealth.isAlive();
    }
}
