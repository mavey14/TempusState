using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuinUISci : MonoBehaviour {

    [SerializeField]
    Image currenthb;
    [SerializeField]
    GameObject boss;


    private float hitpoints;
    private float maxhp;
    private float totalhp;
    // Use this for initialization
    void Start () {
        maxhp = 1;
        hitpoints = maxhp;
    }
	
	// Update is called once per frame
	void Update () {
        updatehp();

        if (Input.GetKeyDown(KeyCode.L))
        {
            Damage(10f);
        }
	}

    public void Damage(float dmg)
    {
        hitpoints = hitpoints - dmg;
        if (hitpoints <= 0)
        {
            Debug.Log("damage");
            if (boss!=null&&boss.tag!="RuinsBoss")
            Destroy(boss);
            else
            {
                Debug.Log("death");
                boss.GetComponent<RuinBoss>().Death();
                Destroy(boss,1.5f);
            }
            Dimension.isbattle = false;

            Destroy(gameObject);

        }
    }

    void updatehp()
    {
        if(currenthb)
            currenthb.fillAmount = hitpoints / maxhp;
    }
}
