using System;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    Rigidbody2D RigidB2D;
    void Start()
    {
        RigidB2D = GetComponent<Rigidbody2D>();
        RigidB2D.bodyType = RigidbodyType2D.Static;
    }
    void Update()
    {
        Movement(Input.GetAxis("Vertical"));

        var pos = transform.position;
        pos.y = Mathf.Clamp(transform.position.y, -4, 4);
        transform.position = pos;
    }

    void Movement(float direction)
    {
        transform.Translate(new Vector3(0, direction * 5 * Time.deltaTime, 0));
    }
}
