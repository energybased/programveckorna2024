using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;


public class ClockUI : MonoBehaviour
{
    public enum Period { Morning, Afternoon, Night}
    public Period currentPeriod;
    private const float REAL_SECONDS_PER_INGAME_DAY = 960f;
    private const float afternoon_length = 360f;
    private const float morning_length = 360f;
    private const float daytime_length = afternoon_length + morning_length;
    private const float nighttime_length = 240f;
    public bool isday = true;
    public bool israining = false;
    private float chanceofrain = 0f;

    private Transform clockHandTransform;
    private TextMeshProUGUI dayText;
    private TextMeshProUGUI timeOfDayText;
    public float day;
    int numDays = 1;

    private void Awake()
    {
        clockHandTransform = transform.Find("clockHand");
        dayText = transform.Find("dayText").GetComponent<TextMeshProUGUI>();
        timeOfDayText = transform.Find("timeOfDayText").GetComponent<TextMeshProUGUI>();
        dayText.text = "Day " + numDays;
    }

    public void test()
    {
        print("maybe rain?");
    }


    private void Update()
    {
        timeOfDayText.text = currentPeriod.ToString();
        if (isday == true)
        {
            day += Time.deltaTime / daytime_length;
            if (day < 0.5f && currentPeriod != Period.Morning)
            {
                currentPeriod = Period.Morning;
                test();
            } else if (day > 0.5f && currentPeriod != Period.Afternoon)
            {
                currentPeriod = Period.Afternoon;
                test();
            }
        }
        else
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
