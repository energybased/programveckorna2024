using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor.Tilemaps;
using UnityEngine;

public class ArbetareManager : ArbetareBase
{
    public List<GameObject> arbetareList = new List<GameObject>();
    public List<GameObject> v‰ntarPÂAnst‰llning = new List<GameObject>();
    public List<GameObject> tiredWorker = new List<GameObject>();
    public Canvas guys;
    public GameObject worker;
    public GameObject CV;

    public float totalSpeed;
    public float totalQuality;
    public float totalService;

    public int totalTired;

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
        foreach (var GameObject in v‰ntarPÂAnst‰llning)
        {
            Destroy(GameObject);
        }

        v‰ntarPÂAnst‰llning.Clear();
        guys.enabled = false;

        Invoke("countStats", 0.1f);
    }

    public void countStats()
    {
        totalQuality = 0;
        totalService = 0;
        totalSpeed = 0;

        for (int i = 0; i < arbetareList.Count; i++)
        {
            if(i < arbetareList.Count)
            {
                totalSpeed += arbetareList[i].GetComponent<ArbetareScript>().workerSpeed;
                totalQuality += arbetareList[i].GetComponent<ArbetareScript>().workerQuality;
                totalService += arbetareList[i].GetComponent<ArbetareScript>().workerService;
            }
 
            if(i < tiredWorker.Count)
            {
                totalSpeed += tiredWorker[i].GetComponent<ArbetareScript>().workerSpeed;
                totalQuality += tiredWorker[i].GetComponent<ArbetareScript>().workerQuality;
                totalService += tiredWorker[i].GetComponent<ArbetareScript>().workerService;
            }
        }

        totalSpeed = totalSpeed / arbetareList.Count + tiredWorker.Count;
        totalQuality = totalQuality / arbetareList.Count + tiredWorker.Count;
        totalService = totalService / arbetareList.Count + tiredWorker.Count;
    }

    // Start is called before the first frame update
    void Start()
    {
        guys.enabled = false;

        arbetareList.Clear();
        tiredWorker.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < arbetareList.Count; i++)
        {
           if(arbetareList[i].GetComponent<ArbetareScript>().tiredHappened)
           {
                totalTired += 1;
                tiredWorker.Add(arbetareList[i]);
                arbetareList.RemoveAt(i);
                countStats();
           }
        }

        for (int i = 0; i < tiredWorker.Count; i++)
        {
            if (tiredWorker[i].GetComponent<ArbetareScript>().tiredHappened == false)
            {
                totalTired += 1;
                arbetareList.Add(tiredWorker[i]);
                tiredWorker.RemoveAt(i);
                countStats();
            }
        }
    }
}
