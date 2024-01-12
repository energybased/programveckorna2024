using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int money;
    public List<GameObject> furnitureList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        for (int i = 0; i < furnitureList.Count; i++)
        {
            print("Name: " + furnitureList[i].name + "\nIndex: " + furnitureList.IndexOf(furnitureList[i]));
        }
        */
    }
}
