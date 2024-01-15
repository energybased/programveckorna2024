using UnityEngine;

public class selectObjectFunction : MonoBehaviour
{
    [HideInInspector]
    public furnitureData furnitureData;
    public void SelectObject(GameObject emptyObject)
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var objectPreview = Instantiate(emptyObject, mouseWorldPos, Quaternion.identity);
        objectPreview.transform.localScale = Vector3.one * furnitureData.furnitureSize;
        objectPreview.AddComponent<SpriteRenderer>().sprite = furnitureData.furnitureSprites[0];
        objectPreview.AddComponent<selectedObjectBehavior>();
        objectPreview.GetComponent<selectedObjectBehavior>().originButton = gameObject;
        objectPreview.GetComponent<selectedObjectBehavior>().furnitureData = furnitureData;
        Cursor.visible = false;
    }
}
