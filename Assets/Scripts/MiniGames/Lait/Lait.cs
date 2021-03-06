﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lait : Game
{

    public enum State
    {
        ChooseColor,
        ChangeLevel,
    }

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

    public AudioClip clipBouton, clipLevier;

    // logic
    public bool vider;
    public bool bleu;

    public float remplirGaugeYMax = 6.98f;
    public float remplirGaugeYMin = 5.51f;

    public float viderGaugeYMax = -0.84f;
    public float viderGaugeYMin = -3.07f;

    public State state = State.ChooseColor;
    private KeyCode buttonKey, levelKey;
    GameObject gauge, led, bouton;
    float maxY, minY;
    AudioSource audioSource;

    public float speed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        vider = Random.value > 0.5f ? true : false;
        bleu = Random.value > 0.5f ? true : false;

        gauge = bleu ? gaugeBleu : gaugeVert;
        bouton = bleu ? boutonBleu : boutonVert;
        led = bleu ? ledBleu : ledVert;
        maxY = vider ? viderGaugeYMax : remplirGaugeYMax + 1;
        minY = vider ? viderGaugeYMin - 1 : remplirGaugeYMin;
        var levier = vider ? boutonHaut : boutonBas;

        buttonKey = bleu ? KeyCode.B : KeyCode.G;
        levelKey = vider ? KeyCode.DownArrow : KeyCode.UpArrow;

        levier.SetActive(true);
        if (vider) SetY(gauge, remplirGaugeYMax);

        SetConsignes($"Choose the <color=#d917d8><b>{(bleu ? "Blue" : "Green")}</b></color> tank and <color=#d917d8><b>{(vider ? "Empty" : "Fill")}</b></color> it until the last scale. Hit <color=#d917d8><b>Enter</b></color> when done.");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(buttonKey)) bouton.SetActive(false);

        switch (state)
        {
            case State.ChooseColor:
                if (Input.GetKeyDown(buttonKey))
                {
                    state = State.ChangeLevel;
                    bouton.SetActive(true);
                    led.SetActive(true);
                    PlaySound(clipBouton);
                    return;
                }
                break;
            case State.ChangeLevel:
                if (Input.GetKeyDown(levelKey) || Input.GetKeyUp(levelKey)) SwitchLevier();

                if (Input.GetKey(levelKey))
                {
                    ChangeGaugeY(Time.deltaTime);
                    return;
                }

                var y = gauge.transform.localPosition.y;

                Debug.Log($"{minY}, {maxY}, {y}");
                if (y < maxY && y > minY && Input.GetKeyDown(KeyCode.Return))
                {
                    if (ship) ship.repairMilk();
                    Destroy(gameObject);
                    Debug.Log("ok");
                    return;
                }
                break;

        }
    }


    void SetY(GameObject g, float y)
    {
        g.transform.localPosition = new Vector2(g.transform.localPosition.x, y);
    }

    void ChangeGaugeY(float deltaY)
    {
        var step = speed * deltaY * (vider ? -1 : 1);
        var y = gauge.transform.localPosition.y + step;
        SetY(gauge, Mathf.Clamp(y, viderGaugeYMin, remplirGaugeYMax));
    }

    void SwitchLevier()
    {
        PlaySound(clipBouton);
        if (!ledValve.activeSelf) PlaySound(clipLevier);

        boutonBas.SetActive(!boutonBas.activeSelf);
        boutonHaut.SetActive(!boutonHaut.activeSelf);
        ledValve.SetActive(!ledValve.activeSelf);
    }

    void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
