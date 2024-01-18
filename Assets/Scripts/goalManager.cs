using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalManager : MonoBehaviour
{
    public goalBaseClass[] goals;

    public bool CheckGoals(){
        for(int i = 0; i < goals.Length; i++){
            if(goals[i].isCompleted == false)
            {
                return false;
            }
        }
        return true;
    }

    void Update()
    {
        if(CheckGoals()){
            print("level Complete!");
        }
    }
}
