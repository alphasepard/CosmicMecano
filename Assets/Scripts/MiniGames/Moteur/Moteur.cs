using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moteur : Game
{
    public bool left = true;
    public int nbDirt;
    GameObject brosse;

    private void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.Return)) End();
        else if (nbDirt == 0) return;
        else if (Input.GetKeyDown(left ? KeyCode.RightArrow : KeyCode.LeftArrow))
        {
            left = !left;
            DeleteOneDirt();
            var position = brosse.transform.localPosition;
            brosse.transform.localPosition = new Vector3(-1 * position.x, position.y, position.z);
            if (nbDirt == 0) Destroy(brosse);
        }
    }

    void End()
    {
        if(nbDirt == 0) ship.repairEngine();
        else ship.errorDamageShip();
        Destroy(gameObject);
    }
}
