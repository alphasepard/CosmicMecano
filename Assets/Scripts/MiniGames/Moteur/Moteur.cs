using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moteur : Game
{
    public bool left = true;
    public int nbDirt;
    public AudioClip[] clips;

    GameObject brosse;
    AudioSource audioSource;

    static readonly System.Random rand = new System.Random();

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        nbDirt = transform.childCount;
        brosse = GetComponentInChildren<Brosse>().gameObject;
        SetConsignes("Clean up engine aeration.");
    }

    private void DestroyOneDirt()
    {
        var dirts = GetComponentsInChildren<Dirt>();
        if (dirts.Length != 0) Destroy(dirts[0].gameObject);
    }

    private void DeleteOneDirt()
    {
        if (transform.childCount > 0)
        {
            DestroyOneDirt();
            nbDirt--;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(left ? KeyCode.RightArrow : KeyCode.LeftArrow))
        {
            left = !left;
            DeleteOneDirt();
            Play();
            var position = brosse.transform.localPosition;
            brosse.transform.localPosition = new Vector3(-1 * position.x, position.y, position.z);
            if (nbDirt == 0) End();
        }
    }

    void End()
    {
        if(nbDirt == 0) ship.repairEngine();
        else ship.errorDamageShip();
        Destroy(gameObject);
    }

    void Play()
    {
        var i = rand.Next(0, clips.Length);
        audioSource.clip = clips[i];
        audioSource.Play();
    }
}
