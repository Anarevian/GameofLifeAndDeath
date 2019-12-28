using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject player;

    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float minYAngle;
    [SerializeField] private float maxYAngle;

    [SerializeField] private string mouseXAxisName;
    [SerializeField] private string mouseYAxisName;

    [SerializeField] private float offset = -3;
    [SerializeField] private float farthestZoom = -7;
    [SerializeField] private float closestZoom = -2;
    [SerializeField] private float camFollow = 8;
    [SerializeField] private float camZoom = 1.75f;

    public float x = 0.0f;
    private float y = 0.0f;


    Camera myCamera;
    LayerMask mask;

    private void Start()
    {
        myCamera = gameObject.GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mask = 1 << LayerMask.NameToLayer("Clippable") | 0 << LayerMask.NameToLayer("NotClippable");
    }

    private void Update()
    {       
        x += Input.GetAxis(mouseXAxisName) * mouseSensitivity;
        y += Input.GetAxis(mouseYAxisName) * mouseSensitivity;
        y = Mathf.Clamp(y, minYAngle, maxYAngle);               
    }

    private void LateUpdate()
    {
        cameraMovement();
        Zoom();
    }

    private void cameraMovement()
    {
        target.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(-y, x, 0), 0.5f);
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(0, x, 0), 0.5f);

        transform.LookAt(target);
    }

    private void Zoom()
    {
        offset += Input.GetAxis("Mouse ScrollWheel") * camZoom;

        if (offset > closestZoom) offset = closestZoom;
        else if (offset < farthestZoom) offset = farthestZoom;

        float unobstructed = offset;
        Vector3 idealPostion = target.TransformPoint(Vector3.forward * offset);

        RaycastHit hit;
        if (Physics.Linecast(target.position, idealPostion, out hit, mask.value))
        {
            unobstructed = -hit.distance + .01f;
        }


        Vector3 desiredPos = target.TransformPoint(Vector3.forward * unobstructed);
        Vector3 currentPos = myCamera.transform.position;

        Vector3 goToPos = new Vector3(Mathf.Lerp(currentPos.x, desiredPos.x, camFollow), Mathf.Lerp(currentPos.y, desiredPos.y, camFollow), Mathf.Lerp(currentPos.z, desiredPos.z, camFollow));

        myCamera.transform.position = goToPos;
        myCamera.transform.LookAt(target.position);


        float c = myCamera.nearClipPlane;
        bool clip = true;
        while (clip)
        {
            Vector3 pos1 = myCamera.ViewportToWorldPoint(new Vector3(0, 0, c));
            Vector3 pos2 = myCamera.ViewportToWorldPoint(new Vector3(.5f, 0, c));
            Vector3 pos3 = myCamera.ViewportToWorldPoint(new Vector3(1, 0, c));
            Vector3 pos4 = myCamera.ViewportToWorldPoint(new Vector3(0, .5f, c));
            Vector3 pos5 = myCamera.ViewportToWorldPoint(new Vector3(1, .5f, c));
            Vector3 pos6 = myCamera.ViewportToWorldPoint(new Vector3(0, 1, c));
            Vector3 pos7 = myCamera.ViewportToWorldPoint(new Vector3(.5f, 1, c));
            Vector3 pos8 = myCamera.ViewportToWorldPoint(new Vector3(1, 1, c));

            Debug.DrawLine(myCamera.transform.position, pos1, Color.yellow);
            Debug.DrawLine(myCamera.transform.position, pos2, Color.yellow);
            Debug.DrawLine(myCamera.transform.position, pos3, Color.yellow);
            Debug.DrawLine(myCamera.transform.position, pos4, Color.yellow);
            Debug.DrawLine(myCamera.transform.position, pos5, Color.yellow);
            Debug.DrawLine(myCamera.transform.position, pos6, Color.yellow);
            Debug.DrawLine(myCamera.transform.position, pos7, Color.yellow);
            Debug.DrawLine(myCamera.transform.position, pos8, Color.yellow);

            if (Physics.Linecast(myCamera.transform.position, pos1, out hit, mask.value))
            {

            }
            else if (Physics.Linecast(myCamera.transform.position, pos2, out hit, mask.value))
            {

            }
            else if (Physics.Linecast(myCamera.transform.position, pos3, out hit, mask.value))
            {

            }
            else if (Physics.Linecast(myCamera.transform.position, pos4, out hit, mask.value))
            {

            }
            else if (Physics.Linecast(myCamera.transform.position, pos5, out hit, mask.value))
            {

            }
            else if (Physics.Linecast(myCamera.transform.position, pos6, out hit, mask.value))
            {

            }
            else if (Physics.Linecast(myCamera.transform.position, pos7, out hit, mask.value))
            {

            }
            else if (Physics.Linecast(myCamera.transform.position, pos8, out hit, mask.value))
            {

            }
            else clip = false;

            if (clip) myCamera.transform.localPosition += myCamera.transform.forward * c;
        }
    }
}

