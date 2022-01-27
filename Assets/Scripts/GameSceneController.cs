using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] GameObject grapplePrefab;
    [SerializeField] GameObject gates;
    [SerializeField] GameObject player;
    [SerializeField] GameObject mainCamera;

    [SerializeField] CameraController cameraController;

    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

    GameObject instantiatedGrapple;

    public int starCoinsCollected;
    void Start()
    {
        instantiatedGrapple = null;

        player.transform.position = GlobalData.roomPositions[LevelManager.Instance.levelNum - 1][LevelManager.Instance.roomNum - 1];
        NewRoomEntered(LevelManager.Instance.roomNum);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

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

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ToolManager.Instance.toolCurrentlySelected == Tool.ToolType.Grapple)
            {
                instantiatedGrapple = Instantiate(grapplePrefab, Vector3.zero, Quaternion.identity);
                Grapple grapple = instantiatedGrapple.GetComponent<Grapple>();
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                grapple.InitializeGrapple(worldPosition);
            }
            else if (ToolManager.Instance.toolCurrentlySelected == Tool.ToolType.Pistol)
            {
                player.GetComponent<Player>().ShootPistol();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (ToolManager.Instance.toolCurrentlySelected == Tool.ToolType.Grapple)
            {
                instantiatedGrapple.GetComponent<Grapple>().EndGrapple();
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            player.GetComponent<Player>().InteractPressed();
        }

        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                int numberPressed = i + 1;
                ToolManager.Instance.SetToolActive(numberPressed - 1);
            }
        }
    }

    public void NewRoomEntered(int roomNum) // roomNum != roomIndex (roomIndex = roomNum - 1)
    {
        int roomIndex = roomNum - 1;
        mainCamera.GetComponent<CameraController>().changeCameras(roomIndex);
        PowerupManager.Instance.GetComponent<PowerupManager>().ClearAllPowerups();
        LevelManager.Instance.InitRoom(roomNum);
        GemManager.Instance.GetComponent<GemManager>().InitGemsForRoom();
    }

    public void PlayerDied()
    {
        print("Player died");
    }
}
