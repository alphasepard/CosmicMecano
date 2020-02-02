using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lait : MonoBehaviour
{
    // rendering
    public GameObject boutonBas;
    public GameObject boutonHaut;
    public GameObject boutonVert;
    public GameObject boutonBleu;
    public GameObject ledValve;
    public GameObject ledBleu;
    public GameObject ledVert;
    public GameObject gaugeVert;
    public GameObject gaugeBleu;

    // logic
    public bool vider;
    public bool bleu;
    public float gaugeBasY = -5.40f;
    public float gaugeHautY = 1f;

    // Start is called before the first frame update
    void Start()
    {
        vider = Random.value > 0.5f ? true : false;
        bleu = Random.value > 0.5f ? true : false;

        if (vider)
        {
            boutonBas.SetActive(true);
            if (bleu)
            {
                // gauge vert remplie
                // gauge bleu vide
            } else
            {
                // gauge bleu remplie
                // gauge vert vide
            }
        }
        else
        {
            boutonHaut.SetActive(true);
            boutonBas.SetActive(true);
            if (bleu)
            {
                // gauge vert vide
                // gauge bleu remplie
            }
            else
            {
                // gauge bleu vide
                // gauge vert remplie
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
