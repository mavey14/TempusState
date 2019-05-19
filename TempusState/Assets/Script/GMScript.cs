using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GMScript : MonoBehaviour {

    public static bool isgamepause;
    public bool timestop;
    public static bool isgame;
    public static bool[] stages;
    public static bool[] skills;
    public static int difficulty;
    public static bool istutorialdone;
    public int cskill;
    [SerializeField]
    private GameObject uiportal;
    public GameObject[] loadingScreen;
    [SerializeField]
    private GameObject EscMenu;
    Scene currentScene;
    int sceneIndex;
    [SerializeField]
    GameObject[] puzz;
    bool skill1;

    // Use this for initialization
    void Start () {
        timestop = false;
        cskill = 0;
        istutorialdone = false;
       // isgame = isgamepause =false;
        stages= new bool[3];
        skills = new bool[3];
        stages[0]= stages[1] = stages[2] = false;
        skills[0] = skills[1] = skills[2] = false;
        skills[0] = stages[0] =  true;
        currentScene = SceneManager.GetActiveScene();
        sceneIndex = currentScene.buildIndex;
        if (sceneIndex == 1)
        {
            if (Audiomanager.cambience != null)
            {
                FindObjectOfType<Audiomanager>().Stop(Audiomanager.cambience);
            }
           // FindObjectOfType<Audiomanager>().Play("GAmbience");
            puzz[/*Random.Range(1, 3)*/0].SetActive(true);

        }
        else if (sceneIndex == 2)
        {
            if (Audiomanager.cambience != null)
            {
                FindObjectOfType<Audiomanager>().Stop(Audiomanager.cambience);
            }
            //FindObjectOfType<Audiomanager>().Play("GAmbience");
        }
        else if (sceneIndex == 3)
        {
            if (Audiomanager.cambience != null)
            {
                FindObjectOfType<Audiomanager>().Stop(Audiomanager.cambience);
            }
            FindObjectOfType<Audiomanager>().Play("RAmbience");
        }
        else if (sceneIndex == 4)
        {
            if (Audiomanager.cambience != null)
            {
                FindObjectOfType<Audiomanager>().Stop(Audiomanager.cambience);
            }
            FindObjectOfType<Audiomanager>().Play("VAmbience");
        }

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
             Debug.Log("test");
            if (EscMenu != null)
            {
                if (isgame && !EscMenu.activeSelf)
                {
                    Debug.Log("test");
                    EscMenu.SetActive(true);
                }
                else if (isgame && EscMenu.activeSelf)
                {
                    Debug.Log("test");
                    EscMenu.SetActive(false);
                }
            }
         

        }

        

        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    SavePlayer();
        //}
        //else if (Input.GetKeyDown(KeyCode.F2))
        //{
        //    LoadPlayer();
        //}

        //if (Input.GetKeyDown(KeyCode.F3))
        //{
        //    stages[0] = true;
        //    stages[1] = true;
        //    stages[2] = false;
        //}

        //if (Input.GetKeyDown(KeyCode.F4))
        //{
        //    skills[0] = true;
        //    skills[1] = true;
        //    skills[2] = false;
        //}
    }

    public void SavePlayer()
    {

        SaveSystem.SavePlayer(this);
        Debug.Log("Saved");
    }

    public void LoadPlayer()
    {
        playerdata data = SaveSystem.loadPlayer();
        Debug.Log("Stages");
       // Debug.Log(data.stage[0].ToString());
       // Debug.Log(data.stage[1].ToString());
       // Debug.Log(data.stage[2].ToString());
       // Debug.Log("Skills");
       // Debug.Log(data.skils[0].ToString());
        //Debug.Log(data.skils[1].ToString());
       // Debug.Log(data.skils[2].ToString());

        //public bool[] stages;
        //public bool[] skills;

        stages[0] = data.stage[0];
        stages[1] = data.stage[1];
        stages[2] = data.stage[2];

        skills[0] = data.skils[0];
        skills[1] = data.skils[1];
        skills[2] = data.skils[2];

        

        difficulty = data.difficulty;
    }


    //SAVING 
    //public void SavePlayer()
    //{
    //    SaveSystem.SavePlayer(this);
    //}

    //public void LoadPlayer()
    //{
    //    playerdata data = SaveSystem.loadPlayer();
    //    clevel = data.level;
    //    chealth = data.health;
    //    Vector3 position;

    //    position.x = data.position[0];
    //    position.y = data.position[1];
    //    position.z = data.position[2];

    //    mousepos = position;
    //}

    public void removeCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

     
        isgame = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void LoadLevel(int sceneIndex)
    {

        StartCoroutine(LoadAscynchronously(sceneIndex));
    }

    IEnumerator LoadAscynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager
            .LoadSceneAsync(sceneIndex);
        loadingScreen[0].SetActive(true);
        while (!operation.isDone)
        {
            //float progress = Mathf.Clamp01(operation.progress / .9f);
            //slider.value = progress;
            //progressText.text = progress * 100f + "%";
            yield return null;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player"&&gameObject.name== "LavaPortal"&& stages[2]==false)
        {
          
            uiportal.SetActive(true);
        }
        else if (other.tag == "Player" && gameObject.name == "Ruinsportal" && stages[1] == false)
        {

            uiportal.SetActive(true);
        }
        else if (other.tag == "Player" && gameObject.name == "CemeteryPortal" && stages[0] == false)
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
