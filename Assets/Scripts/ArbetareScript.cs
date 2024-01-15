using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbetareScript : ArbetareBase
{
    public float breakTimer = 0; //timer som räknar upp till rast
    public int timeUntilBreak; //hur långt brodern arbetar innan trötthet
    public bool tiredHappened = false; //kollar om bror har blivit trött

    ArbetareManager arbManage;
    CV cv;

    GameObject kassa;
    float cookTime;
    float serviceTime;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetKeyDown(KeyCode.F) && collider.gameObject.tag == "boss" && tiredHappened == true)
        {
            print("go on break");
            Invoke("onBreak", 15f);
        }

    }

    public void goToTills()
    {
       if(kassa.GetComponent<kassaSkript>().busy == false)
       {
            kassa.GetComponent<kassaSkript>().busy = true;
            Vector3.MoveTowards(transform.position, kassa.transform.position, 100);
            serviceTime = 2f;
            for (int i = 0; i < workerService; i++)
            {
                serviceTime -= 0.3f;
            }
            if(tiredHappened == true)
            {
                serviceTime *= 2;
            }
            Invoke("cook", serviceTime);
       }
       
    }

    public void cook()
    {
        

        Vector3.MoveTowards(transform.position, kassa.transform.position, 100);
        serviceTime = 2f;
        for (int i = 0; i < workerService; i++)
        {
            serviceTime -= 0.3f;
        }
        if (tiredHappened == true)
        {
            serviceTime *= 2;
        }
        Invoke("cook", serviceTime);
    }

    public void onBreak() //coroutine medans man är på rast
    {
        breakTimer = 0;
        tiredHappened = false;
    }

    private void stats()
    {
        workerSpeed = cv.workerSpeed;
        workerQuality = cv.workerQuality;
        workerService = cv.workerService;
        restingTime = cv.restingTime;

        for (float i = 0; i < restingTime; i++)
        {
            timeUntilBreak += 30;
        }

        breakTimer = 0;
    }

    //Start
    void Start()
    {
        tiredHappened = false;

        kassa = GameObject.Find("KassaPosition");
        arbManage = GetComponent<ArbetareManager>();

        cv = GetComponentInChildren<CV>();

        Invoke("stats", 1f);

    }

    //Update
    void Update()
    {
        breakTimer += 1 * Time.deltaTime; //timer som räknar upp till rast

        if (breakTimer >= 2 && breakTimer >= timeUntilBreak && tiredHappened == false)
        {
            print("bro is tired");

            tiredHappened = true;
        }

        
    }
}
