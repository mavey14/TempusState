using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    [SerializeField]
    GameObject obsenter;
    //[SerializeField]
    //GameObject obsexit;
    [SerializeField]
    GameObject Player;
    bool activate;
    [SerializeField]
    GameObject gmscript;
    // Use this for initialization
    void Start () {
        activate = true;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activate == true&& other.gameObject.tag == "Player")
        {
           // Debug.Log("asda");

            // Debug.Log("test saan galing ");
             Vector3 dir = (Player.GetComponent<Transform>().position - this.transform.position).normalized;
            if (Vector3.Dot(dir, Vector3.forward) > 0)
            {
                //  Debug.Log("Enter");
                //collide = true;
                //Debug.Log(collide);
                Debug.Log("Enter");
                if (obsenter != null)
                    obsenter.SetActive(false);
                if (this.gameObject.name == "LastPortal")
                {
                    if (gmscript != null)
                    {
                        gmscript.GetComponent<GMScript>().LoadLevel(2);
                    }
                }
                //if (obsexit != null)
                //obsexit.SetActive(true);
            }
            else /*if (Vector3.Dot(dir, transform.forward) < 0)*/
            {
                Debug.Log("Exit");
                if (obsenter != null)
                    obsenter.SetActive(true);
                // if (obsexit != null)
                //obsexit.SetActive(false);
            }

            StartCoroutine(Reactive());
            activate = false;
        }
       
    }

    IEnumerator Reactive()
    {
        yield return new WaitForSeconds(1f);
        activate =true;
    }

    
}
