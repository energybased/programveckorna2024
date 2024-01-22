using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class KundAI : MonoBehaviour
{
    public Transform Till;
    public Transform pickUp;
    public Transform emptyTable;

    public float speed = 200f;
    public float nextWayPointDistance = 3f;
    
    public int currentWaypoint;

    public bool hasOrdered = false;
    bool hasPickedUp = false;

    Path path;
    
    
    bool ReachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb2d;
    Animator anim;
    SpriteRenderer sr;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if(hasOrdered == false && hasPickedUp == false)
        {
            seeker.StartPath(rb2d.position, Till.position, OnPathComplete);
            Debug.Log("Going to Till");
        }   
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    

    void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            ReachedEndOfPath = true;
            return;
        }
        else
        {
            ReachedEndOfPath = false;
        }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb2d.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;    

            rb2d.AddForce(force);

            float distance = Vector2.Distance(rb2d.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }
    }
    void OnTriggerEnter2D (Collider2D Collision)
    {
        
        if(Collision.gameObject.tag == "Arbetare" && hasOrdered == false && hasPickedUp == false)
        {
            StartCoroutine(CoffeeOrder());  
        }

        if(Collision.gameObject.tag == "Arbetare" && hasOrdered == true && hasPickedUp == false)
        {
            CoffeePickUp();
        }
    }   
    IEnumerator CoffeeOrder()
        {
            Debug.Log("Ordering Coffee");
            path = null;
            yield return new WaitForSeconds(5);
            hasOrdered = true;
            seeker.StartPath(rb2d.position, pickUp.position, OnPathComplete);
            Debug.Log("Going to PickUp");
        }

    IEnumerator DeSpawn()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }

    void CoffeePickUp()
        {
            hasPickedUp =  true;
            path = null;
            seeker.StartPath(rb2d.position, emptyTable.position, OnPathComplete);
            Debug.Log("Going to Exit");
            Debug.Log("Has picked up order");
            StartCoroutine(DeSpawn());
        }

    private void Update()
    {
        if (rb2d.velocity.x > 0)
        {
            anim.SetBool("goSide", true);
            sr.flipX = true;
        }
        else
        {
            anim.SetBool("goSide", false);
            sr.flipX = false;
        }

        if (rb2d.velocity.x < 0)
        {
            anim.SetBool("goSide", true);
        }
        else
        {
            anim.SetBool("goSide", false);
        }

        if (rb2d.velocity.y > 0)
        {
            anim.SetBool("goUp", true);
        }
        else
        {
            anim.SetBool("goUp", false);
        }

        if (rb2d.velocity.y < 0)
        {
            anim.SetBool("goDown", true);
        }
        else
        {
            anim.SetBool("goDown", false);
        }
    }
}


