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
    public GameObject bowString;
    public GameObject stringRoot;

    Arrow currentArrow;

    float tension;
    bool charging;

    private Vector3 stringStart;
    void Start()
    {
        charging = false;
        tension = 0;
        stringStart = bowString.transform.localPosition;
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
                bowString.transform.parent = currentArrow.transform;
                bowString.transform.localPosition += Vector3.back / 2f;
            }
            //натяжение лука
            tension = Mathf.Min(tension + Time.deltaTime, maxChargeTimeSec);
            currentArrow.transform.localPosition = Vector3.back * Mathf.Lerp(-maxBackwardPosition, maxForwardPosition, tension / maxChargeTimeSec);
        }
        else
        {
            if (charging && currentArrow != null)
            {
                bowString.transform.parent = stringRoot.transform;

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
                bowString.transform.localPosition = stringStart;
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
