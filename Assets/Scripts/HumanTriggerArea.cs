using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HumanTriggerArea : MonoBehaviour
{
    public bool enableAbility; 

    public TextMeshProUGUI transformText; 
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.GetChild(0).GetComponent<Canvas>().enabled = false;
        enableAbility = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enableAbility && Input.GetKeyDown(KeyCode.E))
        {
            transformAbility();
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            enableAbility = true;
            gameObject.transform.GetChild(0).GetComponent<Canvas>().enabled = true;
        }

   
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            enableAbility = false;
            gameObject.transform.GetChild(0).GetComponent<Canvas>().enabled = false;
        }


    }


    void transformAbility()
    {

        if (PlayerControl.instance.isHuman == true)
        {
            PlayerControl.instance.isHuman = false;
        }

        else if (PlayerControl.instance.isHuman == false)
        {
            PlayerControl.instance.isHuman = true;
        }
   }
      
 

}
