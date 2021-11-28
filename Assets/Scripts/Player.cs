using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;

    bool isColliding;
    bool isJumping;

    void Start()
    {
        if (speed == 0f) { speed = GlobalData.defaultPlayerSpeed; }
        if (jumpSpeed == 0f) { speed = GlobalData.defaultPlayerJumpSpeed; }

        isColliding = false;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveX();
        Jump();
    }

    void MoveX()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontal, 0));
    }

    void Jump()
    {
        if (Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) > 0f)
        {
            isJumping = true;
            print("isjumping is true");
        }
        else
        {
            isJumping = false;
        }

        if (!isJumping)
        {
            print("initiating jump");
            float vertical = Input.GetAxis("Vertical") * jumpSpeed;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, vertical));
            isJumping = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isColliding = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }


}
