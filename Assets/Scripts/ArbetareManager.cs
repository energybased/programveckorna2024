using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbetareManager : ArbetareBase
{
    public List<GameObject> arbetareList = new List<GameObject>();
    public List<GameObject> v‰ntarPÂAnst‰llning = new List<GameObject>();
    public Canvas guys;
    public GameObject worker;
    public GameObject CV;

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

        foreach (var GameObject in v‰ntarPÂAnst‰llning)
        {
            Destroy(GameObject);
        }

        guys.enabled = false;
    }

    public void guy2()
    {
        arbetareList.Add(v‰ntarPÂAnst‰llning[1]);
        v‰ntarPÂAnst‰llning.RemoveAt(1);

        foreach (var GameObject in v‰ntarPÂAnst‰llning)
        {
            Destroy(GameObject);
        }

        guys.enabled = false;
    }

    public void guy3()
    {
        arbetareList.Add(v‰ntarPÂAnst‰llning[2]);
        v‰ntarPÂAnst‰llning.RemoveAt(2);

        foreach (var GameObject in v‰ntarPÂAnst‰llning)
        {
            Destroy(GameObject);
        }

        guys.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        guys.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
