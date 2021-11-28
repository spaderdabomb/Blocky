using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    int buttonIndex;

    void Awake()
    {
        buttonIndex = gameObject.transform.GetSiblingIndex() + 1;
    }

    void Start()
    {
        gameObject.GetComponentInChildren<Text>().text = buttonIndex.ToString();
        gameObject.GetComponent<Button>().onClick.AddListener(LevelManager.Instance.InitializeLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void testing()
    {
        print("Hello world");
    }
}