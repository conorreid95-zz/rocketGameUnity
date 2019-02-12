using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rocketRigidBody;

    AudioSource audioData;

    [SerializeField] float rcsThrust = 250f;

    [SerializeField] float mainThrust = 4500f;

    public ConstantForce gravity;

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

        audioData = GetComponent<AudioSource>();


        rendSpace = spaceKeyObject.GetComponent<Renderer>();
        rendA = aKeyObject.GetComponent<Renderer>();
        rendD = dKeyObject.GetComponent<Renderer>();
        spaceKeyMaterial = spaceKeyObject.GetComponent<Renderer>().material;
        aKeyMaterial = aKeyObject.GetComponent<Renderer>().material;
        dKeyMaterial = dKeyObject.GetComponent<Renderer>().material;

        gameObject.GetComponent<Rigidbody>().useGravity = false;

        gravity = gameObject.AddComponent<ConstantForce>();
        gravity.force = new Vector3(0.0f, -40f, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        getThrust();

        getRotation();
    }



    private void getRotation()
    {

        rocketRigidBody.freezeRotation = true;

        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            print("Can't Rotate Both Ways at The Same Time!");
            aKeyMaterial.color = Color.white;
            dKeyMaterial.color = Color.white;

        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);

            //print("Left Rotation Pressed!");
            aKeyMaterial.color = Color.red;
            dKeyMaterial.color = Color.white;
        }
        else if (Input.GetKey(KeyCode.D))
        {

            //print("Right Rotation Pressed!");
            dKeyMaterial.color = Color.red;
            aKeyMaterial.color = Color.white;

            

            transform.Rotate(-Vector3.forward * rotationSpeed);

        }
        else
        {
            aKeyMaterial.color = Color.white;
            dKeyMaterial.color = Color.white;
        }

        rocketRigidBody.freezeRotation = false;

    }

    private void getThrust()
    {

        float mainThrustSpeed = mainThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            //print("Space Pressed!");
            rocketRigidBody.AddRelativeForce(Vector3.up * mainThrustSpeed);

            if (!audioData.isPlaying)
            {
                audioData.Play(0);
            }

            spaceKeyMaterial.color = Color.red;
        }
        else
        {
            spaceKeyMaterial.color = Color.white;
            audioData.Pause();
        }
    }
}



