using System;
using UnityEngine;
using Unity.Netcode;

public class PaddleMovement : NetworkBehaviour
{
    Rigidbody2D RigidB2D;
    private void Initialize()
    {
        RigidB2D = GetComponent<Rigidbody2D>();
        RigidB2D.bodyType = RigidbodyType2D.Static;
    }
    public override void OnNetworkSpawn() 
    {
        base.OnNetworkSpawn();
        Initialize();
    }
    void Update()
    {
        if (!Application.isFocused) return;

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
