using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public GameObject consign;
    private Text consignText;
    // Start is called before the first frame update
    void Start()
    {
        consign.GetComponent<Text>();
    }

    public void SetConsign(string text)
    {
        consignText.text = text;
    }
}
