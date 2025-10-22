using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//Note: Whichever sprite you want to implement, create a counter variable for it, ex. silkwormCount and set it to 0 in start.
//When making an item from the prefabs, you will have to drag the desired sprite into the inspector
//In inventory script
//See OnButtonClick for more instructions

public class ItemControl : MonoBehaviour
   
{
    public Button getItem; // Button that pops up for taking item

    Rigidbody2D myItem;


    public Sprite itemSprite; //sprite for that item 
    public Sprite altItemSprite;
    public Sprite replacementSprite;


    public bool hasItem = false;
    public bool hasStick;
    public bool hasPearl;
    public bool hasMirror;    

    Vector3 cameraPos; //camera position 
    
    public static int stickCount;
    public static int treeCount;
    public static int numInventoryObjects;
    public static int shardCount;


   
    //working with inventory script

    public ItemControl instance; //Used to access inventory script 
    public GameObject itemSlot; //Item slot prefab that will be instantiated upon picking up a new item for the first time
    public RectTransform itemBox;

    public GameObject inventory;

    private bool inTrigger = false;

    //Audio
    public AudioSource ding; 


    //Item bools 
    //bool hasSilkworm;
    

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }


        myItem = gameObject.GetComponent<Rigidbody2D>();
        hasStick = false;
        hasMirror = false;  

        itemSprite = gameObject.GetComponent<SpriteRenderer>().sprite; //Get the sprite in the item 
      
        numInventoryObjects = 0;
        stickCount = 0; 
        treeCount = 0;
     
      /*  if (gameObject.GetComponent<SpriteRenderer>().sprite.name == "Mirror")
        {
            gameObject.SetActive(false);
        }
  
        */
    }
    
    // Update is called once per frame
    void Update()
    {
        cameraPos = Camera.main.transform.position;
        if (inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            OnButtonClick();
        }



        //mirror control
       // checkMirror();


    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            
            inTrigger = true;
          
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    
        inTrigger = false;
    }

    public void OnButtonClick()
    {
        ding.Play();
        //Add any item into this switch case based on the sprites name

        switch (itemSprite.name)
        {
            case "Stick":
                stickCount++; //Add to the counter
                             
                if (stickCount == 1) // if it is a new item, then it will create an inventory slot
                {
                  
                    numInventoryObjects++; //Add to the number of inventory objects
                    //CheckInventoryObjects(); //Check what position it goes to
                    GameObject newStickSlot = Instantiate(itemSlot, itemBox); //Create the slot
                    newStickSlot.transform.GetChild(1).GetComponent<Image>().sprite = itemSprite;
                    hasStick = true; //Assign the sprite to be displayed     
                }
                break;


            case "TreeSilk":
                
                treeCount++;
                if (treeCount == 1) // if it is a new item
                {
                    numInventoryObjects++;
                    //CheckInventoryObjects();
                    GameObject newTreeSlot = Instantiate(itemSlot, itemBox);
                    newTreeSlot.transform.GetChild(1).GetComponent<Image>().sprite = altItemSprite;
                }

                PlayerControl.instance.animator.SetTrigger("Jump");
                gameObject.GetComponent<SpriteRenderer>().sprite = replacementSprite;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                break;

            case "Pearl":
                numInventoryObjects++; //Add to the number of inventory objects
                                       //CheckInventoryObjects(); //Check what position it goes to
                GameObject newPearlSlot = Instantiate(itemSlot, itemBox); //Create the slot
                newPearlSlot.transform.GetChild(1).GetComponent<Image>().sprite = itemSprite;
                hasPearl = true; //Assign the sprite to be displayed    
                break;

            case "Shard":
                shardCount++;
                if (shardCount == 1) // if it is a new item
                {
                    
                    numInventoryObjects++;
                    //CheckInventoryObjects();
                    GameObject newShardSlot = Instantiate(itemSlot, itemBox);
                    newShardSlot.transform.GetChild(1).GetComponent<Image>().sprite = itemSprite;
                }


     
                break;

            case "Mirror":
                GameObject newMirrorSlot = Instantiate(itemSlot, itemBox); //Create the slot
                newMirrorSlot.transform.GetChild(1).GetComponent<Image>().sprite = itemSprite;
                hasMirror = true; //Assign the sprite to be displayed    
                break;

        }

       
      //Deactivate the game object item once picked up
        if(replacementSprite != null)
        {
            if (replacementSprite.name != "Tree")
            {
                gameObject.SetActive(false);
            
            }
        }

  

    }
    
    public void UsedItem(string itemName)
    {
        for(int i = 0; i < inventory.transform.childCount; i++)
        {
            if (inventory.transform.GetChild(i).transform.GetChild(1).GetComponent<Image>().sprite.name == itemName)
            {
                Destroy(inventory.transform.GetChild(i).gameObject);
            }
        }
    }


  /*  void checkMirror()
    {
        if (gameObject.GetComponent<SpriteRenderer>().sprite.name == "Mirror" && hasMirror == false)
        {

            gameObject.SetActive(false);

        }

        else if (gameObject.GetComponent<SpriteRenderer>().sprite.name == "Mirror" && hasMirror == true)
        {

            gameObject.SetActive(true);
        }
    }
  */
}
