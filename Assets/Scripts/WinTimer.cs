using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class WinTimer : MonoBehaviour
{
    public TimeSpan timeSpent = TimeSpan.FromSeconds(0);

    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSpent += TimeSpan.FromSeconds(Time.deltaTime);
        text.text = string.Format("{0:mm}’{0:ss}'’{0:ff}", timeSpent);
    }
}
