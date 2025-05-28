using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int leftPlayerScore;
    public int rightPlayerScore;

    public BallMovement ball;
    public GameObject leftGoal;
    public GameObject rightGoal;
   
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void OnGoalScored(GameObject goal)
    {
        if (goal == leftGoal)
        {
            rightPlayerScore++;
            Debug.Log("Right Player Scores! Score: " + rightPlayerScore);
        }
        if (goal == rightGoal)
        {
            leftPlayerScore++;
            Debug.Log("Left Player Scores! Score: " + leftPlayerScore);
        }

        ball.ResetBall();
    }
}
