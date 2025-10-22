using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CaveMusic : MonoBehaviour
{
    public AudioSource caveSound;
    public float max;
    public float min;


    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            caveSound.volume = max;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
   
        if (collider.tag == "Player")
        {
           
          caveSound.volume = min;
        }
    }

}
