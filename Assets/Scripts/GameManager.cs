using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Café Stats")]
    public int money;
    public int totalComfort;
    public int totalDesign;
    public int totalAtmosphere;
    [Header("Furniture Objects Lists")]
    public List<GameObject> furnitureInventoryList = new List<GameObject>();
    public List<GameObject> furniturePlacedList = new List<GameObject>();

    public void CalculateStats()
    {
        int comfort = 0, design = 0, atmosphere = 0;
        for (int i = 0; i < furniturePlacedList.Count; i++)
        {
            comfort += furniturePlacedList[i].GetComponent<furnitureStats>().furnitureData.furnitureComfort;
            design += furniturePlacedList[i].GetComponent<furnitureStats>().furnitureData.furnitureDesign;
            atmosphere += furniturePlacedList[i].GetComponent<furnitureStats>().furnitureData.furnitureAtmosphere;
        }
        totalComfort = comfort; 
        totalDesign = design;
        totalAtmosphere = atmosphere;
    }
}