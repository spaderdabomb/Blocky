using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolManager : MonoBehaviour
{
    public static ToolManager Instance;

    public List<Tool.ToolType> activeTools;
    public List<GameObject> toolsList;
    public Dictionary<Tool.ToolType, bool> toolsCurrentlyAvailable;
    public Dictionary<Tool.ToolType, GameObject> toolsPrefabDict;

    public Tool.ToolType toolCurrentlySelected;

    [SerializeField] GameObject toolGrappleUIPrefab;
    [SerializeField] GameObject toolPistolUIPrefab;
    [SerializeField] GameObject toolsHolder;
    [SerializeField] GameObject grappleUI;

    [SerializeField] GameObject pistol;
    [SerializeField] GameObject player;

    private void Awake()
    {
        // Initialize singleton
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;

        // Initialize self-dependent variables
        toolCurrentlySelected = Tool.ToolType.Grapple;
        activeTools = new List<Tool.ToolType>();
        activeTools.Add(Tool.ToolType.Grapple);
        toolsList = new List<GameObject>();
        toolsList.Add(grappleUI);
        toolsCurrentlyAvailable = new Dictionary<Tool.ToolType, bool>()
        {
            { Tool.ToolType.Grapple, true },
            { Tool.ToolType.Pistol, false }
        };

        toolsPrefabDict = new Dictionary<Tool.ToolType, GameObject>()
        {
            { Tool.ToolType.Grapple, toolGrappleUIPrefab },
            { Tool.ToolType.Pistol, toolPistolUIPrefab }
        };
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void NewToolPickedUp(Tool.ToolType newTool)
    {
        if (!toolsCurrentlyAvailable[newTool])
        {
            activeTools.Add(newTool);
            GameObject instantiatedTool = Instantiate<GameObject>(toolsPrefabDict[newTool], toolsHolder.transform);
            toolsList.Add(instantiatedTool);
            toolsCurrentlyAvailable[newTool] = true;
            toolCurrentlySelected = newTool;
            SetToolActive(GetIndexFromTool(newTool));
        }
        else
        {
            toolCurrentlySelected = newTool;
        }
    }

    public void SetToolActive(int toolIndex)
    {
        if (0 <= toolIndex && toolIndex <= activeTools.Count - 1)
        {
            toolCurrentlySelected = GetToolFromIndex(toolIndex);

            for (int i = 0; i < toolsHolder.transform.childCount; i++)
            {
                if (toolIndex == i)
                    toolsHolder.transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                else
                    toolsHolder.transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 75);
            }
        }

        // Set pistol to be active or inactive
        if (toolCurrentlySelected == Tool.ToolType.Pistol)
            pistol.SetActive(true);
        else
            pistol.SetActive(false);
    }
    public int GetIndexFromTool(Tool.ToolType toolType)
    {
        int toolIndex = activeTools.IndexOf(toolType);

        return toolIndex;
    }

    public Tool.ToolType GetToolFromIndex(int index)
    {
        Tool.ToolType toolType = activeTools[index];

        return toolType;
    }
}

