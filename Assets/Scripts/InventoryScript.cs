using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
   
    public static InventoryScript instance;
    public TextMeshProUGUI numberText;
    public TextMeshProUGUI itemTitle;
    GameObject inventoryCanvas;
    int counter; 


    // Start is called before the first frame update
    void Start()
    {

        if (instance == null)
        {
            instance = this;
        }

        inventoryCanvas = GameObject.Find("InventorySlots"); //Canvas for Inventory Elements, which will become the parent of the slot

        gameObject.transform.SetParent(inventoryCanvas.transform); //Set it as the parent

        numberText.enabled = false;

        //setting the inventory name text to the right text 
        if (gameObject.transform.GetChild(1).GetComponent<Image>().sprite.name == "SilkObject")
        {
            itemTitle.text = "Silkworm";
        }
        
        else
        {
            itemTitle.text = gameObject.transform.GetChild(1).GetComponent<Image>().sprite.name;
        }



    }

    // Update is called once per frame
    void Update()
    {

        //check the number of items 
        itemIdentifier();
        setCounter();
       if (counter == 1)
        {
            numberText.enabled = false;
        }
        else if (counter >= 2)
        {
            numberText.enabled = true;
        }
       
    }

    public void itemIdentifier()
    {
        //which item number to update
        switch (gameObject.transform.GetChild(1).GetComponent<Image>().sprite.name)

        {
            case "Stick":
            
                numberText.text = ItemControl.stickCount.ToString();
           

                break;

            case "SilkObject":

                numberText.text = ItemControl.treeCount.ToString();

                break;

            case "Shard":

                numberText.text = ItemControl.shardCount.ToString();

                break;


            default:
                break;
        }
    }

    void setCounter()
    {
        switch (gameObject.transform.GetChild(1).GetComponent<Image>().sprite.name)
        {
            case "Stick":
                counter = ItemControl.stickCount;
                break;

            case "SilkObject":
                counter = ItemControl.treeCount;
                break;

            case "Shard":
                counter = ItemControl.shardCount;
                break;
        }
    }
}
