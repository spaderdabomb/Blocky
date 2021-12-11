using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health, moveSpeed;
    [SerializeField] bool facingLeft;
    [SerializeField] Transform boundaryLeft, boundaryRight;

    Rigidbody2D rg2d;

    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();

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
        print("initialize");
    }

    void Update()
    {
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
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
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

    enum EnemyState
    {
        Idle,
        Walking,
        Jumping,
        Grappling
    }
}
