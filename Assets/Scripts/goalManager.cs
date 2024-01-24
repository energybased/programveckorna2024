using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class goalManager : MonoBehaviour
{
    public List<goalBaseClass> currentGoals;
    public int amountOfStars;
    Image goalsStarsSpriteReference;
    [SerializeField]
    Sprite[] starSprites;
    [Header("Goal Text References")]
    [SerializeField]
    TMP_Text[] goalProgressionText;
    [SerializeField]
    TMP_Text[] goalText;
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
    void refreshGoalHUD()
    {
        goalText[0].SetText(currentGoals[0].goalName);
        goalProgressionText[0].SetText("   " + currentGoals[0].goalProgress + "/" + currentGoals[0].goalTarget);
        goalText[1].SetText(currentGoals[1].goalName);
        goalProgressionText[1].SetText("   " + currentGoals[1].goalProgress + "/" + currentGoals[1].goalTarget);
        goalText[2].SetText(currentGoals[2].goalName);
        goalProgressionText[2].SetText("   " + currentGoals[2].goalProgress + "/" + currentGoals[2].goalTarget);
    }
    void Update()
    {
        goalsStarsSpriteReference.sprite = starSprites[amountOfStars];
        refreshGoalHUD();
        if(checkCurrentGoals())
        {
            if(amountOfStars < 3)
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
                }
            }
            else if(amountOfStars >= 3)
            {
                print("gg");
                //completed!
                //queue some cool particles or some shit
            }
        }
    }
}
