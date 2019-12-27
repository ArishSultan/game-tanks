using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView PV;
    private CharacterController myCC;
    public float movSpeed;
    public float rotationSpeed;
    public bool isp1;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        myCC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
            Move();
            Rotation(); 
    }


    void Move()
    {
        if (isp1)
        {
            if (Input.GetKey(KeyCode.W))
            {
                myCC.Move(transform.forward * Time.deltaTime * movSpeed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                myCC.Move(-transform.right * Time.deltaTime * movSpeed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                myCC.Move(-transform.forward * Time.deltaTime * movSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                myCC.Move(transform.right * Time.deltaTime * movSpeed);
            }
        }
        else
        {

            if (Input.GetKey(KeyCode.I))
            {
                myCC.Move(transform.forward * Time.deltaTime * movSpeed);
            }
            if (Input.GetKey(KeyCode.J))
            {
                myCC.Move(-transform.right * Time.deltaTime * movSpeed);
            }
            if (Input.GetKey(KeyCode.K))
            {
                myCC.Move(-transform.forward * Time.deltaTime * movSpeed);
            }
            if (Input.GetKey(KeyCode.L))
            {
                myCC.Move(transform.right * Time.deltaTime * movSpeed);
            }
        }


    }

    void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        transform.Rotate(new Vector3(0, mouseX, 0));
    }
}
