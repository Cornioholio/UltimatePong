using UnityEngine;
using Unity.Netcode;

public class ScoreGoal :  NetworkBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            gameManager.OnGoalScored(gameObject);
        }
    }
}