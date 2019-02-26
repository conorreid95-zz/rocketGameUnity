using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSlowMotionBox : MonoBehaviour
{
    public GameObject slowMotionBox;
    // Start is called before the first frame update
    void Start()
    {

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
                slowMotionBox.GetComponent<TimeSlower>().slowAcive = true;
                print("Time Slowed");
                Behaviour halo = (Behaviour)GetComponent("Halo");
                halo.enabled = false;
                break;
            default:
                break;
        }


    }
}
