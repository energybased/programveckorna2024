using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalManager : MonoBehaviour
{
    public int amountOfStars = 0;
    
    public List<goalBaseClass> currentGoals;
    [SerializeField]
    goalBaseClass[] tier1Goals;
    [SerializeField]
    goalBaseClass[] tier2Goals;
    [SerializeField]
    goalBaseClass[] tier3Goals;

    void Start()
    {
        for (int i = 0; i < tier1Goals.Length; i++)
        {
            var componentsToAdd = gameObject.AddComponent<goalBaseClass>();
            currentGoals.Add(componentsToAdd); 
        }
    }
    public bool checkCurrentGoals()
    {
        print("working");
        for(int i = 0; i < currentGoals.Count; i++){
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
            amountOfStars++;
            switch(amountOfStars)
            {
                case 1:
                    var componentsToRemove = gameObject.GetComponents<goalBaseClass>();
                    for(int i = 0; i < componentsToRemove.Length; i++)
                    {
                        Destroy(componentsToRemove[i]);
                    }
                    currentGoals.Clear();
                    for (int i = 0; i < tier2Goals.Length; i++)
                    {
                        var componentsToAdd = gameObject.AddComponent<goalBaseClass>();
                        currentGoals.Add(componentsToAdd); 
                    }
                    print("done?");
                    break;
            }
        }
    }
}
