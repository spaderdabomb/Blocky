using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameSceneController gameSceneController;

    public PlayerState currentState;

    public BoxCollider2D playerBoxCollider2D;
    public CircleCollider2D playerCircleCollider2D;
    public CharacterController2D controller;
    public float runSpeed = 20f;
    public float grappleDamage = 1f;
    public float bounceOffEnemyForce = 1000f;

    float horizontalMove = 0f;
    public bool jump = false;

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

        UpdateState();
    }

    void UpdateState()
    {
        if (currentState == PlayerState.Jumping)
        {

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
        if (collision.CompareTag("Star") && playerCircleCollider2D.IsTouching(collision))
        {
            GameObject.Destroy(collision.gameObject);
            gameSceneController.starCoinsCollected += 1;
        }

        if (collision.CompareTag("DeathTiles"))
        {

        }

        if (collision.CompareTag("Powerup") && playerCircleCollider2D.IsTouching(collision))
        {
            GameObject.Destroy(collision.gameObject);
            PowerupManager.Instance.NewPowerupPickedUp(PowerupManager.PowerupType.DoubleJump);
        }

    }
}
