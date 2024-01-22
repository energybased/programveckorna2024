using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ArbetareScript : ArbetareBase
{
    public float breakTimer = 0; //timer som r�knar upp till rast
    public int timeUntilBreak; //hur l�ngt brodern arbetar innan tr�tthet
    public bool tiredHappened = false; //kollar om bror har blivit tr�tt

    GameManager gameManager;
    ArbetareManager arbManage;
    CV cv;

    float cookTime;
    public float serviceTime;

    bool movingTill = false;
    bool movingCook = false;
    bool movingDropOff = false;

    Animator anim;
    Rigidbody2D rb2d;

    float step;
    public float speed = 2;

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
            anim.SetBool("ståNer", true);
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
        gameManager = FindObjectOfType<GameManager>();
        arbManage = FindObjectOfType<ArbetareManager>();
        cv = GetComponentInChildren<CV>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        Invoke("stats", 1f);
    }

    //Update
    void Update()
    {
        breakTimer += 1 * Time.deltaTime; //timer som r�knar upp till rast
        step = Time.deltaTime * speed;

        if (breakTimer >= 2 && breakTimer >= timeUntilBreak && tiredHappened == false)
        {
            print("bro is tired");

            tiredHappened = true;
        }

        if (movingTill == true)
        {
            anim.SetBool("ståNer", false);
            anim.SetBool("går", true);
            transform.position = Vector3.MoveTowards(transform.position, arbManage.kassaPos.transform.position, step);
            print("gick till kunden");
            if(transform.position == arbManage.kassaPos.transform.position)
            {
                anim.SetBool("ståNer", true);
                anim.SetBool("går", false);
               
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
        }

        if(movingCook == true)
        {
            anim.SetBool("går", true);
            GetComponent<SpriteRenderer>().flipX = true;
            anim.SetBool("ståNer", false);
            transform.position = Vector3.MoveTowards(transform.position, arbManage.usedStations.Last().transform.position, step);
            if (transform.position == arbManage.usedStations.Last().transform.position)
            {
                anim.SetBool("arbeting", true);
                anim.SetBool("går", false);
                GetComponent<SpriteRenderer>().flipX = false;
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
                anim.SetBool("arbeting", false);
                Invoke("giveDrink", cookTime);
            }
        }

        if(movingDropOff == true)
        {
            print("finished everything");
            anim.SetBool("arbeting", false);
            anim.SetBool("går", true);
            GetComponent<SpriteRenderer>().flipX = true;
            transform.position = Vector3.MoveTowards(transform.position, arbManage.dropOffPos.transform.position,step);
            if(transform.position == arbManage.dropOffPos.transform.position)
            {
                anim.SetBool("ståNer", true);
                anim.SetBool("går", false);
                GetComponent<SpriteRenderer>().flipX = false;
                arbManage.availableStations.Add(arbManage.usedStations[0]);
                arbManage.usedStations.RemoveAt(0);
                movingDropOff = false;
                arbManage.arbetareList.Add(gameObject);
                arbManage.busyWorking.Remove(gameObject);
                gameManager.money = gameManager.money + 20;
            }   
        }
    }
}
