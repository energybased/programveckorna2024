using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KundSpawner : MonoBehaviour
{
    
    public GameObject kundPrefab;
    [SerializeField]
    int NumberOfKunder = 0;

    void Start()
    {
        
    }

    
    void Update()
    {
       
        if(NumberOfKunder == 0)
        {
            SpawnKund();
        }
        
    }

    void SpawnKund()
    {
        Instantiate(kundPrefab, new Vector3(-0.5f, -8, 1), Quaternion.identity);
        NumberOfKunder += 1;
    }
}
