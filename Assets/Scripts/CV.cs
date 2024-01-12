using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CV : ArbetareBase
{   
    
    int rngSpeed;
    int rngQuality;
    int rngService;
    int rngBreak;

    // Start is called before the first frame update
    void Start()
    {
        rngSpeed = Random.Range(1, 11);
        rngQuality = Random.Range(1, 11);
        rngService = Random.Range(1, 11);
        rngBreak = Random.Range(1, 11);
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate speed
        switch(rngSpeed)
        {
            case <= 3:
                workerSpeed = 1;
                break;

            case >= 4 and <= 6:
                workerSpeed = 2;
                break;

            case >= 7 and <= 9:
                workerSpeed = 3;
                break;
            
            case >= 10:
                workerSpeed = 4;
                break;
        }


        //Calculate quality
        switch (rngQuality)
        {
            case <= 3:
                workerQuality = 1;
                break;

            case >= 4 and <= 6:
                workerQuality = 2;
                break;

            case >= 7 and <= 9:
                workerQuality = 3;
                break;

            case >= 10:
                workerQuality = 4;
                break;
        }


        //Calculate Service
        switch (rngService)
        {
            case <= 3:
                workerService = 1;
                break;

            case >= 4 and <= 6:
                workerService = 2;
                break;

            case >= 7 and <= 9:
                workerService = 3;
                break;

            case >= 10:
                workerService = 4;
                break;
        }

        //Calculate break time
        switch(rngBreak)
        {
            case <= 3:
                restingTime = 1;
                break;

            case >= 4 and <= 6:
                restingTime = 2;
                break;

            case >= 7 and <= 9:
                restingTime = 3;
                break;
            
            case >= 10:      
                restingTime = 4;    
                break;
        }
    }
}
