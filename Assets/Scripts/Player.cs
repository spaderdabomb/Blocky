using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // TODO: Player can move after pressing jump after grappling

    [SerializeField] GameSceneController gameSceneController;
    [SerializeField] PowerupManager powerupManager;

    [SerializeField] GameObject pistol;
    [SerializeField] GameObject laserBulletPrefab;

    public PlayerState currentState;

    public BoxCollider2D playerBoxCollider2D;
    public CircleCollider2D playerCircleCollider2D;
    public CharacterController2D controller;
    public float runSpeed = 20f;
    public float grappleDamage = 1f;
    public float pistolDamage = 1f;
    public float bounceOffEnemyForce = 1000f;

    float horizontalMove = 0f;
    float timeSinceLastPistolShot = 0f;
    float timeBetweenPistolShots = 1f;
    bool jump = false;
    bool canJumpOverride = false;
    bool canFlyOverride = false;
    bool usedDoubleJump;

    void Start()
    {
        currentState = new PlayerState();
        currentState = PlayerState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle horizontal movement and jump input
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            currentState = PlayerState.Jumping;
        }

        timeSinceLastPistolShot += Time.deltaTime;

        UpdatePowerups();
        UpdateState();
    }

    void UpdatePowerups()
    {
        for (int i = 0; i < powerupManager.activePowerups.Count; i++)
        {
            // Update double jump parameters
            if (powerupManager.activePowerups[i] == Powerup.PowerupType.DoubleJump)
            {
                // Resets ability to use double jump
                if (controller.m_Grounded)
                {
                    usedDoubleJump = false;
                }

                if (Input.GetButtonDown("Jump") && !usedDoubleJump && !controller.m_Grounded)
                {
                    canJumpOverride = true;
                    usedDoubleJump = true;
                }
            }
            else
            {
                canJumpOverride = false;
                usedDoubleJump = false;
            }

            // Update fly parameters
            if (powerupManager.activePowerups[i] == Powerup.PowerupType.Fly)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    currentState = PlayerState.Flying;
                    canFlyOverride = true;
                }
                else if (Input.GetButtonUp("Jump"))
                {
                    if (controller.m_Grounded)
                    {
                        currentState = PlayerState.Walking;
                    }
                    else
                    {
                        currentState = PlayerState.Jumping;
                    }
                    canFlyOverride = false;
                }
            }
            else
            {
                currentState = PlayerState.Walking;
                canFlyOverride = false;
            }

            // Update shield parameters
            if (powerupManager.activePowerups[i] == Powerup.PowerupType.Shield)
            {

            }
        }
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
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump, canJumpOverride, canFlyOverride);
        }

        jump = false;
        canJumpOverride = false;
    }

    public enum PlayerState 
    {
        Idle,
        Walking,
        Jumping,
        Grappling,
        Flying
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
            gameSceneController.PlayerDied();
        }

        if (collision.CompareTag("Powerup") && playerCircleCollider2D.IsTouching(collision))
        {
            PowerupManager.Instance.NewPowerupPickedUp(collision.gameObject.GetComponent<Powerup>().powerupType);
            GameObject.Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Tool") && playerCircleCollider2D.IsTouching(collision))
        {
            ToolManager.Instance.NewToolPickedUp(collision.gameObject.GetComponent<Tool>().toolType);
            GameObject.Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Laser") && playerCircleCollider2D.IsTouching(collision))
        {
            SpriteMask laserSpriteMask = collision.transform.parent.GetComponentInChildren<SpriteMask>();
            if (PowerupManager.Instance.activePowerups.Contains(Powerup.PowerupType.Shield) && !laserSpriteMask.enabled)
            {
                int shieldPowerupIndex = PowerupManager.Instance.GetIndexFromPowerup(Powerup.PowerupType.Shield);
                PowerupManager.Instance.RemovePowerupAtIndex(shieldPowerupIndex);
                ParticleSystem laserParticleSystem = collision.transform.parent.GetComponentInChildren<ParticleSystem>();
                laserParticleSystem.Play();
                laserSpriteMask.enabled = true;
            }
            else if (!laserSpriteMask.enabled)
            {
                gameSceneController.PlayerDied();
            }
        }

    }

    public void ShootPistol()
    {
        if (timeSinceLastPistolShot >= timeBetweenPistolShots)
        {
            Vector2 bulletOffset = new Vector2(0.41f, 0.07f);
            Vector2 gunPosition = pistol.transform.position;
            GameObject laserBullet = Instantiate<GameObject>(laserBulletPrefab);
            laserBullet.transform.position = gunPosition + bulletOffset;
            laserBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(GlobalData.laserBulletSpeed * Mathf.Sign(transform.localScale.x), 0);
            timeSinceLastPistolShot = 0;
        }
    }

    public void InteractPressed()
    {
        List<Collider2D> colliderList = new List<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();
        int colliderCount = playerCircleCollider2D.OverlapCollider(contactFilter.NoFilter(), colliderList);

        for (int i = 0; i <  colliderList.Count; i++)
        {
            if (colliderList[i].CompareTag("Chest"))
            {
                print("got chest");
            }
        }
    }
}
