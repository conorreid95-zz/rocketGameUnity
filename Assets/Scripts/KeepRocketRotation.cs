using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRocketRotation : MonoBehaviour
{
    //values that will be set in the Inspector
    Transform Target;
    public float RotationSpeed;
    [SerializeField] bool stableRotationEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if (stableRotationEnabled)
        {


            //check axies to see if correction is needed, then apply
            if (transform.rotation.eulerAngles.x > 0.1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0f, 0f, 0f, 1f), Time.deltaTime * RotationSpeed);

            }
            else if (transform.rotation.eulerAngles.y > 0.1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0f, 0f, 0f, 1f), Time.deltaTime * RotationSpeed);
            }
            /*
            else if (transform.rotation.eulerAngles.z > 0.1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0f, 0f, 0f, 1f), Time.deltaTime * RotationSpeed);
            }
            */
        }
        
    }
}
