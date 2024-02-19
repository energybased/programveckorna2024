using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ArbetareScript : ArbetareBase
{
    //max skrev allt

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
    [SerializeField] bool cookOnce;

    Animator anim;
    Rigidbody2D rb2d;

    float step;
    public float speed = 2;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetKeyDown(KeyCode.F) && collider.gameObject.tag == "Player" && tiredHappened == true) //om arbetare trööt och du där, ge rast
        {
            print("go on break");
            Invoke("onBreak", 15f);
        }
    }

    public void goToTills() //gå till kassan
    {
        anim.SetBool("standDown", true);
        print("fick kund");
        movingTill = true;
    }

    public void cook()
    {
        if(arbManage.availableStations == null) //om inga stationer finns prova igen
        {
            print("no stations, no cook");
            Invoke("cook", 2f);
        }
        else
        {
            print("start the cook");
            arbManage.usedStations.Add(arbManage.availableStations[0]); //ta en station och gör den till avnönd
            arbManage.availableStations.RemoveAt(0);
            movingCook = true; //gå till arbetsplats
        }
    }
    private void giveDrink() //ge dricka till kund
    {
        print("give drinky");
        movingDropOff = true;
    }

    public void onBreak() //coroutine medans man �r p� rast
    {
        breakTimer = 0;
        tiredHappened = false;
    }

    private void stats() //hitta stats
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

        serviceTime = 0;
        for (int i = 0; i < workerService; i++)
        {
            serviceTime -= 0.5f;
        }
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

        if (breakTimer >= 2 && breakTimer >= timeUntilBreak && tiredHappened == false) //kollar om bror är trött
        {
            print("bro is tired");

            tiredHappened = true;
        }

        if (movingTill == true) //går mot kassan
        {
            anim.SetBool("standDown", false);
            anim.SetBool("walks", true);
            if (transform.position != arbManage.kassaPos.transform.position) //om inte vid kassan, gå till den
            {
                transform.position = Vector3.MoveTowards(transform.position, arbManage.kassaPos.transform.position, step);
            }
            else 
            {
                print("gick till kunden");

                anim.SetBool("standDown", true);
                anim.SetBool("walks", false);

                print("service time calced");
                movingTill = false;
            }
            
        }

        if(arbManage.kassaBusy == true)
        {
            if (arbManage.kund.hasOrdered == true && cookOnce == false)
            {
                cookOnce = true;
                print("ordere true");
                cook(); //arbeta
            }
        }
        
        if (movingCook == true) //rör sig till stationen
        {
            anim.SetBool("walks", true);
            GetComponent<SpriteRenderer>().flipX = true;
            anim.SetBool("standDown", false);
            if(transform.position != arbManage.usedStations.Last().transform.position) //om inte vid stationen, gå dit
            {
                transform.position = Vector3.MoveTowards(transform.position, arbManage.usedStations.Last().transform.position, step);
            }
            else //animationer (som inte funkar)
            {
                anim.SetBool("working", true);
                anim.SetBool("walks", false);
                GetComponent<SpriteRenderer>().flipX = false;
                print("moved to cook");
                movingCook = false;
                cookTime = 3f;
                for (int i = 0; i < workerSpeed; i++) //tid beror på arbetarens stats
                {
                    cookTime -= 0.5f;
                }
                if (tiredHappened == true)
                {
                    cookTime *= 2;
                }
                arbManage.kassaBusy = false;
                anim.SetBool("working", false);
                Invoke("giveDrink", cookTime); //ge dricka
            }
        }

        if(movingDropOff == true)
        {
            print("finished everything"); //gå till dropoff
            anim.SetBool("working", false);
            anim.SetBool("walks", true);
            GetComponent<SpriteRenderer>().flipX = true;
            transform.position = Vector3.MoveTowards(transform.position, arbManage.dropOffPos.transform.position,step);
            if(transform.position == arbManage.dropOffPos.transform.position)
            {
                anim.SetBool("standDown", true);
                anim.SetBool("walks", false);
                GetComponent<SpriteRenderer>().flipX = false;
                arbManage.kund.drinkyDone = true; //reset massa saker
                arbManage.availableStations.Add(arbManage.usedStations[0]); //stationen blir oanvänd igen
                arbManage.usedStations.RemoveAt(0);
                movingDropOff = false;
                arbManage.arbetareList.Add(gameObject); //tillbaka in i arbetarelistan
                arbManage.busyWorking.Remove(gameObject);

                for (int i = 0; i < workerQuality; i++)
                {
                    gameManager.money = gameManager.money + 5; //tjäna pengar, baserat på stats
                }
    
                cookOnce = false;
            }   
        }
    }
}
