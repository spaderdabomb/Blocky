using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectController : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject scrollContent;

    void Start()
    {
        for (int i = 0; i < GlobalData.numLevels; i++)
        {
            Instantiate(buttonPrefab, scrollContent.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
