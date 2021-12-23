using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] int roomTriggerIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("new camera");

            mainCamera.GetComponent<CameraController>().changeCameras(roomTriggerIndex);
        }
    }
}
