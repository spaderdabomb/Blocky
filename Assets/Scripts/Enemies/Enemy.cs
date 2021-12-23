using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float health, moveSpeed;
    [SerializeField] bool facingLeft;
    [SerializeField] Transform boundaryLeft, boundaryRight;
    [SerializeField] BoxCollider2D bounceCollider;

    protected Rigidbody2D rg2d;
    protected Animator animator;
    protected GameObject player;
    protected EnemyState currentState;

    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = new EnemyState();
        currentState = EnemyState.Walking;

        if (facingLeft)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

        InitializeEnemy();
    }

    protected virtual void InitializeEnemy()
    {
        
    }

    void Update()
    {
        // Handle moving into a boundary
        if (transform.position.x >= boundaryRight.position.x)
        {
            facingLeft = true;
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (transform.position.x <= boundaryLeft.position.x)
        {
            facingLeft = false;
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

        Attack();
        Animate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected virtual void Attack()
    {
        float distanceFromPlayer = (player.transform.position - transform.position).magnitude;
        if (distanceFromPlayer < 1f)
        {
            currentState = EnemyState.Attacking;
        }
        else if (distanceFromPlayer >= 1f && currentState == EnemyState.Attacking)
        {
            currentState = EnemyState.Walking;
        }
    }

    protected virtual void Move()
    {
        if (currentState == EnemyState.Walking)
        {
            if (facingLeft)
            {
                rg2d.velocity = new Vector2(-moveSpeed, rg2d.velocity.y);
            }
            else
            {
                rg2d.velocity = new Vector2(moveSpeed, rg2d.velocity.y);
            }
        }
        else if (currentState == EnemyState.Attacking)
        {
            rg2d.velocity = new Vector2(0, 0);
        }
    }

    protected virtual void Dying()
    {
        animator.SetBool("dying", true);
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, animationLength);
    }

    public virtual void Damage(float numDamage)
    {
        health -= numDamage;
        if (health < 1f)
        {
            Dying();
        }
    }

    protected virtual void Animate()
    {
        if (currentState == EnemyState.Idle)
        {
            animator.SetBool("idling", true);
            animator.SetBool("walking", false);
            animator.SetBool("jumping", false);
            animator.SetBool("ducking", false);
            animator.SetBool("attacking", false);
        }
        else if (currentState == EnemyState.Walking)
        {
            animator.SetBool("idling", false);
            animator.SetBool("walking", true);
            animator.SetBool("jumping", false);
            animator.SetBool("ducking", false);
            animator.SetBool("attacking", false);
        }
        else if (currentState == EnemyState.Jumping)
        {
            animator.SetBool("idling", false);
            animator.SetBool("walking", false);
            animator.SetBool("jumping", true);
            animator.SetBool("ducking", false);
            animator.SetBool("attacking", false);
        }
        else if (currentState == EnemyState.Ducking)
        {
            animator.SetBool("idling", false);
            animator.SetBool("walking", false);
            animator.SetBool("jumping", false);
            animator.SetBool("ducking", true);
            animator.SetBool("attacking", false);
        }
        else if (currentState == EnemyState.Attacking)
        {
            animator.SetBool("idling", false);
            animator.SetBool("walking", false);
            animator.SetBool("jumping", false);
            animator.SetBool("ducking", false);
            animator.SetBool("attacking", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bounceCollider.IsTouching(collision.collider))
        {
            print(player.GetComponent<Player>().bounceOffEnemyForce);
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, player.GetComponent<Player>().bounceOffEnemyForce));
            player.GetComponent<Player>().currentState = Player.PlayerState.Jumping;
            Damage(1);
        }
    }

    public enum EnemyState
    {
        Idle,
        Walking,
        Jumping,
        Ducking,
        Attacking,
        Dying
    }
}
