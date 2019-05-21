using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuinUISci : MonoBehaviour {

    [SerializeField]
    Image currenthb;
    [SerializeField]
    GameObject boss;
    [SerializeField]
    GameObject[] deactivateUI;


    private float hitpoints;
    private float maxhp;
    private float totalhp;
    // Use this for initialization
    void Start () {
        maxhp = 400;
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
            if (boss != null && boss.tag != "RuinsBoss")
            {
                foreach (var item in deactivateUI)
                {
                    if (item != null)
                    {
                        item.SetActive(false);
                    }
                }
                Destroy(boss);

            }
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
