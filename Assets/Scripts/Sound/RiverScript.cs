using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RiverScript : MonoBehaviour

{
    public float max;
    public float min;
    public static RiverScript instance;
    public AudioSource RiverSound;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (RiverSound != null)
        {
            if (collision.tag == "Player")
            {

                RiverSound.volume = max;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (RiverSound != null)
        {
            RiverSound.volume = min;
        }
    }
}
