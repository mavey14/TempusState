using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GMScript : MonoBehaviour {

    public bool isgamepause;
    public bool timestop;
    int difficulty;
    public int cskill;
    [SerializeField]
    private GameObject uiportal;


    // Use this for initialization
    void Start () {
        timestop = false;
        cskill = 0;
	}
	
	
    public void NormalMode()
    {
        difficulty = 0;
    }

    public void ApocalypseMode()
    {
        difficulty = 1;
    }

    public void PlayGame()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
          
            uiportal.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            
            uiportal.SetActive(false);
        }
    }
}
