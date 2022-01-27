using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int levelNum;
    public int roomNum;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    void Start()
    {
        
    }

    public void InitLevel(int _levelNum, int _roomNum)
    {
        levelNum = _levelNum;
        roomNum = _roomNum;
        SceneManager.LoadScene("GameScene");
    }

    public void InitRoom(int _roomNum)
    {
        roomNum = _roomNum;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
