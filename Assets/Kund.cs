using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Kund : MonoBehaviour
{
    [SerializeField]
    RaycastHit2D hit;
    float distance = 10f;


    [SerializeField]
    float walkspeed;
    bool takeaway;

    bool isInCafé = false;


    [SerializeField]
    GameObject kassa;
    Vector3 kassaPos;


    [SerializeField]
    GameObject kaffeHämt;
    Vector3 kaffeHämtPos;


    [SerializeField]
    GameObject café;
    Vector3 caféPos;


    [SerializeField]
    GameObject gåUt;
    Vector3 gåUtPos;
    bool hasOrdered = false;
    bool harHämtat = false;

    bool förstIKön = true;

    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    
    void Start()
    {
        caféPos = café.transform.position;
        kassaPos = kassa.transform.position;
        kaffeHämtPos = kaffeHämt.transform.position;
        gåUtPos = gåUt.transform.position;
    }
    
    
    void Update()
    {
        float step = walkspeed * Time.deltaTime;
        if(hasOrdered == false && isInCafé == false && förstIKön == true)
        {
            transform.position =  Vector3.MoveTowards(transform.position, caféPos, step);
            StartCoroutine(InCafé());
        }

        if(hasOrdered == false && isInCafé == true)
        {
            transform.position =  Vector3.MoveTowards(transform.position, kassaPos, step);
        }

        if(hasOrdered == true && harHämtat == false)
        {
            transform.position =  Vector3.MoveTowards(transform.position, kaffeHämtPos, step);
        }

        if(harHämtat == true)
        {
            transform.position =  Vector3.MoveTowards(transform.position, gåUtPos, step);
            StartCoroutine(PåvägUt());
        }   
    }

    void OnTriggerEnter2D (Collider2D Collision)
    {
        
        if(Collision.gameObject.tag == "Arbetare" && hasOrdered == false)
        {
            OrderCoffee();
            StartCoroutine(CoffeeOrder());
        }

        if(Collision.gameObject.tag == "Arbetare" && hasOrdered == true)
        {
            PickUpCoffee();
        }

        if(Collision.gameObject.tag == "Kund")
        {
            StandInLine();
            förstIKön = false;
        }
        
    }

    void OrderCoffee()
    {
        Debug.Log("Beställ Kaffe");
        
    }

    void PickUpCoffee()
    {
        float step = walkspeed * Time.deltaTime;
        ChangeSprite();
        harHämtat = true;
    }
    
    void ChangeSprite()
    {
        spriteRenderer.sprite = newSprite;
    }

    void ChooseEmptyTable()
    {
        Debug.Log("Går till ett ledigt bord");
    }

    void StandInLine()
    {
        
    }

    
    IEnumerator PåvägUt()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }
    
    IEnumerator InCafé()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Är i kafét");
        StopAllCoroutines();

        isInCafé = true;
    }
    
    IEnumerator CoffeeOrder()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Har beställt kaffe");

        hasOrdered = true;
    }
}
