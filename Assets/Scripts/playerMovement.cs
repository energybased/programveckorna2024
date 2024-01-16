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

   void Start ()
   {
      rb2d = FindObjectOfType<Rigidbody2D>(); 
      uiManager = FindObjectOfType<UIManager>(); 
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
   }
   
}
