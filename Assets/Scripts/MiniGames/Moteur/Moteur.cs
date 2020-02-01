using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moteur : MonoBehaviour
{
    public bool left = true;
    public int nbDirt;
    GameObject brosse;

    private void Start()
    {
        nbDirt = transform.childCount;
        brosse = GetComponentInChildren<Brosse>().gameObject;
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
        if (nbDirt == 0)
        {
            if (Input.GetKeyDown(KeyCode.Return)) End();
        }
        else if (Input.GetKeyDown(left ? KeyCode.RightArrow : KeyCode.LeftArrow))
        {
            left = !left;
            DeleteOneDirt();
            brosse.transform.position = new Vector3(-1 * brosse.transform.position.x, brosse.transform.position.y, brosse.transform.position.z);
            if (nbDirt == 0) Destroy(brosse);
        }
    }

    void End()
    {
        Destroy(gameObject);
    }
}
