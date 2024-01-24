using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalsTier3 : goalBaseClass
{
    void Start()
    {
        goalProgress = 0;
        if(currentGoalType == goalTypes.Service)
        {
            goalName = "Serve 100 customers";
            goalTarget = 100;
        }
        else if(currentGoalType == goalTypes.Economic)
        {
            goalName = "Spend 1000$";
            goalTarget = 1000;
        }
        else if(currentGoalType == goalTypes.Management)
        {
            goalName = "Hire 9 employees";
            goalTarget = 9;
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
