using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;
    Rigidbody2D BaiHu;
    public Animator animator;
    SpriteRenderer spr;

    private CapsuleCollider2D c;

    [SerializeField] private RuntimeAnimatorController Master_Fox_Animator;
    [SerializeField] private RuntimeAnimatorController human_fox_controller;

    public GameObject[] shadows;

    /*
    public CapsuleCollider2D backCollider;
    public CapsuleCollider2D frontCollider;
    public CapsuleCollider2D leftCollider;
    public CapsuleCollider2D rightCollider;
    */

    public float speed;
    public float moveForce;
    Vector2 moveDir;

    public int numTails = 1;
    public bool isHuman = false;

    public IInteractable Interactable;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        //activateCollider(frontCollider);

        BaiHu = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        c = GetComponent<CapsuleCollider2D>();

        animator.runtimeAnimatorController = Master_Fox_Animator;

        /*backCollider = GetComponent<CapsuleCollider2D>();
        frontCollider = GetComponent<CapsuleCollider2D>();
        leftCollider = GetComponent<CapsuleCollider2D>();
        rightCollider = GetComponent<CapsuleCollider2D>();*/

    }

    private void FixedUpdate()
    {
        if (DialogueUI.instance.IsOpen || FadingScript.instance.fadeRunning) return;
        //BaiHu.AddForce(moveDir * moveForce * Time.deltaTime);

        if (moveDir != Vector2.zero)
        {
            BaiHu.velocity = moveDir * moveForce;
        }
        else
        {
            BaiHu.velocity = Vector2.zero;
        }
        //BaiHu.AddForce( moveDir * Time.deltaTime * speed);
    }

    void Update()
    {
        if (DialogueUI.instance.IsOpen || FadingScript.instance.fadeRunning) return;


        //Shadow adjustment
        if (!isHuman)
        {
            shadows[0].SetActive(true);
            shadows[1].SetActive(false);
        }
        else
        {
            shadows[0].SetActive(false);
            shadows[1].SetActive(true);
        }
        
        //----------Player Controls----------

        moveDir = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveDir += Vector2.up;
            //BaiHu.position = BaiHu.position + Vector2.up * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir += Vector2.left;
            //BaiHu.position = BaiHu.position + Vector2.left * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveDir += Vector2.down;
            //BaiHu.position = BaiHu.position + Vector2.down * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDir += Vector2.right;
            //BaiHu.position = BaiHu.position + Vector2.right * Time.deltaTime * speed;
        }

        moveDir = moveDir.normalized;

        //----------Accessing the Animation that's needed at the right time----------
        //if they're moving
        if (moveDir.magnitude > 0f)
        {
            animator.SetFloat("Speed", 5f);
        }

        //if they're not moving
        if (moveDir == Vector2.zero)
        {
            animator.SetFloat("Speed", 0f);
        }
        
        // ----------changes the animation to have correct # tails----------



       /* switch (numTails)
        {
            case 2:
                animator.SetInteger("NumTails", 2);
                break;

        }*/


        //----------Flips the sprite and hitbox----------


        //if moving right don't flip the sprite's art
        
        if (moveDir.x > 0f)
        {
            //c.offset = new Vector2(-0.12f, 0);
            //c.direction = CapsuleDirection2D.Horizontal;

            if(animator.runtimeAnimatorController == human_fox_controller)
            {
                //changeCollider(new Vector2(0.18f, 0.42f), new Vector2(-0.006f, -0.05f), CapsuleDirection2D.Vertical);
                changeCollider(new Vector2(0.18f, 0.17f), new Vector2(-0.006f, -0.147f), CapsuleDirection2D.Horizontal);
            }
            else
            {
                changeCollider(new Vector2(0.54f, 0.35f), new Vector2(-0.12f, 0), CapsuleDirection2D.Horizontal);
            }

            //BaiHu.transform.localScale = new Vector2(BaiHu.transform.localScale.x, BaiHu.transform.localScale.y);
            //activateCollider(rightCollider);
            spr.flipX = false;
        }

        //if moving to left, flip the sprite art to face the correct direction
        if (moveDir.x < 0f)
        {

            if (animator.runtimeAnimatorController == human_fox_controller)
            {
                //changeCollider(new Vector2(0.18f, 0.42f), new Vector2(-0.006f, -0.05f), CapsuleDirection2D.Vertical);
                changeCollider(new Vector2(0.18f, 0.17f), new Vector2(-0.006f, -0.147f), CapsuleDirection2D.Horizontal);

            }
            else
            {
                changeCollider(new Vector2(0.54f, 0.35f), new Vector2(0.12f, 0), CapsuleDirection2D.Horizontal);

            }
            //BaiHu.transform.localScale = new Vector2(-BaiHu.transform.localScale.x, BaiHu.transform.localScale.y);
            //activateCollider(leftCollider);
            spr.flipX = true;

            //boxCollider.offset = new Vector2(-boxCollider.offset.x, boxCollider.offset.y);
        }

        if (moveDir.y > 0f)
        {
            //activateCollider(backCollider);
            if (animator.runtimeAnimatorController == human_fox_controller)
            {
                //changeCollider(new Vector2(0.18f, 0.42f), new Vector2(-0.006f, -0.05f), CapsuleDirection2D.Vertical);
                changeCollider(new Vector2(0.18f, 0.17f), new Vector2(-0.006f, -0.147f), CapsuleDirection2D.Horizontal);
            }
            else
            {
                changeCollider(new Vector2(0.18f, 0.42f), new Vector2(-0.006f, -0.05f), CapsuleDirection2D.Vertical);

            }

            animator.SetBool("MovingUp", true);

        }
        else
        {
            animator.SetBool("MovingUp", false);
        }

        if (moveDir.y < 0f)
        {

            //activateCollider(frontCollider);

            if (animator.runtimeAnimatorController == human_fox_controller)
            {
                //changeCollider(new Vector2(0.18f, 0.42f), new Vector2(-0.006f, -0.05f), CapsuleDirection2D.Vertical);
                changeCollider(new Vector2(0.18f, 0.17f), new Vector2(-0.006f, -0.147f), CapsuleDirection2D.Horizontal);

            }
            else
            {
                changeCollider(new Vector2(0.18f, 0.35f), new Vector2(-0.006f, 0.04f), CapsuleDirection2D.Vertical);
            }
            animator.SetBool("MovingDown", true);

        }
        else
        {
            animator.SetBool("MovingDown", false);
        }


        //----------Dialogue----------
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Interactable != null)
            {
                Interactable.Interact(this);
            }
        }
    }

    /*
    void activateCollider(CapsuleCollider2D activeCollider)
    {
        backCollider.enabled = false;
        frontCollider.enabled = false;
        rightCollider.enabled = false;
        leftCollider.enabled = false;

        activeCollider.enabled = true;

    }
    */

    void changeCollider(Vector2 size, Vector2 offset, CapsuleDirection2D direction)
    {
        c.size = size;
        c.offset = offset;
        c.direction = direction;
    }

    public void setNumTails(int numTails)
    {
        this.numTails = numTails;
    }

}
