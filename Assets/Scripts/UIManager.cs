using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField]
    GameObject layoutGroupObject;
    [SerializeField]
    GameObject inventoryButtonPrefab;
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
            
            var buttonObject = Instantiate(inventoryButtonPrefab, transform.position, Quaternion.identity, layoutGroupObject.transform);
            buttonObject.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = furniture.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            print("Insufficient Funds");
        }
    }
    // Update is called once per frame
    void Update()
    {
        debugText1.text = "Money: " + gameManager.money;
    }
}
