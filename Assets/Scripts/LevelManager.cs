using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int levelNum;

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

    public void InitializeLevel(int _levelNum)
    {
        levelNum = _levelNum;
        SceneManager.LoadScene("GameScene");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
