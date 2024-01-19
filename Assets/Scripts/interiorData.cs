using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class interiorData : ScriptableObject
{
    [Header("Interior Data")]
    public int interiorCost;
    [HideInInspector]
    public enum interiors{Wall, Floor}
    public interiors currentInteriorType;
    public Sprite uiPreviewSprite;
    public Sprite interiorSprite;
    
}
