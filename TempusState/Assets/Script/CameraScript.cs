using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    [SerializeField]
    private Transform[] lookAt;
    private Transform camTransform;
    private Camera cam;
    private float distance = 20f;
    private float currentX = 0f;
    private float currentY = 0f;
    //private float sensitivityX = 4.0f;
    //private float sensitivityY = 1f;
    private const float anglemin = -20f;
    private const float anglemax = 60f;
    private const float Maxdistance = 20f;
    private const float MinDistance = 5f;
    int target;
    [SerializeField]
    GameObject gmscript;
	// Use this for initialization
	void Start () {
        camTransform = transform;
        target=0;
	}
	
	// Update is called once per frame
	private void Update ()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y") * -1f;
        currentY = Mathf.Clamp(currentY, anglemin, anglemax);

        distance += Input.GetAxis("Mouse ScrollWheel") * 10f;
        distance = Mathf.Clamp(distance, MinDistance, Maxdistance);

        changetarget();
	}

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt[target].position + rotation * dir;

        camTransform.LookAt(lookAt[target].position);
    }

    void changetarget()
    {


        if(gmscript.GetComponent<GMScript>().cskill == 0)
        {
            target = 0;
        }
        else if (gmscript.GetComponent<GMScript>().cskill == 1)
        {
            target = 1;
        }
    }
}
