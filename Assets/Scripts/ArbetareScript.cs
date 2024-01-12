using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbetareScript : ArbetareBase
{
    public float breakTimer = 0; //timer som räknar upp till rast
    public int timeUntilBreak; //hur långt brodern arbetar innan trötthet
    public bool tiredHappened = false; //kollar om bror har blivit trött

    CV cv; 

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetKeyDown(KeyCode.F) && collider.gameObject.tag == "boss" && tiredHappened == true)
        {
            print("go on break");
            Invoke("onBreak", 15f);
        }
    }

    public void onBreak() //coroutine medans man är på rast
    {
        breakTimer = 0;
        tiredHappened = false;
        workerSpeed *= 2f;
        workerQuality *= 2f;
        workerService *= 2f;
    }

    private void stats()
    {
        workerSpeed = cv.workerSpeed;
        workerQuality = cv.workerQuality;
        workerService = cv.workerService;
        restingTime = cv.restingTime;

        for (int i = 0; i < restingTime; i++)
        {
            timeUntilBreak += 30;
        }

        breakTimer = 0;
    }

    //Start
    void Start()
    {
        tiredHappened = false;

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
            workerSpeed *= 0.5f;
            workerQuality *= 0.5f;
            workerService *= 0.5f;

            tiredHappened = true;
        }
    }
}
