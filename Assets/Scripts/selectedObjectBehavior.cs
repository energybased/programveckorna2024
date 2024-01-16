using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class selectedObjectBehavior : MonoBehaviour
{
    public furnitureData furnitureData;
    SpriteRenderer sr;
    GameManager gameManager;
    UIManager uiManager;
    float currentOpacity;
    float minimumOpacity = 0.25f;
    float maximumOpacity = 0.75f;
    static float t = 0.0f;
    bool isInTheWay;
    public GameObject originButton;

    int spriteOrientation = 0;
    void Start()
    {
        sr = FindObjectOfType<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();

        for (int i = 0; i < uiManager.uiPlacementTips.Length; i++)
        {
            var uiToolTip = Instantiate(uiManager.uiPlacementTips[i], new Vector3(transform.position.x -1 + i, transform.position.y + 0.75f + (furnitureData.furnitureSize.y * 0.5f)  , 0), Quaternion.identity, transform);    
            uiToolTip.transform.localScale = Vector3.one/new Vector2(furnitureData.furnitureSize.x, furnitureData.furnitureSize.y);
        }
    }
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(Mathf.Round(mousePos.x * 2f)*0.5f, Mathf.Round(mousePos.y * 2f)*0.5f,0);
        
        isInTheWay = Physics2D.OverlapBox(transform.position, furnitureData.furnitureSize/2, 0, LayerMask.GetMask("Furniture", "Player"));
        if(isInTheWay)
        {
            sr.color = new Color(0.8f, 0, 0);
        }
        else
        {
            sr.color = new Color(0, 0.8f, 0);
            if(Input.GetMouseButtonDown(0)) //Place Object
            {
                var newObject = Instantiate(furnitureData.furniturePrefab as GameObject, transform.position, Quaternion.identity);
                newObject.GetComponent<SpriteRenderer>().sprite = furnitureData.furnitureSprites[spriteOrientation];
                newObject.GetComponent<furnitureStats>().furnitureData = furnitureData;
                gameManager.furnitureInventoryList.Remove(gameManager.furnitureInventoryList.Find(obj=>obj.GetComponent<furnitureStats>().furnitureName == furnitureData.furnitureName));
                gameManager.furniturePlacedList.Add(newObject);
                Destroy(originButton);
                Destroy(gameObject);
                gameManager.CalculateStats();
                Cursor.visible = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.E) || Input.mouseScrollDelta.y < 0) //Rotate Clockwise
        {
            if(spriteOrientation >= 3)
            {
                spriteOrientation = 0;
                ChangeOrientation(spriteOrientation);
            }
            else
            {
                spriteOrientation++;
                ChangeOrientation(spriteOrientation);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Q) || Input.mouseScrollDelta.y > 0) //Rotate Counter-Clockwise
        {
            if(spriteOrientation <= 0)
            {
                spriteOrientation = 3;
                ChangeOrientation(spriteOrientation);
            }
            else
            {
                spriteOrientation--;
                ChangeOrientation(spriteOrientation);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape)) //Cancel Placement
        {
            Destroy(gameObject);
            Cursor.visible = true;
        }

        if(sr != null) //Flashing (Opacity ping pong-ing between 0.6 and 0.4)
        {
            currentOpacity = Mathf.Lerp(minimumOpacity, maximumOpacity, t);
            t += 2.5f * Time.deltaTime;
            if (t > 1.0f)
            {
                float temp = maximumOpacity;
                maximumOpacity = minimumOpacity;
                minimumOpacity = temp;
                t = 0.0f;
            }
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, currentOpacity);
        }
    }
    void ChangeOrientation(int orientation)
    {
        sr.sprite = furnitureData.furnitureSprites[orientation];
    }
    
}