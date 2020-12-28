using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{


    public float moveSpeed = 0.1f;
    public float XMin = 4f;
    public float XMax = 4f;
    public bool isMovingRight = true;
    public bool isFacingRight = true;
    private float startPositionX;
    private Rigidbody2D rigidBody;


    // Start is called before the first frame update
    void Start()
    {
        startPositionX = this.transform.position.x;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingRight)
        {
            if (this.transform.position.x < startPositionX + XMax)
            {
                MoveRight();
            }
            else
            {
                isMovingRight = false;
                MoveLeft();                
            }
        }
        else
        {
            if (this.transform.position.x > startPositionX - XMin)
            {
                MoveLeft();
            }
            else
            {
                isMovingRight = true;
                MoveRight();              
            }
        }
    }


    void MoveRight()
    {
        transform.Translate(moveSpeed, 0.0f, 0.0f, Space.World);
    }

    void MoveLeft()
    {
        transform.Translate(-moveSpeed, 0.0f, 0.0f, Space.World);
    }

    /*
    void MoveRight()
    {
        if (rigidBody.velocity.x < -moveSpeed)
        {
            rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);
            rigidBody.AddForce(Vector2.right * 90.6f, ForceMode2D.Impulse);
        }
    }

    void MoveLeft()
    {
        if (rigidBody.velocity.x < moveSpeed)
        {
            rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
            rigidBody.AddForce(Vector2.right * 90.6f, ForceMode2D.Impulse);
        }
    }
    */
}
