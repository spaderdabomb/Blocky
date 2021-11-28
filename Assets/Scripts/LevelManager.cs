using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int levelNum;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    public void InitializeLevel()
    {
        levelNum = 1;
        SceneManager.LoadScene("GameScene");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
