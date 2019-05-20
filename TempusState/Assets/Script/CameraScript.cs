using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    [SerializeField]
    private Transform[] lookAt;
    private Transform camTransform;
    private Camera cam;
    private float distance;
    private float currentX = 0f;
    private float currentY = 0f;
    //private float sensitivityX = 4.0f;
    //private float sensitivityY = 1f;
    private const float anglemin = -10f;
    private const float anglemax = 20f;
    private float Maxdistance;
    private float MinDistance;
    int target;
    [SerializeField]
    GameObject gmscript;
    public bool camerashake;
    bool shakecd;
	// Use this for initialization
	void Start () {
        distance = gmscript.GetComponent<GMScript>().sceneIndex == 3 ? 80f : 10f;
        MinDistance = gmscript.GetComponent<GMScript>().sceneIndex == 3 ? 25f : 5f;
        Maxdistance = gmscript.GetComponent<GMScript>().sceneIndex == 3 ? 40f : 20f;
        camTransform = transform;
        target=0;
        camerashake = shakecd=false;

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


        if (Input.GetKeyDown(KeyCode.K))
        {
            camerashake = true;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            camerashake = false;
        }

    }

    private void LateUpdate()
    {
        if (camerashake == false)
        {
            Vector3 dir = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            camTransform.position = lookAt[target].position + rotation * dir;

            camTransform.LookAt(lookAt[target].position);
        }
        else if (camerashake == true)
        {
           // if(shakecd==false)
            StartCoroutine(shake(.1f, .1f));
            //shakecd = true;
        }
       
    }

    IEnumerator shake(float duration, float magnitude)
    {

      // Vector3 originalPos = transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f)*magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.position = new Vector3(transform.position.x+x, transform.position.y+y,transform.position.z);

            elapsed += Time.deltaTime;
            yield return null;


        }
       //transform.position = originalPos;
        yield return new WaitForSeconds(.2f);
        camerashake = false;
       // yield return new WaitForSeconds(5f);
       // shakecd = false;
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
