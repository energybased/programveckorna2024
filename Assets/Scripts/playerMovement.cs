using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
   
   Rigidbody2D rb2d;
   UIManager uiManager;

   float horizontal;
   float vertical;

   public int runSpeed;

    Animator animator;
    SpriteRenderer sr;

   void Start ()
   {
      rb2d = gameObject.GetComponent<Rigidbody2D>(); 
      uiManager = FindObjectOfType<UIManager>();
      animator = GetComponent<Animator>();
 
   }

   void Update ()
   {
      horizontal = Input.GetAxisRaw("Horizontal");
      vertical = Input.GetAxisRaw("Vertical"); 
   }

   private void FixedUpdate()
   {  
        if(uiManager.playerCanMove)
        {
             rb2d.velocity = new Vector2(horizontal, vertical).normalized * runSpeed;
        }

        if(rb2d.velocity.x > 0)
        {
            animator.SetBool("g�H�ger", true);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            animator.SetBool("g�H�ger", false);
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (rb2d.velocity.x < 0)
        {
            animator.SetBool("g�V�nster", true);
        }
        else
            animator.SetBool("g�V�nster", false);

        if (rb2d.velocity.y > 0)
        {
            animator.SetBool("g�Upp", true);
        }
        else
            animator.SetBool("g�Upp", false);

        if (rb2d.velocity.y < 0)
        {
            animator.SetBool("g�Ner", true);
        }
        else
            animator.SetBool("g�Ner", false);
    }
   
}
