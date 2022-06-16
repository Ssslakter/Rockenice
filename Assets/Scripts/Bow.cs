using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public float maxArrowSpeed = 10;
    public float minChargeTimeSec = 0.5f;
    public float maxChargeTimeSec = 3;
    public float maxForwardPosition = 0.2f;
    public float maxBackwardPosition = 0.5f;

    public GameObject arrow;

    Arrow currentArrow;

    float tension;
    bool charging;
    void Start()
    {
        charging = false;
        tension = 0;
    }


    void Update()
    {
        if ((int)Input.GetAxisRaw("Fire1") == 1)
        {
            if (!charging)
            {
                //взятие стрелы
                charging = true;
                currentArrow = StartCharging();
            }
            //натяжение лука
            tension = Mathf.Min(tension + Time.deltaTime, maxChargeTimeSec);
            currentArrow.transform.localPosition = Vector3.back * Mathf.Lerp(-maxBackwardPosition, maxForwardPosition, tension / maxChargeTimeSec);
        }
        else
        {
            if (charging && currentArrow != null)
            {
                if (tension < minChargeTimeSec)
                {
                    //Убираем стрелу и не стреляем
                    CancelShot();
                }
                else
                {
                    //стреляем
                    Shoot(tension, currentArrow);
                }
                charging = false;
                tension = 0;
            }
        }

    }
    Arrow StartCharging()
    {
        Arrow newArrow = Instantiate(arrow, transform).GetComponent<Arrow>();
        return newArrow;
    }
    void Shoot(float tension, Arrow currentArrow)
    {
        currentArrow.transform.parent = null;
        currentArrow.SetSpeed(transform.forward * maxArrowSpeed * Mathf.Lerp(minChargeTimeSec, maxChargeTimeSec, tension));
    }

    void CancelShot()
    {
        Destroy(currentArrow.gameObject);
    }
}
