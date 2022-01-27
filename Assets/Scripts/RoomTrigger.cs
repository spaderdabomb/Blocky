using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] GameSceneController gameSceneController;
    [SerializeField] GameObject mainCamera;
    [SerializeField] int roomTriggerIndex;
    [SerializeField] PowerupManager powerupManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameSceneController.NewRoomEntered(roomTriggerIndex);
        }
    }
}
