using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectsBehevior : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag=="Player"&&gameObject.name=="Poison")
        {
            other.GetComponent<PlayerUIScript>().Poison();
        }

        if (other.tag == "Player" && gameObject.tag == "Ava")
        {
            other.GetComponent<PlayerUIScript>().Damage(5f);
        }
    }

}
