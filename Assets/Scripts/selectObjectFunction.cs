using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectObjectFunction : MonoBehaviour
{
    public void SelectObject(GameObject emptyObject)
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var localSprite = gameObject.transform.GetChild(0).GetComponent<Image>();
        
        var selectedObject = Instantiate(emptyObject, mouseWorldPos, Quaternion.identity);
        selectedObject.AddComponent<SpriteRenderer>().sprite = localSprite.sprite;
        selectedObject.AddComponent<selectedObjectBehavior>();
        selectedObject.GetComponent<selectedObjectBehavior>().originButton = gameObject;
    }
}
