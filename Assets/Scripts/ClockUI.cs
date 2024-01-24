using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

// All script is written by Ronnie except the one marked with // E which is written by Erik 

public class ClockUI : MonoBehaviour
{
    public enum Period { Morning, Afternoon, Night} // R - Three identifiable periods
    public Period currentPeriod;
    private const float afternoon_length = 240f; // R - The duration of afternoon is 240 seconds/4 minutes
    private const float morning_length = 240f; // R - Morning -||-
    private const float daytime_length = afternoon_length + morning_length; // R - Day consists of the morning and afternoon (480 seconds/8 minutes)
    private const float nighttime_length = 120f; // R - The duration of night is 120 seconds/2 minutes
    public bool isday = true; // R - The game always starts during the day
    

    private Transform clockHandTransform; 
    private TextMeshProUGUI dayText; 
    private TextMeshProUGUI timeOfDayText; // E
    public float day;
    int numDays = 1; 

    public bool israining = false; // R - It can't rain when you start the game
    private int chanceofrain; // R - Creates a variable for chance of rain
    public ParticleSystem rainParticleSystem;

    private void Awake()
    {
        clockHandTransform = transform.Find("clockHand");
        dayText = transform.Find("dayText").GetComponent<TextMeshProUGUI>();
        timeOfDayText = transform.Find("timeOfDayText").GetComponent<TextMeshProUGUI>(); // E
        dayText.text = "Day " + numDays;
    }
    void test()
    {
        chanceofrain = Random.Range(0, 100); // R - Sets the variable chanceofrain to a random value between 0-100
        if (chanceofrain <= 30) // R - Run this code if the random value is equal or less than 30
        {
            israining = true; // R - Enables the event rain
            //decreased chance
            if (israining == true)
            {
                var em = rainParticleSystem.emission; 
                em.enabled = true; // R - Enables the particle systems emission
                rainParticleSystem.Play();

            }
        }
        else
        {
            israining = false;
        }


    }

    public void Update()
    {
        timeOfDayText.text = currentPeriod.ToString(); // E
        if (isday == true) // R - Identifies if the current period is day
        {
            day += Time.deltaTime / daytime_length;
            if (day < 0.5f && currentPeriod != Period.Morning) // R - If less than half of the day has passed and it's not currently morning it sets the current period to morning and runs the "test" code
            {
                currentPeriod = Period.Morning;
                test();
            } 
            else if (day > 0.5f && currentPeriod != Period.Afternoon) // R - -||- but more than half of the day passed & ofc switching out morning with afternoon
            {
                currentPeriod = Period.Afternoon;
                test();
            }
        }
        else // R - If it's not day it's night
        {
            if (currentPeriod != Period.Night)
            {
                currentPeriod = Period.Night;
                test();
            }
            day += Time.deltaTime / nighttime_length;
            
        }
        

        float dayNormalized = day % 1f;

        float rotationDegreesPerDay = 360f;
        clockHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay);

        if (day >= 1)
        {
            if (isday == true)
            {
                isday = false;
            }
            else
            {
                isday = true;
                numDays++;
            }
            day = 0;
            dayText.text = "Day " + numDays;
        }

    }
    
}
