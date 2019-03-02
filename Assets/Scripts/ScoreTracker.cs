using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public int deathCount = 0;
    GameObject playerRocket;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        playerRocket = GameObject.Find("RocketShip");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
