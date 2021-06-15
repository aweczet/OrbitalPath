using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controll : MonoBehaviour
{
    public GameObject[] cameras;
    private int _cameraId = 0;

    private TextMeshProUGUI _planetName;

    private void Start()
    {
        foreach (var cam in cameras)
        {
            cam.SetActive(false);
        }
        cameras[_cameraId].SetActive(true);
        _planetName = transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        ChangeName();
    }

    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Next()
    {
        cameras[_cameraId].SetActive(false);
        _cameraId = _cameraId + 1 > cameras.Length - 1 ? 0 : _cameraId + 1;
        cameras[_cameraId].SetActive(true);
        ChangeName();
    }

    public void Back()
    {
        cameras[_cameraId].SetActive(false);
        _cameraId = _cameraId - 1 < 0 ? cameras.Length - 1 : _cameraId - 1;
        cameras[_cameraId].SetActive(true);
        ChangeName();
    }

    public void ZoomOut()
    {
        Vector3 cameraPosition = cameras[_cameraId].transform.position;
        float newCameraPosition = cameraPosition.y + 2 > 40 ? cameraPosition.y : cameraPosition.y + 2;
        cameras[_cameraId].transform.position = new Vector3(cameraPosition.x, newCameraPosition, cameraPosition.z);
    }

    public void ZoomIn()
    {
        Vector3 cameraPosition = cameras[_cameraId].transform.position;
        float newCameraPosition = cameraPosition.y - 2 < 2 ? cameraPosition.y : cameraPosition.y - 2;
        cameras[_cameraId].transform.position = new Vector3(cameraPosition.x, newCameraPosition, cameraPosition.z);
    }

    private void ChangeName()
    {
        _planetName.text = cameras[_cameraId].transform.parent.parent.name;
    }
}
