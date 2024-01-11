using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField]
    TMP_Text debugText1;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void PurchaseObject(GameObject furniture)
    {
        var cost = furniture.GetComponent<furnitureStats>().furnitureCost;
        if(gameManager.money >= cost)
        {
            gameManager.furnitureList.Add(furniture);
            gameManager.money -= cost;
            print(furniture.name + " has been purchased successfully!");  
            print("Money spent: " + cost);
        }
        else
        {
            print("Insufficient Funds");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(gameManager != null)
        {
            debugText1.text = "money: " + gameManager.money;
        }
    }
}
