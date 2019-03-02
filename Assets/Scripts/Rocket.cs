using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    GameObject rightBooster;
    GameObject leftBooster;
    GameObject body2;
    GameObject noseCone;

    GameObject universeMusic;

    GameObject gameController;

    Rigidbody rocketRigidBody;

    AudioSource audioData;

    public float rcsThrust = 280f;
    public float mainThrust = 3500f;

    [SerializeField] AudioClip mainEngineSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip victorySound;
    [SerializeField] AudioClip victorySoundLast;
    [SerializeField] AudioClip leverSound;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem mainEngineLight;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem victoryParticle;
    [SerializeField] ParticleSystem rightBoostParticle;
    [SerializeField] ParticleSystem leftBoostParticle;

    GameObject embers;
    GameObject bigExplosion;

    public ConstantForce gravity;

    enum State { Alive, Dying, Trancending }

    State state = State.Alive;

    float levelLoadDelay = 1.5f;

    bool invincable = false;

    int currentSceneIndex;

    //bool ControlEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        rightBooster = GameObject.Find("SideBooster1");
        leftBooster = GameObject.Find("SideBooster2");
        body2 = GameObject.Find("Body2");
        noseCone = GameObject.Find("NoseCone");

        embers = GameObject.Find("FireEmbers (3)");
        bigExplosion = GameObject.Find("BigExplosion");

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        gameController = GameObject.Find("GameController");

        Application.targetFrameRate = 120;

        if (currentSceneIndex > 0)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }

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
            case "LandingPadLast":
                state = State.Trancending;
                audioData.Stop();
                victoryParticle.Play();
                audioData.PlayOneShot(victorySoundLast);
                Invoke("LoadMenuLevel", 3f);
                break;
            case "Lever":
                audioData.PlayOneShot(leverSound);
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

                    if (gameController != null)
                    {
                        gameController.GetComponent<ScoreTracker>().deathCount++;
                        print(gameController.GetComponent<ScoreTracker>().deathCount);
                    }

                    DetaachRocketObjects(); //function makes rocket boosters and body detach to simulate explosion

                    if (rightBoostParticle.isPlaying) { rightBoostParticle.Stop(); }
                    if (leftBoostParticle.isPlaying) { leftBoostParticle.Stop(); }

                    audioData.PlayOneShot(deathSound);
                    //ControlEnabled = false;
                    Invoke("LoadCurrentLevel", levelLoadDelay);
                    break;
                }
        }

    }

    private void DetaachRocketObjects()
    {
        body2.transform.parent = null;
        body2.AddComponent<Rigidbody>();
        body2.GetComponent<Rigidbody>().AddExplosionForce(2000f, body2.transform.localPosition, 20f, 20f);

        rightBooster.transform.parent = null;
        rightBooster.AddComponent<Rigidbody>();

        leftBooster.transform.parent = null;
        leftBooster.AddComponent<Rigidbody>();

        noseCone.transform.parent = null;
        noseCone.AddComponent<Rigidbody>();
        //noseCone.GetComponent<Rigidbody>().AddForce(transform.right * 50f);


        embers.transform.parent = null;
        bigExplosion.transform.parent = null;
    }

    private void LoadCurrentLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
       
    }

    private void LoadMenuLevel()
    {
        try
        {
            universeMusic = GameObject.Find("Universe Music");
        }
        catch (Exception)
        {
            throw;
        }
        
        if (universeMusic.GetComponent<AudioSource>().isPlaying)
        {
            Destroy(universeMusic);
        }
        SceneManager.LoadScene(0);
        state = State.Alive;
     
    }

    private void LoadNextLevel()
    {
        
        if ((SceneManager.sceneCountInBuildSettings - 1) > currentSceneIndex)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
            state = State.Alive;
        }
        else
        {
            SceneManager.LoadScene(0);
        }

    }



    private void SetRotation()
    {
        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (state == State.Alive) //if rcket is still being controlled
        {
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) //if trying to rotate in both directions
            {
                print("Can't Rotate Both Ways at The Same Time!");
                if (rightBoostParticle.isPlaying) { rightBoostParticle.Stop(); }
                if (leftBoostParticle.isPlaying) { leftBoostParticle.Stop(); }

            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.RightArrow)) //if trying to rotate in both directions
            {
                print("Can't Rotate Both Ways at The Same Time!");
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftArrow)) //if trying to rotate in both directions
            {
                print("Can't Rotate Both Ways at The Same Time!");
            }
            else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow)) //if trying to rotate in both directions
            {
                print("Can't Rotate Both Ways at The Same Time!");
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) //if rotating left with a (a being held down)
            {
                rocketRigidBody.freezeRotation = true; //freeze rotation before applying calculated rotation
                transform.Rotate(Vector3.forward * rotationSpeed);
                rocketRigidBody.freezeRotation = false;   //unfreeze rotation once calculation is applied

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
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rocketRigidBody.freezeRotation = true; //freeze rotation before applying calculated rotation
                transform.Rotate(-Vector3.forward * rotationSpeed); //apply rotation
                rocketRigidBody.freezeRotation = false;   //unfreeze rotation once calculation is applied

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

        
    }

    private void SetThrust()
    {

        float mainThrustSpeed = mainThrust * Time.deltaTime;

        if (state == State.Alive && currentSceneIndex != 0)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
            {
                rocketRigidBody.AddRelativeForce(Vector3.up * mainThrustSpeed);

                if (!mainEngineParticle.isPlaying)
                {
                    mainEngineParticle.Play();
                }
                if (!mainEngineLight.isPlaying)
                {
                    mainEngineLight.Play();
                }

                if (!audioData.isPlaying)
                {
                    audioData.PlayOneShot(mainEngineSound);
                }

            }
            else
            {
                mainEngineParticle.Stop();
                mainEngineLight.Stop();
                audioData.Stop(); //stop better that pause
            }
        }
    }

    private void CheckDebugKeys()
    {
        if (Input.GetKey("escape") || Input.GetKey(KeyCode.Q))
        {
            LoadMenuLevel();
        }

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



