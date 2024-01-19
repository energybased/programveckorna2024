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
            animator.SetBool("gåHöger", true);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            animator.SetBool("gåHöger", false);
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (rb2d.velocity.x < 0)
        {
            animator.SetBool("gåVänster", true);
        }
        else
            animator.SetBool("gåVänster", false);

        if (rb2d.velocity.y > 0)
        {
            animator.SetBool("gåUpp", true);
        }
        else
            animator.SetBool("gåUpp", false);

        if (rb2d.velocity.y < 0)
        {
            animator.SetBool("gåNer", true);
        }
        else
            animator.SetBool("gåNer", false);
    }
   
}
