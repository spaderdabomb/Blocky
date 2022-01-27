using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerupType powerupType;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum PowerupType
    {
        DoubleJump,
        Fly,
        Shield,
        RapidFire,
    }
}
