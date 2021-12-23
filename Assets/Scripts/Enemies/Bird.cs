using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy
{
    private void Awake()
    {

    }

    protected override void InitializeEnemy()
    {
        base.InitializeEnemy();

        health = 1;
        moveSpeed = 3;
    }

    protected override void Attack()
    {
        
    }

    protected override void Dying()
    {
        animator.SetBool("dying", true);
        rg2d.bodyType = RigidbodyType2D.Dynamic;
        rg2d.gravityScale = 1;
        transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), -transform.localScale.y);

        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, animationLength*2);
        
    }

}
