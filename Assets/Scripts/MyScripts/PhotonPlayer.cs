using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    public PhotonView PV;
    public GameObject myAvatar;
    public int mySpawn;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            PV.RPC("RPC_GetSpawn", RpcTarget.MasterClient);
        }
    }

    private void Update()
    {
        if (myAvatar == null && mySpawn != 0)
        {
            if (PV.IsMine)
            {
                myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Tank"),
                    GameSetup.GS.spawnPoints[mySpawn-1].position, GameSetup.GS.spawnPoints[mySpawn-1].rotation, 0);
                Debug.Log("Going into change material");
                PV.RPC("RPC_changeMaterial", RpcTarget.AllBuffered, null);
                Debug.Log("Exit change material");
            }
        }
    }

    [PunRPC]
    void RPC_GetSpawn()
    {
        mySpawn = GameSetup.GS.nextSpawn;
        GameSetup.GS.UpdateSpawn();
        PV.RPC("RPC_SentSpawn", RpcTarget.OthersBuffered,mySpawn);
    }

    [PunRPC]
    void RPC_SentSpawn(int whichSpawn)
    {
        mySpawn = whichSpawn;
    }

    [PunRPC]
    public void RPC_changeMaterial()
    {
        Debug.Log("Inside change material");

        if (myAvatar != null)
        {
            for (int i = 0; i < myAvatar.transform.GetChild(0).childCount; i++)
            {
                    Renderer rend = myAvatar.transform.GetChild(0).GetChild(i).GetComponent<Renderer>();
                    rend.material = GameSetup.GS.tankMaterials[mySpawn - 1];
            }
        }
    }

}
