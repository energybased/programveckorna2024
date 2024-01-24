using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class uiGoalsOpacity : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    float opacity = 1;
    Image imageRef;
    bool isHovering;
    void Start()
    {
        imageRef = GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
    
    void Update()
    {
        var childrenArrayImage = transform.GetComponentsInChildren<Image>();
        var childrenArrayText = transform.GetComponentsInChildren<TMP_Text>();
        foreach (var childImage in childrenArrayImage)
        {
            childImage.color = new Color(childImage.color.r, childImage.color.g,  childImage.color.b, opacity);
        }
        foreach (var childText in childrenArrayText)
        {
            childText.color = new Color(childText.color.r, childText.color.g,  childText.color.b, opacity);
        }
        imageRef.color = new Color(imageRef.color.r, imageRef.color.g ,imageRef.color.b, opacity);
        
        if(isHovering)
        {
            opacity = Mathf.Lerp(opacity, 0.15f, Time.deltaTime * 3);
        }
        else if(!isHovering)
        {
            opacity = Mathf.Lerp(opacity, 1, Time.deltaTime * 6);
        }
    }
}
