using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalBaseClass : MonoBehaviour
{
    public string goalName;
    public int goalTarget;
    public bool isCompleted;
    public enum goalTypes{Economic, Management, Service}
    public goalTypes currentGoal;
}
