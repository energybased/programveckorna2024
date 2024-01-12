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
        rngSpeed = Random.Range(1, 10);
        rngQuality = Random.Range(1, 10);
        rngService = Random.Range(1, 10);
        rngBreak = Random.Range(1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate speed
        if(rngSpeed <= 3)
            workerSpeed = 1;
        
        else if(rngSpeed <= 6)
            workerSpeed = 2;
        
        else if(rngSpeed <= 9)
            workerSpeed = 3;
        
        else
            workerSpeed = 4;
        

        //Calculate quality
        if (rngQuality <= 3)
            workerQuality = 1;
        
        else if (rngQuality <= 6)        
            workerQuality = 2;
        
        else if (rngQuality <= 9)
            workerQuality = 3;
        
        else
            workerQuality = 4;
        

        //Calculate Service
        if (rngService <= 3)
            workerService = 1;
        
        else if (rngService <= 6)
            workerService = 2;
        
        else if (rngService <= 9)
            workerService = 3;

        else
            workerService = 4;

        //Calculate break time
        if (rngBreak <= 3)
            restingTime = 1;

        else if (rngBreak <= 6)
            restingTime = 2;

        else if (rngBreak <= 9)
            restingTime = 3;

        else
            restingTime = 4;
    }
}
