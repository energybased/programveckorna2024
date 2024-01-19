using System.Collections;
using System.Collections.Generic;
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
        print("WE IN");
        isHovering = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        print("WE OUT");
        isHovering = false;
    }
    
    void Update()
    {
        
        for (int i = 0; i < transform.childCount; i++)
        {
            var tempChild = transform.GetChild(i).GetComponent<Image>();
            tempChild.color = new Color(tempChild.color.r, tempChild.color.g,  tempChild.color.b, opacity);
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
