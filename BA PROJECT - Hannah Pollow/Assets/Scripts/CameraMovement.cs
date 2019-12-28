using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float[] timeBetweenCameraPoints;

    [SerializeField] private GameObject rotationPointsParent;
    [SerializeField] private Transform[] cameraPoints;
    [SerializeField] private GameObject player;

    [SerializeField] private float rotSpeed;

    private Camera cam;
    public int pointsPassed;


    private void Start()
    {
        pointsPassed = 1;
        cam = this.gameObject.GetComponent<Camera>();
        cameraPoints = rotationPointsParent.GetComponentsInChildren<Transform>();
        cam.transform.LookAt(cameraPoints[1]);
        cam.transform.position = cameraPoints[0].transform.position;

        foreach (Transform x in cameraPoints)
        {
            x.gameObject.SetActive(false);
        }
        cameraPoints[pointsPassed - 1].gameObject.SetActive(true);
        cameraPoints[pointsPassed].gameObject.SetActive(true);
        cameraPoints[pointsPassed + 1].gameObject.SetActive(true);

    }  

    private void Update()
    {
        if (pointsPassed == cameraPoints.Length - 2)
        {
            foreach (Transform x in cameraPoints)
            {
               Destroy(x.gameObject);
            }
            player.SetActive(true);
            Destroy(this.gameObject);
            
        }
        else
        {
            cameraPoints[pointsPassed + 1].gameObject.SetActive(true);
            cam.gameObject.transform.Translate(new Vector3(0, 0, 1 * timeBetweenCameraPoints[pointsPassed]) * Time.deltaTime);
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.LookRotation(cameraPoints[pointsPassed].position - cam.transform.position), rotSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Camera Point"))
        {
            pointsPassed++;
        }
    }

}
