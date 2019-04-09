using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class playerdata  {

    public bool[] stage = new bool[3];
    public bool[] skils =  new bool[3];
    public int difficulty;

    public playerdata(GMScript player)
    {
        difficulty = GMScript.difficulty;

        skils[0] = GMScript.skills[0];
        skils[1] = GMScript.skills[1];
        skils[2] = GMScript.skills[2];

        stage[0] = GMScript.stages[0];
        stage[1] = GMScript.stages[1];
        stage[2] = GMScript.stages[2];
        //skils[1] = skills[1];
        //skils[2] = skills[2];

        //stage[0] = stagess[0];
        //stage[1] = stagess[1];
        //stage[2] = stagess[2];
    }

    //SETTINGS
    //public int level;
    //public int health;
    //public float[] position;

    //public playerdata(PlayerPos player)
    //{
    //    level = player.clevel;
    //    health = player.chealth;

    //    position = new float[3];
    //    position[0] = player.mousepos.x;
    //    position[1] = player.mousepos.y;
    //    position[2] = player.mousepos.z;
    //}

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
}
