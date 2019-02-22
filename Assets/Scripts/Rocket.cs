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
    [SerializeField] ParticleSystem rightBoostParticle;
    [SerializeField] ParticleSystem leftBoostParticle;

    public ConstantForce gravity;

    enum State { Alive, Dying, Trancending }

    State state = State.Alive;

    float levelLoadDelay = 1.5f;

    bool invincable = false;

    //bool ControlEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidBody = GetComponent<Rigidbody>();

        audioData = GetComponent<AudioSource>();

        gameObject.GetComponent<Rigidbody>().useGravity = false;

        gravity = gameObject.AddComponent<ConstantForce>();
        gravity.force = new Vector3(0.0f, -35f, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        CheckDebugKeys();
        SetThrust();
        SetRotation();
    }


    private void OnCollisionEnter(Collision collision) //executes on collision with another gameobject
    {
        if(state != State.Alive) { return; }

        switch (collision.gameObject.tag) //switch on tag of object the rocket has collided with
        {
            case "Friendly":
                break;
            case "LaunchPad":
                break;
            case "LandingPad":
                state = State.Trancending;
                audioData.Stop();
                victoryParticle.Play();
                audioData.PlayOneShot(victorySound);
                Invoke("LoadNextLevel", levelLoadDelay);
                break;
            default:
                if (invincable)
                {
                    break;
                }
                else
                {
                    state = State.Dying;
                    audioData.Stop();
                    deathParticle.Play();

                    if (rightBoostParticle.isPlaying) { rightBoostParticle.Stop(); }
                    if (leftBoostParticle.isPlaying) { leftBoostParticle.Stop(); }

                    audioData.PlayOneShot(deathSound);
                    //ControlEnabled = false;
                    Invoke("LoadCurrentLevel", levelLoadDelay);
                    break;
                }
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



    private void SetRotation()
    {

        rocketRigidBody.freezeRotation = true; //freeze rotation before applying calculated rotation

        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (state == State.Alive) //if rcket is still being controlled
        {
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) //if trying to rotate in both directions
            {
                print("Can't Rotate Both Ways at The Same Time!");
            }
            else if (Input.GetKey(KeyCode.A)) //if rotating left with a (a being held down)
            {
                transform.Rotate(Vector3.forward * rotationSpeed);

                if (rightBoostParticle.isPlaying) { } //if boost particle effect is already playing do nothing, don't want to restart animation
                else
                {
                    rightBoostParticle.Play(); //if not playing, play
                }

                if (leftBoostParticle.isPlaying) //if rotating one way, stop particle effect for the other rotation if its playing
                {
                    leftBoostParticle.Stop();
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(-Vector3.forward * rotationSpeed); //apply rotation

                if (leftBoostParticle.isPlaying) { } //same as above for other rotation
                else
                {
                    leftBoostParticle.Play();
                }

                if (rightBoostParticle.isPlaying)
                {
                    rightBoostParticle.Stop();
                }
            }
            else //if no rotations, stop all sie boost particle effects if playing
            {
                if (rightBoostParticle.isPlaying) { rightBoostParticle.Stop(); }
                if (leftBoostParticle.isPlaying) { leftBoostParticle.Stop(); }
            }
        }

        rocketRigidBody.freezeRotation = false; //unfreeze rotation once calculation is applied
    }

    private void SetThrust()
    {

        float mainThrustSpeed = mainThrust * Time.deltaTime;

        if (state == State.Alive)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rocketRigidBody.AddRelativeForce(Vector3.up * mainThrustSpeed);

                mainEngineParticle.Play();

                if (!audioData.isPlaying)
                {
                    audioData.PlayOneShot(mainEngineSound);
                }

            }
            else
            {
                mainEngineParticle.Stop();
                audioData.Pause();
            }
        }
    }

    private void CheckDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            state = State.Trancending;
            Invoke("LoadNextLevel", 0.05f);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {

            invincable = !invincable;
            if (invincable)
            {
                print("Invincable Mode ON");
            }
            else
            {
                print("Invincable Mode OFF");
            }
            
        }

    }
}



