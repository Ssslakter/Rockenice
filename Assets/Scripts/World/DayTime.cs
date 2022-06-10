using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DayTime : MonoBehaviour
{
    public Gradient ambientColor;
    public Gradient directionalColor;
    public Gradient fogColor;
    [SerializeField] private float angleAtNoon;
    public float speed;
    public float timeOfDay;
    public Light sun;

    private Vector3 dir;
    private float prevRotation = -1f;

    void Start()
    {

        dir = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angleAtNoon), Mathf.Sin(Mathf.Deg2Rad * angleAtNoon), 0f);
        timeOfDay = 0;
    }


    void Update()
    {

        if (prevRotation == -1f)
        {
            sun.transform.eulerAngles = 100 * Vector3.down;
            prevRotation = 0f;
        }
        else prevRotation = timeOfDay / 86400f;
        if (Application.isPlaying)
        {
            timeOfDay += Time.deltaTime * speed;
            UpdateLighting(timeOfDay / 86400f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = fogColor.Evaluate(timePercent);
        sun.color = directionalColor.Evaluate(timePercent);
        sun.transform.Rotate(dir, (timePercent - prevRotation) * 360f);

    }
}
