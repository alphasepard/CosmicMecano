using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public GameObject consign;
    public GameObject controls, controlsMoteur, controlsBouclier;
    private Text consignText;
    // Start is called before the first frame update
    void Start()
    {
        consignText = consign.GetComponentInChildren<Text>();
    }

    private void ShowControls()
    {
        controls.SetActive(true);
        consign.SetActive(true);
    }

    public void ShowControlsMoteur()
    {
        controlsBouclier.SetActive(false);
        controlsMoteur.SetActive(true);
        ShowControls();
    }

    public void ShowControlsBouclier()
    {
        controlsMoteur.SetActive(false);
        controlsBouclier.SetActive(true);
        ShowControls();
    }

    public void HideControls()
    {
        controls.SetActive(false);
        consign.SetActive(false);
    }

    public void SetConsign(string text)
    {
        if(consignText) consignText.text = text;
    }
}
