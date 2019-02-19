using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rocketRigidBody;

    AudioSource audioData;

    [SerializeField] float rcsThrust = 250f;
    [SerializeField] float mainThrust = 4200f;

    [SerializeField] AudioClip mainEngineSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip victorySound;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem victoryParticle;

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

    enum State { Alive, Dying, Trancending }

    State state = State.Alive;

    //bool ControlEnabled = true;

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
        gravity.force = new Vector3(0.0f, -35f, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        getThrust();

        getRotation();
    }


    private void OnCollisionEnter(Collision collision) //executes on collision with another gameobject
    {
        if(state != State.Alive) { return; }

        switch (collision.gameObject.tag) //switch on tag of object the rocket has collided with
        {
            case "Friendly":
                print("Collided with friendly object");
                break;
            case "LaunchPad":
                print("Collided with launch pad");
                break;
            case "LandingPad":
                print("Collided with landing pad");
                state = State.Trancending;
                audioData.Stop();
                victoryParticle.Play();
                audioData.PlayOneShot(victorySound);
                Invoke("LoadNextLevel", 1f);
                break;
            default:
                print("Dead");
                state = State.Dying;
                audioData.Stop();
                deathParticle.Play();
                audioData.PlayOneShot(deathSound);
                //ControlEnabled = false;
                Invoke("LoadCurrentLevel", 1f);
                break;

        }

    }

    private void LoadCurrentLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(0);
            state = State.Alive;
            // ControlEnabled = true;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(1);
            state = State.Alive;
            //ControlEnabled = true;
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
        state = State.Alive;
        //ControlEnabled = true;
    }

    private void getRotation()
    {

        rocketRigidBody.freezeRotation = true;

        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (state == State.Alive)
        {
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
        }
        

        rocketRigidBody.freezeRotation = false;

    }

    private void getThrust()
    {

        float mainThrustSpeed = mainThrust * Time.deltaTime;

        if (state == State.Alive)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //print("Space Pressed!");
                rocketRigidBody.AddRelativeForce(Vector3.up * mainThrustSpeed);

                mainEngineParticle.Play();

                if (!audioData.isPlaying)
                {
                    audioData.PlayOneShot(mainEngineSound);
                }

                spaceKeyMaterial.color = Color.red;
            }
            else
            {
                spaceKeyMaterial.color = Color.white;
                mainEngineParticle.Stop();
                audioData.Pause();
            }
        }
    }
}



