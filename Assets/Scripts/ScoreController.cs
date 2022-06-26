using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class ScoreController : MonoBehaviour
{
    public static string score;
    public LocalizeStringEvent localizeStringEvent;

    private void Start()
    {
        localizeStringEvent = gameObject.GetComponent<LocalizeStringEvent>();
    }
    private void Update()
    {
        score = Mathf.Max(Mathf.Ceil(Global.player.transform.position.y), float.Parse(score)).ToString();
        localizeStringEvent.RefreshString();
    }

}
