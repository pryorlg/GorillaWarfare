
using UnityEngine;

public class VictoryZone : MonoBehaviour {

    public GameManager gameManager;

    void OnTriggerEnter() 
    {
       gameManager.CompleteLevel();
    }
}

