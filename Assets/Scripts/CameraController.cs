using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject cameraHolder;
    List<GameObject> virtualCameras = new List<GameObject>();
    private void Awake()
    {
        foreach (Transform gameObjectTransform in cameraHolder.transform)
        {
            virtualCameras.Add(gameObjectTransform.gameObject);
        }
    }
    public void changeCameras(int cameraIndex)
    {
        for (int i = 0; i < virtualCameras.Count; i++)
        {
            if (i == cameraIndex)
            {

            }
            else
            {
                virtualCameras[cameraIndex].SetActive(false);
            }
        }

        virtualCameras[cameraIndex].SetActive(true);
    }
}
