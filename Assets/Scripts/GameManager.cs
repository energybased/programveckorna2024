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
        money = 100;
    }

    // Update is called once per frame
    void Update()
    {
        print("Amount of furniture: "+furnitureList.Count);
    }
}
