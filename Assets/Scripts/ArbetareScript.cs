using System.Linq;
using UnityEngine;

public class ArbetareScript : ArbetareBase
{
    public float breakTimer = 0; //timer som r�knar upp till rast
    public int timeUntilBreak; //hur l�ngt brodern arbetar innan tr�tthet
    public bool tiredHappened = false; //kollar om bror har blivit tr�tt

    ArbetareManager arbManage;
    CV cv;

    float cookTime;
    float serviceTime;

    bool movingTill = false;
    bool movingCook = false;
    bool movingDropOff = false;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetKeyDown(KeyCode.F) && collider.gameObject.tag == "Player" && tiredHappened == true)
        {
            print("go on break");
            Invoke("onBreak", 15f);
        }
    }

    public void goToTills()
    {
        if (arbManage.kassaBusy == false)
        {
            print("fick kund");
            arbManage.kassaBusy = true; 
            movingTill = true;
            if (movingTill == false)
            {
                for (int i = 0; i < workerService; i++)
                {
                    serviceTime = 2f;
                    serviceTime -= 0.3f;
                }
                if (tiredHappened == true)
                {
                    serviceTime *= 2;
                }
                Invoke("cook", serviceTime);
            }
        }
        else
        {
            Invoke("goToTills", 2f);
        }
    }

    public void cook()
    {
        if(arbManage.availableStations == null)
        {
            Invoke("cook", 2f);
        }
        else
        {
            int temp = Random.Range(1, arbManage.availableStations.Count);
            arbManage.usedStations.Add(arbManage.availableStations[temp]);
            arbManage.availableStations.RemoveAt(temp);
            movingTill = true;
            if (movingTill == false)
            {
                arbManage.kassaBusy = false;
                cookTime = 2f;
                for (int i = 0; i < workerSpeed; i++)
                {
                    cookTime -= 0.3f;
                }
                if (tiredHappened == true)
                {
                    cookTime *= 2;
                }
                Invoke("giveDrink", cookTime);
            }
        }
    }

    private void giveDrink()
    {
        movingDropOff = true;
        if(movingDropOff == false)
        {
            arbManage.arbetareList.Add(gameObject);
            arbManage.busyWorking.Remove(gameObject);
        }
    }

    public void onBreak() //coroutine medans man �r p� rast
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
        
        arbManage = FindObjectOfType<ArbetareManager>();
        cv = GetComponentInChildren<CV>();

        Invoke("stats", 1f);
    }

    //Update
    void Update()
    {
        breakTimer += 1 * Time.deltaTime; //timer som r�knar upp till rast

        if (breakTimer >= 2 && breakTimer >= timeUntilBreak && tiredHappened == false)
        {
            print("bro is tired");

            tiredHappened = true;
        }

        if (movingTill == true)
        {
            print("g�r till kunden");
            transform.position = Vector3.MoveTowards(transform.position, arbManage.kassaPos.transform.position, 1000);
            movingTill = false;
        }

        if(movingCook == true)
        {
            Vector3.MoveTowards(transform.position, arbManage.usedStations.Last().transform.position, 100);
            movingCook = false;
        }

        if(movingDropOff == true)
        {
            Vector3.MoveTowards(transform.position, arbManage.dropOffPos.transform.position, 100);
            movingDropOff = false;
        }
    }
}
