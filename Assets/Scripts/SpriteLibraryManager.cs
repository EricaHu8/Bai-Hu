using System.Collections;
using System.Collections.Generic;
using UnityEngine.U2D.Animation;
using UnityEngine;

public class SpriteLibraryManager : MonoBehaviour
{
    /*public static SpriteLibraryAsset instance;*/
    public SpriteLibrary spriteLibrary;
    public SpriteLibraryAsset[] updatedTails;
    [SerializeField] private RuntimeAnimatorController Master_Fox_Animator;
    [SerializeField] private RuntimeAnimatorController human_fox_controller;

    private Animator animator;

    //public int numTails = 1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = Master_Fox_Animator;
        /*  if (instance == null)
          {
              instance = this;
          }*/
    }

    // Update is called once per frame
    void Update()
    {
        switch (PlayerControl.instance.numTails)
        {
            case 2:
                spriteLibrary.spriteLibraryAsset = updatedTails[1];
                break;
            case 4:
                spriteLibrary.spriteLibraryAsset = updatedTails[2];
                break;
            case 6:
                spriteLibrary.spriteLibraryAsset = updatedTails[3];
                break;
            case 9:

                spriteLibrary.spriteLibraryAsset = updatedTails[4];
                break;

        }

        //if they step on the tile to turn human it switches animators
        if (PlayerControl.instance.isHuman == true)
        {
            animator.runtimeAnimatorController = human_fox_controller;
        }
        else
        {
            animator.runtimeAnimatorController = Master_Fox_Animator;
        }


    }

    /*  public void setNumTails(int numTails)
      {
          this.numTails = numTails;
      }*/
}
