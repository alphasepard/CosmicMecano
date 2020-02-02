using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var score = GameObject.Find("Persisted").GetComponent<Score>().score;
        GetComponent<Text>().text = string.Format("{0:mm}’{0:ss}'’{0:ff}", score);
    }
}
