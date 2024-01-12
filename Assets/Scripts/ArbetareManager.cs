using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbetareManager : ArbetareBase
{
    public List<GameObject> arbetareList = new List<GameObject>();
    public GameObject worker;
    public GameObject CV;

    public void newWorker()
    {
        Vector2 workerSpawn = new Vector2(-5, 0);
        for (int i = 0; i < 3; i++)
        {
            GameObject newestWorker = Instantiate(worker, workerSpawn, Quaternion.identity);
            Instantiate(CV, newestWorker.transform);
            workerSpawn.x += 5;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
