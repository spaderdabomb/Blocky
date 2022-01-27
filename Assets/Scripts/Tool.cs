using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public ToolType toolType;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum ToolType
    {
        Grapple,
        Pistol
    }
}
