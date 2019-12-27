using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;
    public Transform[] spawnPoints;
    public Material[] tankMaterials;
    public int nextSpawn;

    private void OnEnable()
    {
        if(GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }
    }

    public void UpdateSpawn()
    {
        if(nextSpawn == 1)
        {
            nextSpawn = 2;
        }
        else if (nextSpawn == 2)
        {
            nextSpawn = 3;
        }
        else if(nextSpawn == 3)
        {
            nextSpawn = 4;
        }
        else if (nextSpawn == 4)
        {
            nextSpawn = 1;
        }
    }
}
