using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DayTime : MonoBehaviour
{
    //public Gradient ambientColor;
    //public Gradient directionalColor;
    //public Gradient fogColor;
    [SerializeField] private Color fogColorDay = Color.grey, fogColorNight = Color.black;
    [SerializeField] private float intensityAtNoon = 1f, intensityAtSunSet = 0.5f;
    [SerializeField] private float angleAtNoon;
    [SerializeField] private float starsFadeInTime = 10f, starsFadeOutTime = 10f;
    [SerializeField] private float timeLight = 35000f, timeExtinguish = 90000f;
    public float speed;
    public float timeOfDay;
    public Light sun;
    public Transform player;
    public float ttintColor;

    private Vector3 dir;
    private float prevRotation = -1f, intensity, sunSet, sunRise, fade = 0;
    private Color tintColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    private Renderer rend;

    void Start()
    {
        rend = transform.GetComponent<ParticleSystem>().GetComponent<Renderer>();
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
            transform.position = player.position;
            timeOfDay += Time.deltaTime * speed;
            UpdateLighting(timeOfDay % 86400f);
        }
    }

    private void UpdateLighting(float time)
    {
        //RenderSettings.ambientLight = ambientColor.Evaluate(timePercent);
        //RenderSettings.fogColor = fogColor.Evaluate(timePercent);
        //sun.color = directionalColor.Evaluate(timePercent);
        if (time < sunRise) intensity = intensityAtSunSet * time / sunRise;
        else if (time < 43200f) intensity = intensityAtSunSet + (intensityAtNoon - intensityAtSunSet) * (time - sunRise) / (43200f - sunRise);
        else if (time < sunSet) intensity = intensityAtNoon - (intensityAtNoon - intensityAtSunSet) * (time - 43200f) / (sunSet - 43200f);
        else intensity = intensityAtSunSet - (1f - intensityAtSunSet) * (time - sunSet) / (86400f - sunSet);
        sun.transform.Rotate(dir, (time / 86400f - prevRotation) * 360f);
        RenderSettings.fogColor = Color.Lerp(fogColorNight, fogColorDay, intensity * intensity);
        if (sun != null) sun.intensity = intensity;

        if (TimeBetween(time, timeLight, timeExtinguish))
        {
            fade += Time.deltaTime / starsFadeInTime;
            if (fade > 1f) fade = 1f;
        }
        else
        {
            fade -= Time.deltaTime / starsFadeOutTime;
            if (fade < 0f) fade = 0f;
        }
        tintColor.a = fade;
        ttintColor = fade;
        rend.material.SetColor("_TintColor", tintColor);
    }
    private bool TimeBetween(float currentTime, float startTime, float endTime)
    {
        if (startTime < endTime)
        {
            return (currentTime >= startTime && currentTime <= endTime) ? true : false;
        }
        else
        {
            return (currentTime < startTime && currentTime > endTime) ? false : true;
        }

    }
}
