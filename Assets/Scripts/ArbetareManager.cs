using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ArbetareManager : ArbetareBase
{
    public List<GameObject> arbetareList = new List<GameObject>();
    List<GameObject> waitingForHire = new List<GameObject>();
    List<GameObject> tiredWorker = new List<GameObject>();
    public List<GameObject> busyWorking = new List<GameObject>();

    public List<GameObject> availableStations = new List<GameObject>();
    public List<GameObject> usedStations = new List<GameObject>();

    [SerializeField] List<GameObject> workerSkins = new List<GameObject>();

    [SerializeField] List<Button> guys = new List<Button>();
    [SerializeField] GameObject worker;
    [SerializeField] GameObject CV;
    [SerializeField] GameObject unk;

    public GameObject startPos;
    public GameObject kassaPos;
    public GameObject dropOffPos;

    float totalSpeed;
    float totalQuality;
    float totalService;
    int totalTired;
    int totalGuys;

    public bool kassaBusy = false;

    public KundAI kund;

    [SerializeField] TextMeshProUGUI statText1;
    [SerializeField] TextMeshProUGUI statText2;
    [SerializeField] TextMeshProUGUI statText3;

    Vector3 scaleChange = new Vector3(0.5f, 0.5f, 0.5f);
    Vector2 workerSpawn = new Vector2(0, 0);

    [SerializeField] TextMeshProUGUI noMoreGuys;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "kund" && kassaBusy == false)
        {
            kund = collision.GetComponent<KundAI>();
            kassaBusy = true;
            print("kund har kommit");
                
            busyWorking.Add(arbetareList[0]);   
            kund.coffeeTimer = 5;
            ArbetareScript lastWorker = busyWorking.Last().GetComponent<ArbetareScript>();
            if(lastWorker.tiredHappened)
            {
                lastWorker.serviceTime *= 0.5f;
            }
            kund.coffeeTimer -= lastWorker.serviceTime;
            lastWorker.goToTills();
            arbetareList.RemoveAt(0);         
        }
    }
    
    public void checkForWorkers()
    {
        if(totalGuys < 3 && waitingForHire.Count == 0)
        {
            newWorker();
        }
        else
        {
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

    public void closeApplicantsTab()
    {
        if(waitingForHire != null)
        {
            for (int i = 0; i < waitingForHire.Count; i++)
            {
                waitingForHire[i].SetActive(false);
            }
        }
    }

    public void newWorker()
    {
        for (int i = 0; i < guys.Count; i++)
        {
            guys[i].enabled = true;
        }
       
        workerSpawn = new Vector2(unk.transform.position.x, unk.transform.position.y);
        workerSpawn.x -= 5.9f;
        workerSpawn.y += 0.7f;


        for (int i = 0; i < 3; i++)
        {
            GameObject newestWorker = Instantiate(workerSkins[Random.Range(0,workerSkins.Count)], workerSpawn, Quaternion.identity);
            newestWorker.transform.localScale -= scaleChange;
            Instantiate(CV, newestWorker.transform);
            waitingForHire.Add(newestWorker);
            workerSpawn.x += 1.2f;
        }
        Invoke("cvShowStats",0.2f);
    }

    void cvShowStats()
    {
        CV CVtemp = waitingForHire[0].GetComponentInChildren<CV>();
        statText1.text = "Quality: " + CVtemp.workerQuality + " Speed: " + CVtemp.workerSpeed + " Service: " + CVtemp.workerService + " Energy: " + CVtemp.restingTime;
        CVtemp = waitingForHire[1].GetComponentInChildren<CV>();
        statText2.text = "Quality: " + CVtemp.workerQuality + " Speed: " + CVtemp.workerSpeed + " Service: " + CVtemp.workerService + " Energy: " + CVtemp.restingTime;
        CVtemp = waitingForHire[2].GetComponentInChildren<CV>();
        statText3.text = "Quality: " + CVtemp.workerQuality + " Speed: " + CVtemp.workerSpeed + " Service: " + CVtemp.workerService + " Energy: " + CVtemp.restingTime;
    }

    //choose a guy (or woman)
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
    public void restoreGuyLists()
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

    public void countStats()
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
        for (int i = 0; i < arbetareList.Count; i++)
        {
           if(arbetareList[i].GetComponent<ArbetareScript>().tiredHappened)
           {
                tiredWorker.Add(arbetareList[i]);
           }
        }

        totalTired = tiredWorker.Count;

        for (int i = 0; i < tiredWorker.Count; i++)
        {
            if (tiredWorker[i].GetComponent<ArbetareScript>().tiredHappened == false)
            {
                tiredWorker.RemoveAt(i);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab) && waitingForHire != null)
        {
            closeApplicantsTab();
        }   

        if(totalGuys == 3)
        {
            noMoreGuys.enabled = true;

            for (int i = 0; i < guys.Count; i++)
            {
                guys[i].gameObject.SetActive(false);
            }
        }
    }
}
