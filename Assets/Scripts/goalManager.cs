using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalManager : MonoBehaviour
{
    
    public goalBaseClass[] currentGoals;
    public goalBaseClass[] tier1Goals;
    public goalBaseClass[] tier2Goals;
    public goalBaseClass[] tier3Goals;


    public bool checkCurrentGoals(){
        for(int i = 0; i < currentGoals.Length; i++){
            if(currentGoals[i].isCompleted == false)
            {
                return false;
            }
        }
        return true;
    }

    void Update()
    {
        if(checkCurrentGoals())
        {
            
        }
    }
}
