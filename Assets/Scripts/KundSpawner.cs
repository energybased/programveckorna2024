using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KundSpawner : MonoBehaviour
{
    
    public GameObject kundPrefab;
    [SerializeField]
<<<<<<< Updated upstream
=======

    float counter = 0;
    int counterSpeed;

    [SerializeField]
>>>>>>> Stashed changes
    int NumberOfKunder = 0;

    void Start()
    {
<<<<<<< Updated upstream
        
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
=======
        counterSpeed = 1;
    }
   // Update is called once per frame
    void FixedUpdate()
    {
        counter += counterSpeed * Time.deltaTime;
        float randomNumber1= Random.Range(0,2);
        float randomNumber= Random.Range(0,2);

        if(counter >= 30 && NumberOfKunder <= 10 && randomNumber == 1 )
        {
            
            if(randomNumber1 == 0)
            {
                AstarPath.active.Scan();
                counter = 0;
                Instantiate(kundPrefab, transform.position,transform.rotation);
                NumberOfKunder += 1;

            }
            else
            {
                counter = 20;
            }
            //else if(randomNumber1 == 1)
           // {
             //   counter = 0;
             //   Instantiate(kundPrefab, transform.position,transform.rotation);
             //   Instantiate(kundPrefab, transform.position,transform.rotation);
              //  NumberOfKunder += 2;
          //  }
            
        }
    }

    
>>>>>>> Stashed changes
}
