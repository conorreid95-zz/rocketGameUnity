using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBehaviour : MonoBehaviour
{
    GameObject slidingWall;

    [SerializeField] AudioClip leverSound;
    AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {

        audioData = GetComponent<AudioSource>();

        try
        {
            slidingWall = GameObject.Find("slidingWall");
        }
        catch (System.Exception)
        {

            throw;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision) //executes on collision with another gameobject
    {
        switch (collision.gameObject.tag) //switch on tag of object the rocket has collided with
        {
            case "Player":
                print("Touched by rocket");
                slidingWall.GetComponent<SimpleMovement>().enabled = true;
                print("Wall ENABLED");
                Behaviour halo = (Behaviour)GetComponent("Halo");
                halo.enabled = false;

                Destroy(GetComponent<BoxCollider>());
                Destroy(GetComponent<MeshRenderer>());

                audioData.PlayOneShot(leverSound);
                break;
            default:
                break;
        }
        

    }
}
