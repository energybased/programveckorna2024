using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbetareStats : MonoBehaviour
{
    public float workerSpeed;
    public float workerQuality;
    public float workerService;
    public int restingTime;

    float breakTimer = 0;
    int timeForBreak;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < restingTime; i++)
        {
            timeForBreak += 30;
        }
    }

    // Update is called once per frame
    void Update()
    {
        breakTimer += 1 * Time.deltaTime;

        if(breakTimer > timeForBreak)
        {
            print("jag är trött");
            workerSpeed *= 0.5f;
            workerQuality *= 0.5f;
            workerService *= 0.5f;
        }

    }
}
