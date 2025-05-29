using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class BallMovement : NetworkBehaviour
{
    Rigidbody2D RigidB2D;
    private float ballSpeed = 10.0f;

    public override void OnNetworkSpawn()
    {
        RigidB2D = GetComponent<Rigidbody2D>();

        RigidB2D.simulated = true;
        RigidB2D.bodyType = RigidbodyType2D.Dynamic;
        RigidB2D.angularDamping = 0;
        RigidB2D.gravityScale = 0;
    }
    void LaunchBall() 
    {
        RigidB2D.AddForce(new Vector2(-ballSpeed, 0), ForceMode2D.Impulse);
    }
    public void ResetBall()
    {
        transform.position = Vector2.zero;
        RigidB2D.linearVelocity = Vector2.zero;
        LaunchBall();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            float paddleY = collision.transform.position.y;
            float ballY = transform.position.y;

            float paddleHeight = collision.collider.bounds.size.y;

            float yHit = (ballY - paddleY) / (paddleHeight / 2);
            float xDir = (float)(RigidB2D.linearVelocity.x > 0 ? 0.5 : -0.5);
            Vector2 newDirection = new Vector2(xDir, yHit).normalized;

            RigidB2D.linearVelocity = newDirection * ballSpeed;
        }

    }
}
