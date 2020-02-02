using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MiniGame : MonoBehaviour
{
    public GameObject consign;
    public GameObject controls, controlsMoteur, controlsBouclier, controlsLait;
    private TextMeshProUGUI consignText;
    // Start is called before the first frame update
    void Start()
    {
        consignText = consign.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void ShowControls()
    {
        controls.SetActive(true);
        consign.SetActive(true);
    }

    public void ShowControlsMoteur()
    {
        EnableControl(controlsMoteur);
    }

    public void ShowControlsBouclier()
    {
        EnableControl(controlsBouclier);
    }

    public void ShowControlsLait()
    {
        EnableControl(controlsLait);
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

    private void EnableControl(GameObject control)
    {
        controlsMoteur.SetActive(false);
        controlsBouclier.SetActive(false);
        controlsLait.SetActive(false);

        control.SetActive(true);
    }
}
