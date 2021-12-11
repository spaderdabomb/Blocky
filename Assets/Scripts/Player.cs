using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameSceneController gameSceneController;

    public PlayerState currentState;

    public CharacterController2D controller;
    public float runSpeed = 20f;

    float horizontalMove = 0f;
    bool jump = false;

    void Start()
    {
        currentState = new PlayerState();
        currentState = PlayerState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            currentState = PlayerState.Jumping;
        }

        
    }

    private void FixedUpdate()
    {
        if (currentState == PlayerState.Grappling)
        {

        }
        else
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        }
        jump = false;
    }

    public enum PlayerState 
    {
        Idle,
        Walking,
        Jumping,
        Grappling
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Star"))
        {
            GameObject.Destroy(collision.gameObject);
            gameSceneController.starCoinsCollected += 1;
        }
    }
}
