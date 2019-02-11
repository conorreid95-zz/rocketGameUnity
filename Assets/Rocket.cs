using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rocketRigidBody;


    Renderer rendSpace;
    Renderer rendA;
    Renderer rendD;
    Material spaceKeyMaterial;
    Material aKeyMaterial;
    Material dKeyMaterial;
    public GameObject spaceKeyObject;
    public GameObject aKeyObject;
    public GameObject dKeyObject;

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidBody = GetComponent<Rigidbody>();





        rendSpace = spaceKeyObject.GetComponent<Renderer>();
        rendA = aKeyObject.GetComponent<Renderer>();
        rendD = dKeyObject.GetComponent<Renderer>();
        spaceKeyMaterial = spaceKeyObject.GetComponent<Renderer>().material;
        aKeyMaterial = aKeyObject.GetComponent<Renderer>().material;
        dKeyMaterial = dKeyObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        
    }



    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //print("Space Pressed!");
            rocketRigidBody.AddRelativeForce(Vector3.up);

            spaceKeyMaterial.color = Color.red;
        }
        else
        {
            spaceKeyMaterial.color = Color.white;
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            print("Can't Rotate Both Ways at The Same Time!");
            aKeyMaterial.color = Color.white;
            dKeyMaterial.color = Color.white;

        }
        else if (Input.GetKey(KeyCode.A))
        {
            print("Left Rotation Pressed!");
            aKeyMaterial.color = Color.red;
            dKeyMaterial.color = Color.white;


        }
        else if(Input.GetKey(KeyCode.D))
        {
            print("Right Rotation Pressed!");
            dKeyMaterial.color = Color.red;
            aKeyMaterial.color = Color.white;
        }
        else
        {
            aKeyMaterial.color = Color.white;
            dKeyMaterial.color = Color.white;
        }

    }
}



