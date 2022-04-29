using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform, player2Transform; // Variables to store the player 1 and 2 transform components

    private float cameraOffsetX, cameraOffsetY; // Variables to store the offset values for the camera's direction

    private bool twoPlayers; // Variable to store whether or not there are two players.

    private float cameraPosX, cameraPosY, cameraPosZ; // Variables to store the camera transform's position

    private float playerPosX, playerPosY; // Variables to store player 1 transform's position

    private float player2PosX, player2PosY; // Variables to store player 2 transform's position

    private float midpointX, midpointY; // Variables to store the midpoints between the players

    private float maxDistX, maxDistY; // Variables to store the max distances the players can separate before camera movement stops

    void Start()
    {   
        playerTransform = GameObject.Find("Player").transform; // Set the game object with the tag "Player" to the playerTransform
        
        playerAmt(); // Checks the number of players present in the level

        cameraOffsetX = 4; // Set the offset value for the camera's X direction
        cameraOffsetY = 2; // Set the offset value for the camera's Y direction

        maxDistX = 17; // Set the maximum x separation distance
        maxDistY = 7; // Set the maximum y separation distance
    }



    // LateUpdate is used here as it is called after Update() and FixedUpdated(). This allows for the computation
    // of player physics and movement to be completed before moving the camera, resulting in more precise camera
    // movement.
    void LateUpdate()
    {

        // Store the camera's X, Y, and Z coordinates
        cameraPosX = transform.position.x;
        cameraPosY = transform.position.y;
        cameraPosZ = transform.position.z;
        
        MoveCamera(); // Move the camera
    }

    private void MoveCamera()
    {
        // Get player 1's x postion
        playerPosX = playerTransform.position.x;
        
        // Get player 1's y postion
        playerPosY = playerTransform.position.y;

        // If there is only one player, move the camera only when the player nears the edge with a distance
        // denoted by the cameraOffsetX
        if(!twoPlayers)
        {
            // If the difference between the player 1's x position and the camera's x position is greater
            // than the desired x offset, then we begin moving the camera to the right.
            if(playerPosX - cameraPosX  > cameraOffsetX)
            {
                // Movement is done by updating the camera's transform.postion to a new vector 3, chaning only the x position.
                transform.position = new Vector3(playerTransform.position.x - cameraOffsetX, transform.position.y, transform.position.z);
            }

            // If the difference between the player 1's x position and the camera's x position is less
            // than the negative value of the desired x offset, then we begin moving the camera left.
            if(playerPosX - cameraPosX < -cameraOffsetX)
            {
                transform.position = new Vector3(playerTransform.position.x + cameraOffsetX, transform.position.y, transform.position.z);
            }

            // If the difference between the player 1's y position and the camera's y position is greater
            // than the desired y offset, then we begin moving the camera up.
            if(playerPosY - cameraPosY  > cameraOffsetY)
            {
                // Movement is done by updating the transform.postion to a new vector 3, chaning only the y position.
                transform.position = new Vector3(transform.position.x, playerTransform.position.y - cameraOffsetY, transform.position.z);
            }

            // If the difference between the player 1's y position and the camera's y position is less
            // than the negative value of the desired y offset, then we begin moving the camera down.
            if(playerPosY - cameraPosY < -cameraOffsetY)
            {
                transform.position = new Vector3(transform.position.x, playerTransform.position.y + cameraOffsetY, transform.position.z);
            }
        }

        // If there are two players
        else
        {
            // Get player 2's x position
            player2PosX = player2Transform.position.x;

            // Get player 2's y position
            player2PosY = player2Transform.position.y;
            
            // Get the X midpoint between the two player objects
            midpointX = (playerPosX + player2PosX)/2f;
            
            // Get the Y midpoint between the two player objects
            midpointY = (playerPosY + player2PosY)/2f;


            // Check if the players are further away from eachother than the maximum
            // desired distance on the X axis.
            if(Mathf.Abs(playerPosX - player2PosX) > maxDistX)
            {
                // If the players are also at the maximum Y distance, freeze the 
                // camera in all positions.
                if(Mathf.Abs(playerPosY - player2PosY) > maxDistY)
                {
                    transform.position = new Vector3(cameraPosX, cameraPosY, cameraPosZ);
                }
                
                // Else, if the players are not at the maximum Y distance, continue 
                // updating the Y position of the camera while freezing the X position.
                else
                {
                    transform.position = new Vector3(cameraPosX, midpointY, cameraPosZ);
                }
            }

            // Check if the players are further away from eachother than the maximum
            // desired distance on the Y axis.
            else if(Mathf.Abs(playerPosY - player2PosY) > maxDistY)
            {
                // If the players are also at the maximum X distance, freeze the 
                // camera in all positions.
                if(Mathf.Abs(playerPosX - player2PosX) > maxDistX)
                {
                    transform.position = new Vector3(cameraPosX, cameraPosY, cameraPosZ);
                }

                // Else, if the players are not at the maximum X distance, continue 
                // updating the X position of the camera while freezing the Y position.
                else
                {
                    transform.position = new Vector3(midpointX, cameraPosY, cameraPosZ);
                }
            }

            // Else, if the players are not at the maximum distance, continue updating
            // the camera position based off of the midpoints between the players.
            else
            {
                transform.position = new Vector3(midpointX, midpointY, cameraPosZ);
            }
            
        }
    }

    // Checks the number of players present in the level
    private void playerAmt()
    {
        // Check if a player 2 game object is present
        if(GameObject.Find("Player2") != null)
        {
            twoPlayers = true;
            player2Transform = GameObject.Find("Player2").transform; // Set the game object with the tag "Player2" to the player2Transform
        }

        else
        {
            twoPlayers = false;
        }
    }
}
