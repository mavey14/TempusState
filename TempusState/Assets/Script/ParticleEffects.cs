using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffects : MonoBehaviour {

    [SerializeField]
    GameObject[] skillseffect;
	// Use this for initialization
	void Start () {
        foreach (var skills in skillseffect)
        {
            Destroy(skills, skills.GetComponent<ParticleSystem>().main.duration +
            skills.GetComponent<ParticleSystem>().main.startLifetime.constantMax);
        }
        Destroy(this.gameObject, skillseffect[1].GetComponent<ParticleSystem>().main.duration +
            skillseffect[1].GetComponent<ParticleSystem>().main.startLifetime.constantMax);

    }
	

}
