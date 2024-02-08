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

    [SerializeField] Canvas guys;
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

    [SerializeField] RectTransform rt;

    Vector3 scaleChange = new Vector3(0.5f, 0.5f, 0.5f);

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
    
    public void newWorker()
    {
        guys.enabled = true;
        Vector2 workerSpawn = new Vector2(unk.transform.position.x, unk.transform.position.y);
        workerSpawn.x -= 6;
        workerSpawn.y += 1;


        for (int i = 0; i < 3; i++)
        {
            GameObject newestWorker = Instantiate(workerSkins[Random.Range(0,workerSkins.Count)], workerSpawn, Quaternion.identity);
            newestWorker.transform.localScale -= scaleChange;
            Instantiate(CV, newestWorker.transform);
            waitingForHire.Add(newestWorker);
            workerSpawn.x += 1.3f;
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
        arbetareList.Last().GetComponent<ArbetareScript>().breakTimer = 0;
        totalGuys += 1;
        var temp = arbetareList.Last();
        temp.transform.position = Vector3.MoveTowards(temp.transform.position, startPos.transform.position, 1000);
        
        foreach (var GameObject in waitingForHire)
        {
            Destroy(GameObject);
        }

        waitingForHire.Clear();
        guys.enabled = false;

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
        guys.enabled = false;

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
    }
}
