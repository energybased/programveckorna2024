using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;
    Animator animator;
    Camera cam;
    bool spotlightPlayer;
    bool isConstructing;
    GameObject player;
    public bool playerCanMove;
    [SerializeField]
    GameObject layoutGroupObject;
    [SerializeField]
    GameObject inventoryButtonPrefab;
    public GameObject[] uiPlacementTips;
    [SerializeField]
    Texture2D cursorStashTexture;
    [SerializeField]
    TMP_Text totalMoney;
    [SerializeField]
    TMP_Text totalComfort;
    [SerializeField]
    TMP_Text totalDesign;
    [SerializeField]
    TMP_Text totalAtmosphere;
    [SerializeField]
    GameObject furnitureAvailable;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = FindObjectOfType<Animator>();
        player = GameObject.FindWithTag("Player");
        cam = FindObjectOfType<Camera>();
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
        if(spotlightPlayer && !isConstructing)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, player.gameObject.transform.position - new Vector3(3.125f, -0.5f, 0), Time.deltaTime * 8);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 3.5f, Time.deltaTime * 8);
            playerCanMove = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else if(!spotlightPlayer && isConstructing)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, Vector3.zero - new Vector3(0, 1.5f, 0), Time.deltaTime  * 8);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 9, Time.deltaTime * 8);
            playerCanMove = true;
        }
        else if(!spotlightPlayer && !isConstructing)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, Vector3.zero, Time.deltaTime * 4);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 8, Time.deltaTime * 4);
            playerCanMove = true;
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(animator.GetBool("isClipboardOpen") == false && animator.GetBool("isConstructionOpen") == false)
            {
                spotlightPlayer = true;
                animator.SetBool("isClipboardOpen", true);
                animator.SetTrigger("toggleClipboard");
                
            }
            else if(animator.GetBool("isClipboardOpen") == true && animator.GetBool("isConstructionOpen") == false)
            {
                spotlightPlayer = false;
                animator.SetBool("isClipboardOpen", false);
                animator.SetTrigger("toggleClipboard");
            }
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            if(animator.GetBool("isConstructionOpen") == false && animator.GetBool("isClipboardOpen") == false)
            {
                isConstructing = true;
                animator.SetBool("isConstructionOpen", true);
                animator.SetTrigger("toggleConstruction");
                
            }
            else if(animator.GetBool("isConstructionOpen") == true && animator.GetBool("isClipboardOpen") == false)
            {
                isConstructing = false;
                animator.SetBool("isConstructionOpen", false);
                animator.SetTrigger("toggleConstruction");
            }
        }

        if(Input.GetMouseButton(1) && isConstructing)
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

        if(gameManager.furnitureInventoryList.Count > 0)
        {
            furnitureAvailable.SetActive(false);
        }
        else
        {
            furnitureAvailable.SetActive(true);
        }
        totalMoney.text = "Money: " + gameManager.money;
        totalComfort.text = "Total Comfort: " + gameManager.totalComfort;
        totalDesign.text = "Total Design: " + gameManager.totalDesign;
        totalAtmosphere.text = "Total Atmosphere: " + gameManager.totalAtmosphere;
    }
}
