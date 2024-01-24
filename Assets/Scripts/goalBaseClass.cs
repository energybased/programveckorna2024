using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalBaseClass : MonoBehaviour
{
    public string goalName;
    public int goalTarget;
    public int goalProgress;
    public bool isCompleted;
    public enum goalTypes{Economic, Management, Service}
    public goalTypes currentGoalType;
}
