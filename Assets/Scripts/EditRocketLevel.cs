using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditRocketLevel : MonoBehaviour
{

    [SerializeField] GameObject rocket;
    [SerializeField] float rotationSpeed = 40f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rocket.GetComponent<Transform>().Rotate(Vector3.up, rotationSpeed* Time.deltaTime);
    }
}
