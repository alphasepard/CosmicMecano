using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class WinTimer : MonoBehaviour
{
    public TimeSpan timeSpent = TimeSpan.FromSeconds(0);

    Text text;
    Score score;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        var scoreGameObject = GameObject.Find("Persisted");
        if (scoreGameObject) score = scoreGameObject.GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!score) return;
        timeSpent += TimeSpan.FromSeconds(Time.deltaTime);
        text.text = string.Format("{0:mm}’{0:ss}'’{0:ff}", timeSpent);
        score.score = timeSpent;
    }
}
