using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] GameObject grapplePrefab;
    [SerializeField] GameObject gates;

    GameObject instantiatedGrapple;

    public int starCoinsCollected;
    void Start()
    {
        instantiatedGrapple = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            instantiatedGrapple = Instantiate(grapplePrefab, Vector3.zero, Quaternion.identity);
            Grapple grapple = instantiatedGrapple.GetComponent<Grapple>();
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            grapple.InitializeGrapple(worldPosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            instantiatedGrapple.GetComponent<Grapple>().EndGrapple();
        }

        // Open gates
        for (int i = 0; i < GlobalData.roomStarCoinThresholds[LevelManager.Instance.levelNum - 1].Length; i++)
        {
            if (starCoinsCollected >= GlobalData.roomStarCoinThresholds[LevelManager.Instance.levelNum - 1][i] && i < gates.transform.childCount)
            {
                GameObject tempGate = gates.transform.GetChild(i).gameObject;
                tempGate.SetActive(false);
            }
        }
    }
}
