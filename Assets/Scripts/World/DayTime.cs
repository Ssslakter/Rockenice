using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DayTime : MonoBehaviour
{
    public Gradient ambientColor;
    public Gradient directionalColor;
    public Gradient fogColor;

    [Range(0, 24)]
    public float timeOfDay;
    public Light directionLight;


    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
        {
            timeOfDay += Time.deltaTime / 4;
            timeOfDay %= 24;
            UpdateLighting(timeOfDay / 24f);
        }
        else
        {
            UpdateLighting((timeOfDay - 12f) / 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = fogColor.Evaluate(timePercent);
        directionLight.color = directionalColor.Evaluate(timePercent);
        directionLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f), 50f, 0));

    }
}
