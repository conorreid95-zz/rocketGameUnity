using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRocketRotation : MonoBehaviour
{
    //values that will be set in the Inspector
    Transform Target;
    public float RotationSpeed;

    // Update is called once per frame
    void Update()
    {
        //find the vector pointing from our position to the target
        //_direction = (Target.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        //_lookRotation = Quaternion.LookRotation(_direction);

        if (transform.rotation.eulerAngles.x > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0f, 0f, 0f, 1f), Time.deltaTime * RotationSpeed);

        }
        else if(transform.rotation.eulerAngles.y > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0f, 0f, 0f, 1f), Time.deltaTime * RotationSpeed);
        }
        else if (transform.rotation.eulerAngles.z > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0f, 0f, 0f, 1f), Time.deltaTime * RotationSpeed);
        }

        //print(transform.rotation.eulerAngles);
        //rotate us over time according to speed until we are in the required rotation
        
    }
}
