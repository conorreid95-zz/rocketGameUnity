using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBox : MonoBehaviour
{

    public GameObject rocketShip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        switch (other.gameObject.tag) //switch on tag of object the rocket has collided with
        {
            case "Player":
                print("encountered player");
                rocketShip.GetComponent<Rocket>().mainThrust = 6000;
                rocketShip.GetComponent<Rocket>().rcsThrust = 360;

                

                break;
            default:
                break;

        }

    }
}
