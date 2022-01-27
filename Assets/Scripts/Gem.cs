using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public GemType gemType;

    void Start()
    {
        if (gameObject.GetComponent<Material>().name == "GemRed")
            gemType = GemType.Red;
        else if (gameObject.GetComponent<Material>().name == "GemBlue")
            gemType = GemType.Blue;
        else if (gameObject.GetComponent<Material>().name == "GemGreen")
            gemType = GemType.Green;
        else if (gameObject.GetComponent<Material>().name == "GemPurple")
            gemType = GemType.Purple;
        else if (gameObject.GetComponent<Material>().name == "GemYellow")
            gemType = GemType.Yellow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitGemFoundAnimation()
    {
        gameObject.GetComponent<Animator>().SetBool("ChestOpened", true);
    }

    public enum GemType
    {
        Red,
        Blue,
        Green,
        Purple,
        Yellow
    }
}
