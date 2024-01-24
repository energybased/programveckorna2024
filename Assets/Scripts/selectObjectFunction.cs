using UnityEngine;

public class selectObjectFunction : MonoBehaviour
{
    [HideInInspector]
    public furnitureData furnitureData;
    public void SelectObject()
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var objectPreview =  new GameObject(); //Instantiate(emptyObject, mouseWorldPos, Quaternion.identity);
        objectPreview.transform.position=mouseWorldPos; // Set pos

        objectPreview.transform.localScale = new Vector2(furnitureData.furnitureSize.x, furnitureData.furnitureSize.y);
        objectPreview.AddComponent<SpriteRenderer>().sprite = furnitureData.furnitureSprites[0];
        objectPreview.AddComponent<selectedObjectBehavior>();
        objectPreview.GetComponent<selectedObjectBehavior>().originButton = gameObject;
        objectPreview.GetComponent<selectedObjectBehavior>().furnitureData = furnitureData;
        Cursor.visible = false;
    }
}
