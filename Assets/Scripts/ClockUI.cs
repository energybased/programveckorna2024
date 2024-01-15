using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class ClockUI : MonoBehaviour
{

    private const float REAL_SECONDS_PER_INGAME_DAY = 960f;

    private Transform clockHandTransform;
    private TextMeshProUGUI dayText;
    private float day;
    int numDays = 1;

    private void Awake()
    {
        clockHandTransform = transform.Find("clockHand");
        dayText = transform.Find("dayText").GetComponent<TextMeshProUGUI>();
        dayText.text = "Day " + numDays;
    }

    private void Update()
    {
        day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;

        float dayNormalized = day % 1f;

        float rotationDegreesPerDay = 720f;
        clockHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay);

        if (day >= 1)
        {
            numDays++;
            day = 0;
            dayText.text = "Day " + numDays;
        }

    }
}
