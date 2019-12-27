using System.IO;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{         
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;         
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;
    private PhotonView PV;
    private Joybutton _joybutton;

    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired = true;                


    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
        _joybutton = FindObjectOfType<Joybutton>();
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
        PV = GetComponent<PhotonView>();
    }


    private void Update()
    {
        if (PV.IsMine)
        {
            // Track the current state of the fire button and make decisions based on the current launch force.
            m_AimSlider.value = m_MinLaunchForce;

            if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
            {
                // At max charge, not yet fired.
                m_CurrentLaunchForce = m_MaxLaunchForce;
                Fire();

            }
            else if (_joybutton.IsPressed)
            {
                // Have we pressed the Fire button for the first time?
                m_Fired = false;
                m_CurrentLaunchForce = m_MinLaunchForce;

                m_ShootingAudio.clip = m_ChargingClip;
                m_ShootingAudio.Play();

            }
            else if (_joybutton.IsPressed && !m_Fired)
            {
                // Holding the fire button, not yet fired.
                m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;
                m_AimSlider.value = m_CurrentLaunchForce;

            }
            else if (!_joybutton.IsPressed && !m_Fired)
            {
                // We releasted the fire button, having not fired yet.
                Fire();
            }
        }
    }


    private void Fire()
    {
        // Instantiate and launch the shell.
		m_Fired = true;

        GameObject  myShell = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Shell"),
                m_FireTransform.position,m_FireTransform.rotation, 0);

        Rigidbody myShellInstance = myShell.GetComponent<Rigidbody>(); 

		myShellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

		m_ShootingAudio.clip = m_FireClip;
		m_ShootingAudio.Play();

		m_CurrentLaunchForce = m_MinLaunchForce;
    }
}