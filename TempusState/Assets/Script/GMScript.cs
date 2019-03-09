﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GMScript : MonoBehaviour {

    public bool isgamepause;
    public bool timestop;
    //int difficulty;
    public int cskill;
    [SerializeField]
    private GameObject uiportal;


    // Use this for initialization
    void Start () {
        timestop = false;
        cskill = 0;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
           // Debug.Log("asda");
            SceneManager.LoadScene("GraveyardStages");
           // SceneManager.LoadScene("Graveyard");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("Graveyard");
          //  SceneManager.LoadScene("GraveyardStages");
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene("Ruins");
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene("VolcanoBoss");
        }
    }
    public void NormalMode()
    {
        //difficulty = 0;
    }

    public void ApocalypseMode()
    {
        //difficulty = 1;
    }

    public void PlayGame()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeCemeterySceene()
    {
        SceneManager.LoadScene("Graveyard");
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
