﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GMScript : MonoBehaviour {

    public bool isgamepause;
    public bool timestop;
    int difficulty;
    public int cskill;
   

    // Use this for initialization
    void Start () {
        timestop = false;
        cskill = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
