using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ArbetareManager : ArbetareBase
{
    //max skrev allt

    public List<GameObject> arbetareList = new List<GameObject>(); //lista av arbetare
    List<GameObject> waitingForHire = new List<GameObject>(); //lista av folk som kan bli anställda
    List<GameObject> tiredWorker = new List<GameObject>(); //trötta arbetare
    public List<GameObject> busyWorking = new List<GameObject>(); //arbetare som jobbar

    public List<GameObject> availableStations = new List<GameObject>(); //arbetsstationer
    public List<GameObject> usedStations = new List<GameObject>();

    [SerializeField] List<GameObject> workerSkins = new List<GameObject>(); //arbetare olika variationer

    [SerializeField] List<Button> guys = new List<Button>();
    [SerializeField] GameObject worker;
    [SerializeField] GameObject CV;
    [SerializeField] GameObject unk;

    public GameObject startPos; //pathfinding
    public GameObject kassaPos;
    public GameObject dropOffPos;

    float totalSpeed; //stats
    float totalQuality;
    float totalService;
    int totalTired;
    int totalGuys;

    public bool kassaBusy = false;

    public KundAI kund; //hitta kund

    [SerializeField] TextMeshProUGUI statText1; //texter
    [SerializeField] TextMeshProUGUI statText2;
    [SerializeField] TextMeshProUGUI statText3;

    Vector3 scaleChange = new Vector3(0.5f, 0.5f, 0.5f); //worker positions and scale
    Vector2 workerSpawn = new Vector2(0, 0);

    [SerializeField] TextMeshProUGUI noMoreGuys; //text

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "kund" && kassaBusy == false) //om en kund kommer
        {
            kund = collision.GetComponent<KundAI>();
            kassaBusy = true;
            print("kund har kommit"); //kunden har kommint
                
            busyWorking.Add(arbetareList[0]);  //hitta arbetare, börja kund kod
            kund.coffeeTimer = 5;
            ArbetareScript lastWorker = busyWorking.Last().GetComponent<ArbetareScript>();

            if(lastWorker.tiredHappened) //om arbetare är tröt
            {
                lastWorker.serviceTime *= 0.5f;
            }
            kund.coffeeTimer -= lastWorker.serviceTime; 
            lastWorker.goToTills(); //ta arbetaren och få den att jobba
            arbetareList.RemoveAt(0);         
        }
    }
    
    public void checkForWorkers() //kollar ifall arbetare att anställa finns
    {
        if(totalGuys < 3 && waitingForHire.Count == 0) 
        {
            newWorker(); //skapar nya arbetare
        }
        else
        {
            //gör arbetarna aktiva att anställa

            Vector2 workerSpawn = new Vector2(unk.transform.position.x, unk.transform.position.y);
            workerSpawn.x -= 5.9f;
            workerSpawn.y += 0.7f;

            for (int i = 0; i < waitingForHire.Count; i++)
            {
                waitingForHire[i].SetActive(true);
                waitingForHire[i].transform.position = workerSpawn;
                workerSpawn.x += 1.2f;
            }
        }
    }

    public void closeApplicantsTab() //close tab
    {
        if(waitingForHire != null)
        {
            for (int i = 0; i < waitingForHire.Count; i++)
            {
                waitingForHire[i].SetActive(false);
            }
        }
    }

    public void newWorker() //skapar nya arbetare
    {
        for (int i = 0; i < guys.Count; i++)
        {
            guys[i].enabled = true;
        }
       
        workerSpawn = new Vector2(unk.transform.position.x, unk.transform.position.y);
        workerSpawn.x -= 5.9f;
        workerSpawn.y += 0.7f;


        for (int i = 0; i < 3; i++) //väljer vilken arbetare och skapar stats
        {
            GameObject newestWorker = Instantiate(workerSkins[Random.Range(0,workerSkins.Count)], workerSpawn, Quaternion.identity);
            newestWorker.transform.localScale -= scaleChange;
            Instantiate(CV, newestWorker.transform);
            waitingForHire.Add(newestWorker);
            workerSpawn.x += 1.2f;
        }
        Invoke("cvShowStats",0.2f);
    }

    void cvShowStats() //visar stats på texterna
    {
        CV CVtemp = waitingForHire[0].GetComponentInChildren<CV>();
        statText1.text = "Quality: " + CVtemp.workerQuality + " Speed: " + CVtemp.workerSpeed + " Service: " + CVtemp.workerService + " Energy: " + CVtemp.restingTime;
        CVtemp = waitingForHire[1].GetComponentInChildren<CV>();
        statText2.text = "Quality: " + CVtemp.workerQuality + " Speed: " + CVtemp.workerSpeed + " Service: " + CVtemp.workerService + " Energy: " + CVtemp.restingTime;
        CVtemp = waitingForHire[2].GetComponentInChildren<CV>();
        statText3.text = "Quality: " + CVtemp.workerQuality + " Speed: " + CVtemp.workerSpeed + " Service: " + CVtemp.workerService + " Energy: " + CVtemp.restingTime;
    }

    //välj en arbetare
    public void guy1()
    {
        arbetareList.Add(waitingForHire[0]);
        waitingForHire.RemoveAt(0);
        restoreGuyLists();
    }

    public void guy2()
    {
        arbetareList.Add(waitingForHire[1]);
        waitingForHire.RemoveAt(1);
        restoreGuyLists();
    }

    public void guy3()
    {
        arbetareList.Add(waitingForHire[2]);
        waitingForHire.RemoveAt(2);
        restoreGuyLists();
    }
    public void restoreGuyLists() //cleanup kod som alla arbetare behöver innan de anställs
    {
        var temp = arbetareList.Last();
        temp.GetComponent<ArbetareScript>().breakTimer = 0;
        temp.transform.localScale += scaleChange;
        totalGuys += 1;
        
        temp.transform.position = Vector3.MoveTowards(temp.transform.position, startPos.transform.position, 1000);
        
        foreach (var GameObject in waitingForHire)
        {
            Destroy(GameObject);
        }

        waitingForHire.Clear();

        Invoke("countStats", 0.5f);
    }

    public void countStats() //räkna ihop stats
    {
        totalQuality = 0;
        totalService = 0;
        totalSpeed = 0;

        for (int i = 0; i < arbetareList.Count; i++)
        { 
            totalSpeed += arbetareList[i].GetComponent<ArbetareScript>().workerSpeed;
            totalQuality += arbetareList[i].GetComponent<ArbetareScript>().workerQuality;
            totalService += arbetareList[i].GetComponent<ArbetareScript>().workerService;
        }
        for (int i = 0; i < busyWorking.Count; i++)
        {
            totalSpeed += busyWorking[i].GetComponent<ArbetareScript>().workerSpeed;
            totalQuality += busyWorking[i].GetComponent<ArbetareScript>().workerQuality;
            totalService += busyWorking[i].GetComponent<ArbetareScript>().workerService;
        }

        totalSpeed = totalSpeed / totalGuys;
        totalQuality = totalQuality / totalGuys;
        totalService = totalService / totalGuys;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < guys.Count; i++)
        {
            guys[i].enabled = false;
        }
        noMoreGuys.enabled = false;

        arbetareList.Clear();
        tiredWorker.Clear();
        busyWorking.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < arbetareList.Count; i++) //kollar om trött
        {
           if(arbetareList[i].GetComponent<ArbetareScript>().tiredHappened)
           {
                tiredWorker.Add(arbetareList[i]);
           }
        }

        totalTired = tiredWorker.Count;

        for (int i = 0; i < tiredWorker.Count; i++) //kollar om inte trött
        {
            if (tiredWorker[i].GetComponent<ArbetareScript>().tiredHappened == false)
            {
                tiredWorker.RemoveAt(i);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab) && waitingForHire != null)
        {
            closeApplicantsTab(); //close tab
        }   

        if(totalGuys == 3) //kan inte anställa fler än 3
        {
            noMoreGuys.enabled = true;

            for (int i = 0; i < guys.Count; i++)
            {
                guys[i].gameObject.SetActive(false);
            }
        }
    }
}
