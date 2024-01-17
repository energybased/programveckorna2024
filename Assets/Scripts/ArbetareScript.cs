using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ArbetareScript : ArbetareBase
{
    public float breakTimer = 0; //timer som räknar upp till rast
    public int timeUntilBreak; //hur långt brodern arbetar innan trötthet
    public bool tiredHappened = false; //kollar om bror har blivit trött

    ArbetareManager arbManage;
    CV cv;

    float cookTime;
    public float serviceTime;

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

    public void repeatUntilAwesome()
    {
        if(arbManage.kassaBusy == false)
        {
            print("going 2 da tills");
            goToTills();
        }
        else
        {
            print("trying again");
            repeatUntilAwesome();
        }
    }
    


    public void goToTills()
    {
        if (arbManage.kassaBusy == false)
        {
            arbManage.kund.ordered = true;
            print("fick kund");
            arbManage.kassaBusy = true; 
            movingTill = true;
        }
    }

    public void cook()
    {
        if(arbManage.availableStations == null)
        {
            print("no stations, no cook");
            Invoke("cook", 2f);
        }
        else
        {
            print("start the cook");
            arbManage.usedStations.Add(arbManage.availableStations[0]);
            arbManage.availableStations.RemoveAt(0);
            movingCook = true;
        }
    }

    private void giveDrink()
    {
        print("give drinky");
        movingDropOff = true;
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
        
        arbManage = FindObjectOfType<ArbetareManager>();
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

        if (movingTill == true)
        {
            print("går till kunden");
            transform.position = Vector3.MoveTowards(transform.position, arbManage.kassaPos.transform.position, 1000);
            print("gick till kunden");
            movingTill = false;

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

        if(movingCook == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, arbManage.usedStations.Last().transform.position, 100);
            print("moved to cook");
            movingCook = false;
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

        if(movingDropOff == true)
        {
            print("finished everything");
            transform.position = Vector3.MoveTowards(transform.position, arbManage.dropOffPos.transform.position, 100);
            arbManage.availableStations.Add(arbManage.usedStations[0]);
            arbManage.usedStations.RemoveAt(0);
            movingDropOff = false;
            arbManage.arbetareList.Add(gameObject);
            arbManage.busyWorking.Remove(gameObject);
            arbManage.kund.ordered = false;
        }
    }
}
