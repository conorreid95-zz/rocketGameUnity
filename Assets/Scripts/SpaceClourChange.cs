using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceClourChange : MonoBehaviour
{
    Renderer rend;
    Material white_Material;

    // Start is called before the first frame update
    void Start()
    {
         //Fetch the Renderer from the GameObject
        rend = GetComponent<Renderer>();

        white_Material = GetComponent<Renderer>().material;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            white_Material.color = Color.red;
        }
        else
        {
            white_Material.color = Color.white;

        }
        
    }
}
