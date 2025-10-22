using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfShortcut : MonoBehaviour
{
    private bool pressed = false;

    [SerializeField] private GameObject[] toDestroy;
    [SerializeField] private GameObject[] itemsToGain;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Space) && Input.GetKeyDown(KeyCode.M) && !pressed)
        {
            pressed = true;
            
            for (int i = 0; i < itemsToGain.Length; i++)
            {
                if(itemsToGain[i] != null)
                {
                    itemsToGain[i].GetComponent<ItemControl>().OnButtonClick();
                }
            }

            for (int i = 0; i < toDestroy.Length; i++)
            {
                if(toDestroy[i] != null)
                {
                    Destroy(toDestroy[i]);
                }
            }
        }
    }
}
