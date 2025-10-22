using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestAmbienceScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource forestAmbience;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (forestAmbience != null)
        {
            if (collider.tag == "Player")
            {

                forestAmbience.volume = 1.0f;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (forestAmbience != null)
        {


            if (collider.tag == "Player")
            {
                forestAmbience.volume = 0.0f;
            }
        }
    }

}
