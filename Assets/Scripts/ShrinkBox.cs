using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkBox : MonoBehaviour
{
    [SerializeField] AudioClip shrinkSound;
    AudioSource audioData;

    [SerializeField] GameObject rocketShip;
    [SerializeField] GameObject flameEffect;
    [SerializeField] GameObject sideSmokeLeft;
    [SerializeField] GameObject sideSmokeRight;
    [Range(0.01f, 10f)] [SerializeField] float shrinkFactor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        switch (collision.gameObject.tag) //switch on tag of object the rocket has collided with
        {
            case "Player":
                print("encountered player");
                rocketShip.transform.localScale = new Vector3(1f * shrinkFactor, 2.5f * shrinkFactor, 0.8f * shrinkFactor);
                flameEffect.transform.localScale = new Vector3(0.7f * shrinkFactor, 1f * shrinkFactor, 1.2f * shrinkFactor);
                sideSmokeLeft.transform.localScale = new Vector3(0.05f * shrinkFactor, 0.1f * shrinkFactor, 0.1f * shrinkFactor);
                sideSmokeRight.transform.localScale = new Vector3(0.05f * shrinkFactor, 0.1f * shrinkFactor, 0.1f * shrinkFactor);
                Behaviour halo = (Behaviour)GetComponent("Halo");
                halo.enabled = false;

                Destroy(GetComponent<BoxCollider>());
                Destroy(GetComponent<MeshRenderer>());

                audioData.PlayOneShot(shrinkSound);
                break;
                default:
                    break;

            }
        
    }
}
