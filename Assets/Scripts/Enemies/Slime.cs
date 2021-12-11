using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    private void Awake()
    {

    }

    protected override void InitializeEnemy()
    {
        base.InitializeEnemy();

        health = 1;
        moveSpeed = 2;
    }

}
