using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform lookAt;
    private Transform camTransform;
    private Camera cam;
    private float distance = 20f;
    private float currentX = 0f;
    private float currentY = 0f;
    //private float sensitivityX = 4.0f;
    //private float sensitivityY = 1f;
    private const float anglemin = -60f;
    private const float anglemax = 60f;
    private const float Maxdistance = 10f;
    private const float MinDistance = 5f;
	// Use this for initialization
	void Start () {
        camTransform = transform;
	}
	
	// Update is called once per frame
	private void Update ()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y") * -1f;
        currentY = Mathf.Clamp(currentY, anglemin, anglemax);

        distance += Input.GetAxis("Mouse ScrollWheel") * 10f;
        distance = Mathf.Clamp(distance, MinDistance, Maxdistance);
	}

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;

        camTransform.LookAt(lookAt.position);
    }
}
