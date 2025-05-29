using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    public NetworkVariable<int> leftPlayerScore = new(0);
    public NetworkVariable<int> rightPlayerScore = new(0);

    public GameObject ballPrefab;
    public Transform ballSpawn;
    private BallMovement ballMovement;

    public GameObject paddlePrefab;
    public Transform leftSpawn;
    public Transform rightSpawn;
    public GameObject leftGoal;
    public GameObject rightGoal;

    private int playerCount = 0;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += SpawnPlayerPaddle;
        }
    }

    private void SpawnPlayerPaddle(ulong clientId)
    {
        if (playerCount >= 2)
        {
            Debug.LogWarning("Maximum player count reached.");
            return;
        }

        Transform spawnPoint = playerCount == 0 ? leftSpawn : rightSpawn;
        GameObject paddleInstance = Instantiate(paddlePrefab, spawnPoint.position, Quaternion.identity);
        paddleInstance.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);

        playerCount++;

        if (playerCount == 2)
        {
            GameObject ballInstance = Instantiate(ballPrefab, ballSpawn.position, Quaternion.identity);
            var ballNetObj = ballInstance.GetComponent<NetworkObject>();
            ballNetObj.Spawn();

            ballMovement = ballInstance.GetComponent<BallMovement>();

            StartCoroutine(WaitAndResetBall());
        }
    }

    private System.Collections.IEnumerator WaitAndResetBall()
    {
        yield return new WaitForSeconds(0.1f);
        ballMovement.ResetBall();
    }

    public void OnGoalScored(GameObject goal)
    {
        if (!IsServer) return;

        if (goal == leftGoal)
        {
            rightPlayerScore.Value++;
            Debug.Log("Right Player Scores! Score: " + rightPlayerScore.Value);
        }

        if (goal == rightGoal)
        {
            leftPlayerScore.Value++;
            Debug.Log("Left Player Scores! Score: " + leftPlayerScore.Value);
        }

        ballMovement.ResetBall();
    }
}