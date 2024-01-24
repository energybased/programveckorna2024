using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalsTier2 : goalBaseClass
{
    void Start()
    {
        goalProgress = 0;
        if(currentGoalType == goalTypes.Service)
        {
            goalName = "Serve 50 customers";
            goalTarget = 50;
        }
        else if(currentGoalType == goalTypes.Economic)
        {
            goalName = "Spend 500$";
            goalTarget = 500;
        }
        else if(currentGoalType == goalTypes.Management)
        {
            goalName = "Hire 6 employees";
            goalTarget = 6;
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
