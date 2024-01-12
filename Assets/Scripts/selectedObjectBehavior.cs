using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectedObjectBehavior : MonoBehaviour
{
    SpriteRenderer sr;
    GameManager gameManager;
    float currentOpacity;
    float minimumOpacity = 0.25f;
    float maximumOpacity = 0.75f;
    static float t = 0.0f;
    void Start()
    {
        sr = FindObjectOfType<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(Mathf.Round(mousePos.x * 2f)*0.5f, Mathf.Round(mousePos.y * 2f)*0.5f,0);

        if(Input.GetMouseButtonDown(0))
        {
            var current = gameManager.furnitureList.Find(obj=>obj.name==sr.sprite.name);
            //and isnt in the way of something
            LayerMask mask = LayerMask.GetMask("Furniture");
            if(Physics.CheckBox(new Vector3(transform.position.x,transform.position.y, 0), new Vector3(current.transform.localScale.x/2,current.transform.localScale.y/2, current.transform.localScale.z/2), Quaternion.identity, mask))
            {
                print("IS IN THE WAY");
            }
            else
            {
                print("why");
                var newObject = Instantiate(current, new Vector3(transform.position.x,transform.position.y, 0), Quaternion.identity);
                gameManager.furnitureList.Remove(current);
                Destroy(gameObject);
            }
        }

        if(sr != null)
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
}