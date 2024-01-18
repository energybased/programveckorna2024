using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rain : MonoBehaviour
{
    public bool israining = false;
    private int chanceofrain;
    public ParticleSystem rainParticleSystem;

    public void test()
    {
        print("maybe rain?");
        chanceofrain = Random.Range(0, 100);
        if (chanceofrain <= 1)
        {
            israining = true;
            //chanceofrain <= 20;
            if (israining == true)
            {
                var em = rainParticleSystem.emission;
                em.enabled = true;
                rainParticleSystem.Play();

            }
        }
        else
        {
            israining = false;
        }


    }
}
