using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float rotationSpeedPerSecond;
    [SerializeField] bool rotationEnabled = true;
    [SerializeField] Vector3 rotateAroundPoint;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationEnabled)
        {
            transform.RotateAround(rotateAroundPoint, Vector3.back, Time.deltaTime * rotationSpeedPerSecond);
        }
        else
        {

        }
    }
}
