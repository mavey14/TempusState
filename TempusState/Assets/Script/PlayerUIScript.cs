using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour {

    [SerializeField]
    Image currenthb;

    private float hitpoints;
    private float maxhp;
    private float totalhp;
	// Use this for initialization
	void Start () {
        maxhp = 150;
        hitpoints = maxhp;
        //currenthb = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.P))
        {
            Damage(20f);
            Debug.Log("Damage");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Damage(20f);
            Debug.Log("Damage");
        }
        Debug.Log(hitpoints);
        
    }

    void Damage(float dmg)
    {
        Debug.Log("damagenato");
        hitpoints = hitpoints - dmg;
      
    }

    void updatehp()
    {
        currenthb.fillAmount = hitpoints / maxhp;
    }
}
