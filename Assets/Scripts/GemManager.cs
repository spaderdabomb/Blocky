using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GemManager : MonoBehaviour
{
    public static GemManager Instance;

    Dictionary<Gem.GemType, GameObject> gemPrefabDict;

    [SerializeField] GameObject gemRedUIPrefab;
    [SerializeField] GameObject gemBlueUIPrefab;
    [SerializeField] GameObject gemYellowUIPrefab;
    [SerializeField] GameObject gemPurpleUIPrefab;
    [SerializeField] GameObject gemGreenUIPrefab;

    [SerializeField] GameObject gemHolder;

    void Awake()
    {
        // Initialize singleton
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;

        gemPrefabDict = new Dictionary<Gem.GemType, GameObject>()
        {
            { Gem.GemType.Red,  gemRedUIPrefab },
            { Gem.GemType.Blue,  gemBlueUIPrefab },
            { Gem.GemType.Yellow,  gemYellowUIPrefab },
            { Gem.GemType.Purple,  gemPurpleUIPrefab },
            { Gem.GemType.Green,  gemGreenUIPrefab }
        };
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitGemsForRoom()
    {
        for (int i = 0; i < GlobalData.gemTypesInRooms.Count; i++)
        {
            Tuple<int, int, Gem.GemType[]> gemTypeTuple = GlobalData.gemTypesInRooms[i];
            if (gemTypeTuple.Item1 == LevelManager.Instance.levelNum && gemTypeTuple.Item2 == LevelManager.Instance.roomNum)
            {
                for (int j = 0; j < gemTypeTuple.Item3.Length; j++)
                {
                    GameObject tempGem = Instantiate<GameObject>(gemPrefabDict[gemTypeTuple.Item3[j]], gemHolder.transform);
                    tempGem.GetComponent<Image>().color = new Color32(255, 255, 255, 75);
                }
            }
        }
    }
}
