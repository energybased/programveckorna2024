using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goalManager : MonoBehaviour
{
    public int amountOfStars;
    Image goalsStarsSpriteReference;
    [SerializeField]
    Sprite[] starSprites;
    public List<goalBaseClass> currentGoals;
    void Start()
    {
        goalsStarsSpriteReference = FindObjectOfType<uiGoalsOpacity>().transform.GetChild(0).GetComponent<Image>();
        for (int i = 0; i < 3; i++)
            {
                var componentsToAdd = gameObject.AddComponent(typeof(goalsTier1)) as goalBaseClass;
                componentsToAdd.currentGoalType += i;
                currentGoals.Add(componentsToAdd); 
            }
    }
    public bool checkCurrentGoals()
    {
        for(int i = 0; i < currentGoals.Count; i++)
        {
            if(currentGoals[i].isCompleted == false)
            {
                return false;
            }
        }
        return true;
    }

    void Update()
    {
        goalsStarsSpriteReference.sprite = starSprites[amountOfStars];
        if(checkCurrentGoals())
        {
            amountOfStars++;
            if(amountOfStars == 1)
            {
                var componentsToRemove = gameObject.GetComponents<goalsTier1>();
                for(int i = 0; i < componentsToRemove.Length; i++)
                {
                    Destroy(componentsToRemove[i]);
                }
                currentGoals.Clear();
                for (int i = 0; i < 3; i++)
                {
                    var componentsToAdd = gameObject.AddComponent(typeof(goalsTier2)) as goalsTier2;
                    componentsToAdd.currentGoalType += i;
                    currentGoals.Add(componentsToAdd); 
                }
                print("done?");
            }
            else if(amountOfStars == 2)
            {
                var componentsToRemove = gameObject.GetComponents<goalsTier2>();
                for(int i = 0; i < componentsToRemove.Length; i++)
                {
                    Destroy(componentsToRemove[i]);
                }
                currentGoals.Clear();
                for (int i = 0; i < 3; i++)
                {
                    var componentsToAdd = gameObject.AddComponent(typeof(goalsTier3)) as goalsTier3;
                    componentsToAdd.currentGoalType += i;
                    currentGoals.Add(componentsToAdd); 
                }
                print("done?");
            }
        }
    }
}
