using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameObject[] objects;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    private void Awake()
    {
        objects = GameObject.FindGameObjectsWithTag("BackgroundMusic"); //find all background music objects
    }
    private void Start()
    {
        if (objects.Length == 1)
        {
            Destroy(objects[0]);
        }
    }
    



}
