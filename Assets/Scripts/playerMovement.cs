using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
   
[SerializeField]
Rigidbody2D rb2d;

float horizontal;
float vertical;

public int runSpeed;

void Start ()
{
   rb2d = GetComponent<Rigidbody2D>(); 
}

void Update ()
{
   horizontal = Input.GetAxisRaw("Horizontal");
   vertical = Input.GetAxisRaw("Vertical"); 
}

private void FixedUpdate()
{  
   rb2d.velocity =(new Vector2(horizontal, vertical).normalized) * runSpeed;
}
   
}
