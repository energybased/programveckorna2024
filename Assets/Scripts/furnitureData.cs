using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class furnitureData : ScriptableObject
{
    [Header("Furniture Data")]
    public Object furniturePrefab;
    public Sprite[] furnitureSprites;
    public Sprite uiPreviewSprite;
    public Vector2 furnitureSize;
    [Header("Furniture Stats")]
    public string furnitureName;
    public int furnitureCost;
    public int furnitureComfort;
    public int furnitureDesign;
    public int furnitureAtmosphere;
}
