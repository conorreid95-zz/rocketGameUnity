using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlower : MonoBehaviour
{
    public bool slowAcive = true;
    public float slowAmount = 0.5f;
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
        if (slowAcive)
        {


            switch (other.gameObject.tag) //switch on tag of object the rocket has collided with
            {
                case "Player":
                    Time.timeScale = slowAmount;
                    Time.fixedDeltaTime = 0.02F * Time.timeScale;

                    //Time.fixedDeltaTime = 0.25f;
                    //print("Triggered by player, slowdown active");
                    break;
                default:
                    break;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (slowAcive)
        {


            switch (other.gameObject.tag) //switch on tag of object the rocket has collided with
            {
                case "Player":
                    Time.timeScale = 1f;
                    Time.fixedDeltaTime = 0.02f;
                    //Time.fixedDeltaTime = 0.25f;
                    //print("Triggered by player, slowdown active");
                    break;
                default:
                    break;

            }
        }
    }

    
}
