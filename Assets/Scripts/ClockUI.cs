using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class ClockUI : MonoBehaviour
{

    private const float REAL_SECONDS_PER_INGAME_DAY = 960f;
    private const float afternoon_length = 360f;
    private const float morning_length = 360f;
    private const float daytime_length = afternoon_length + morning_length;
    private const float nighttime_length = 240f;
    public bool isday = true;


    private Transform clockHandTransform;
    private TextMeshProUGUI dayText;
    public float day;
    int numDays = 1;

    private void Awake()
    {
        clockHandTransform = transform.Find("clockHand");
        dayText = transform.Find("dayText").GetComponent<TextMeshProUGUI>();
        dayText.text = "Day " + numDays;
    }

    private void Update()
    {
        if (isday == true)
        {
            day += Time.deltaTime / daytime_length;
        }
        else
        {
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
