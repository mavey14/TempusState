using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] UI;
    [SerializeField]
    GameObject UIBG;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (this.gameObject.name == "g1")
            {
                UI[0].SetActive(true);
                UI[1].SetActive(false);
                UI[2].SetActive(false);
                UIBG.SetActive(true);
            }
            else if (this.gameObject.name == "g2")
            {
                UI[0].SetActive(false);
                UI[1].SetActive(true);
                UI[2].SetActive(false);
            }
            else if(this.gameObject.name == "g3")
            {
                UI[0].SetActive(false);
                UI[1].SetActive(false);
                UI[2].SetActive(true);
            }
        }
    }
}
