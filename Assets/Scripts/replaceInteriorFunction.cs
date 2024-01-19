using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class replaceInteriorFunction : MonoBehaviour
{
    public interiorData interiorData;
    public void ReplaceInterior()
    {
        if(interiorData.currentInteriorType == interiorData.interiors.Wall)
        {
            var wallRef = GameObject.Find("walls").GetComponent<SpriteRenderer>();
            wallRef.sprite = interiorData.interiorSprite;
        }
        else if(interiorData.currentInteriorType == interiorData.interiors.Floor)
        {
            var floorRef = GameObject.Find("floor").GetComponent<SpriteRenderer>();
            floorRef.sprite = interiorData.interiorSprite;
        }
    }
}
