using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUIScript : MonoBehaviour {


    [SerializeField]
    Image currenthb;

    private float hitpoints;
    private float maxhp;
    private float totalhp;
    // Use this for initialization
    void Start () {
        maxhp = 1000;
        hitpoints = maxhp;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Damage(20f);
            Debug.Log("Damage");
        }
        updatehp();
    }


    public void Damage(float dmg)
    {
        Debug.Log("damagenato");
        hitpoints = hitpoints - dmg;
    }

    void updatehp()
    {
        if (currenthb)
            currenthb.fillAmount = hitpoints / maxhp;
    }
}
