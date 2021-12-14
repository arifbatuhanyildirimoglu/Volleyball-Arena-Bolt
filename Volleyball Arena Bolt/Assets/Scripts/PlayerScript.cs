using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;

public class PlayerScript : EntityBehaviour<IPlayerObjectState>
{
    
    private float speed = 5f;

    private float jumpForce = 400f;

    private bool isGrounded;

    private int score = 0;

    public int Score
    {
        get => score;
        set => score = value;
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.collider.name.Equals("Ground"))
            isGrounded = true;

    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.name.Equals("Ground"))
            isGrounded = false;
    }
    
    
    public override void Attached()
    {
        state.SetTransforms(state.PlayerObjectTransform, transform);
    }
    
    
    public override void SimulateOwner()
    {
        
        Vector3 movement = new Vector3();
        Vector3 YAxis = new Vector3();

        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement.x = 1f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x = -1f;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            YAxis.y = 1f;
            GetComponent<Rigidbody>().AddForce(YAxis * jumpForce, ForceMode.Impulse);
        }

        transform.Translate(movement * speed * BoltNetwork.FrameDeltaTime);
    }
}
