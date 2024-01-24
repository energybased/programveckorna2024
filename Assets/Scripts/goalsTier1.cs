using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalsTier1 : goalBaseClass
{
    void Start()
    {
        goalProgress = 0;
        if(currentGoalType == goalTypes.Service)
        {
            goalName = "Serve 25 customers";
            goalTarget = 25;
        }
        else if(currentGoalType == goalTypes.Economic)
        {
            goalName = "Spend 250$";
            goalTarget = 250;
        }
        else if(currentGoalType == goalTypes.Management)
        {
            goalName = "Hire 3 employees";
            goalTarget = 3;
        }
    }
    void Update()
    {
        if(goalProgress >= goalTarget)
        {
            isCompleted = true;
        }
        else
        {
            isCompleted = false;
        }
    }
}