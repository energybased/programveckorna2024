using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField]
    GameObject darkenObject;
    [SerializeField]
    GameObject uiClipboard;
    [SerializeField]
    GameObject layoutGroupObject;
    [SerializeField]
    GameObject inventoryButtonPrefab;
    public GameObject[] uiPlacementTips;
    [SerializeField]
    Texture2D cursorStashTexture;
    [SerializeField]
    TMP_Text debugText1;
    [SerializeField]
    TMP_Text debugText2;
    [SerializeField]
    TMP_Text debugText3;
    [SerializeField]
    TMP_Text debugText4;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void PurchaseObject(furnitureData furnitureData)
    {
        var cost = furnitureData.furnitureCost;
        if(gameManager.money >= cost)
        {
            gameManager.furnitureInventoryList.Add(furnitureData.furniturePrefab as GameObject);
            gameManager.money -= cost;
            print(furnitureData.name + " has been purchased successfully!");  
            print("Money spent: " + cost);
            
            var buttonObject = Instantiate(inventoryButtonPrefab, transform.position, Quaternion.identity, layoutGroupObject.transform);
            buttonObject.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = furnitureData.uiPreviewSprite;
            buttonObject.GetComponent<selectObjectFunction>().furnitureData = furnitureData;
        }
        else
        {
            print("Insufficient Funds");
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!uiClipboard.GetComponent<Animator>().GetBool("isClipboardOpen"))
            {
                uiClipboard.GetComponent<Animator>().SetTrigger("toggleClipboard");
                uiClipboard.GetComponent<Animator>().SetBool("isClipboardOpen", true);
            }
            else
            {
                uiClipboard.GetComponent<Animator>().SetTrigger("toggleClipboard");
                uiClipboard.GetComponent<Animator>().SetBool("isClipboardOpen", false);
            }
        }
        if(Input.GetMouseButton(1))
        {
            Cursor.SetCursor(cursorStashTexture, Vector2.zero, CursorMode.ForceSoftware);
            var objectCheck = Physics2D.OverlapBox(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.one * 0.01f, 0, LayerMask.GetMask("Furniture"));
            if(objectCheck)
            {
                var stashObject = objectCheck.gameObject.GetComponent<furnitureStats>();
                gameManager.furnitureInventoryList.Add(stashObject.furnitureData.furniturePrefab as GameObject);
                gameManager.furniturePlacedList.Remove(gameManager.furniturePlacedList.Find(obj => obj.GetComponent<furnitureStats>().furnitureData.furnitureName == stashObject.furnitureData.furnitureName));
                print(stashObject.furnitureName + " has been stashed!");
                
                var buttonObject = Instantiate(inventoryButtonPrefab, transform.position, Quaternion.identity, layoutGroupObject.transform);
                buttonObject.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = stashObject.furnitureData.uiPreviewSprite;
                buttonObject.GetComponent<selectObjectFunction>().furnitureData = stashObject.furnitureData;
                
                Destroy(objectCheck.gameObject);
                gameManager.CalculateStats();
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        }

        debugText1.text = "Money: " + gameManager.money;
        debugText2.text = "Total Comfort: " + gameManager.totalComfort;
        debugText3.text = "Total Design: " + gameManager.totalDesign;
        debugText4.text = "Total Atmosphere: " + gameManager.totalAtmosphere;
    }
}
