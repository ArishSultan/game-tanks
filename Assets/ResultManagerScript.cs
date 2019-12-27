using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManagerScript : MonoBehaviour
{
    public Joystick m_JoyStick;
    public Image m_JoyButton;
    public GameObject m_ResultPanel;
    public PhotonView PV;

    void Start()
    {
        this.m_ResultPanel.SetActive(false);
        this.m_JoyButton.enabled = true;
        this.m_JoyStick.enabled = true;
    }
    
    public void EndMatch()
    {
        this.m_ResultPanel.SetActive(true);
        this.m_JoyButton.enabled = false;
        this.m_JoyStick.enabled = false;
    }

    public void Restart()
    {
        this.m_ResultPanel.SetActive(false);
        this.m_JoyButton.enabled = true;
        this.m_JoyStick.enabled = true;

        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().name);
    }
}
