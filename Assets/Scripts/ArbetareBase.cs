using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbetareBase : MonoBehaviour
{
    //variabler
    //stats
    public float workerSpeed;
    public float workerQuality;
    public float workerService;
    public int restingTime;
    public enum race { };
    
    //rastbaserade variabler
    public float breakTimer = 0; //timer som räknar upp till rast
    int timeUntilBreak; //hur långt brodern arbetar innan trötthet
    bool tiredHappened = false; //kollar om bror har blivit trött
    public Canvas restButton;


    //funktioner och coroutines
    public void breakButton() //knappen när man går på rast
    {
        restButton.enabled = false;
        print("on break");
        StartCoroutine(onBreak());
    }

    public IEnumerator onBreak() //coroutine medans man är på rast
    {
        yield return new WaitForSeconds(15);
        breakTimer = 0;
        tiredHappened = false;
        workerSpeed *= 2f;
        workerQuality *= 2f;
        workerService *= 2f;
        print("back");
    }

    //Start
    void Start()
    {
        restButton.enabled = false;

        for (int i = 0; i < restingTime; i++)
        {
            timeUntilBreak += 30;
        }
    }

    //Update
    void Update()
    {
        breakTimer += 1 * Time.deltaTime; //timer som räknar upp till rast

        if (breakTimer > timeUntilBreak && !tiredHappened)
        {
            tiredHappened = true;

            restButton.enabled = true;

            workerSpeed *= 0.5f;
            workerQuality *= 0.5f;
            workerService *= 0.5f;
        }  
    }
}
