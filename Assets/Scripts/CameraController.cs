using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject cameraHolder;
    List<GameObject> virtualCameras = new List<GameObject>();
    private void Start()
    {
        foreach (Transform gameObjectTransform in cameraHolder.transform)
        {
            virtualCameras.Add(gameObjectTransform.gameObject);
        }
    }
    public void changeCameras(int cameraIndex)
    {
        print("changing cameras");
        for (int i = 0; i < virtualCameras.Count; i++)
        {
            if (i == cameraIndex)
            {
                virtualCameras[cameraIndex].SetActive(true);
            }
            else
            {
                virtualCameras[cameraIndex].SetActive(false);

            }
        }
    }
}
