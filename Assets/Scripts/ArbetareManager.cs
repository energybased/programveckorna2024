using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Tilemaps;
using UnityEngine;

public class ArbetareManager : ArbetareBase
{
    public List<GameObject> arbetareList = new List<GameObject>();
    public List<GameObject> v‰ntarPÂAnst‰llning = new List<GameObject>();
    public List<GameObject> tiredWorker = new List<GameObject>();
    public List<GameObject> busyWorking = new List<GameObject>();

    public List<GameObject> availableStations = new List<GameObject>();
    public List<GameObject> usedStations = new List<GameObject>();

    public Canvas guys;
    public GameObject worker;
    public GameObject CV;

    public GameObject startPos;
    public GameObject kassaPos;
    public GameObject dropOffPos;

    public float totalSpeed;
    public float totalQuality;
    public float totalService;
    public int totalTired;
    public int totalGuys;

    int chooseWorker;

    public bool kassaBusy = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "kund" && kassaBusy == false)
        {
            print("kund har kommit");
            chooseWorker = UnityEngine.Random.Range(0,arbetareList.Count);
            busyWorking.Add(arbetareList[chooseWorker]);
            busyWorking.Last().GetComponent<ArbetareScript>().goToTills();
            arbetareList.RemoveAt(chooseWorker);
        }
    }

    public void newWorker()
    {
        guys.enabled = true;
        Vector2 workerSpawn = new Vector2(-5, 0);
        for (int i = 0; i < 3; i++)
        {
            GameObject newestWorker = Instantiate(worker, workerSpawn, Quaternion.identity);
            Instantiate(CV, newestWorker.transform);
            v‰ntarPÂAnst‰llning.Add(newestWorker);
            workerSpawn.x += 5;
        }
    }

    //choose a guy (or woman)
    public void guy1()
    {
        arbetareList.Add(v‰ntarPÂAnst‰llning[0]);
        v‰ntarPÂAnst‰llning.RemoveAt(0);
        restoreGuyLists();
    }

    public void guy2()
    {
        arbetareList.Add(v‰ntarPÂAnst‰llning[1]);
        v‰ntarPÂAnst‰llning.RemoveAt(1);
        restoreGuyLists();
    }

    public void guy3()
    {
        arbetareList.Add(v‰ntarPÂAnst‰llning[2]);
        v‰ntarPÂAnst‰llning.RemoveAt(2);
        restoreGuyLists();
    }
    public void restoreGuyLists()
    {
        totalGuys += 1;
        var temp = arbetareList.Last();
        temp.transform.position = Vector3.MoveTowards(temp.transform.position, startPos.transform.position, 1000);
        
        foreach (var GameObject in v‰ntarPÂAnst‰llning)
        {
            Destroy(GameObject);
        }

        v‰ntarPÂAnst‰llning.Clear();
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
