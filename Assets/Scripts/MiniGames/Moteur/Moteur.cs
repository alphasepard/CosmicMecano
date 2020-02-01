using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moteur : MonoBehaviour
{
    public bool left = true;
    public int nbDirt;
    public GameObject[] dirtSprites;

    private void Start()
    {
        nbDirt = transform.childCount;
    }

    private void DeleteOneDirt()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
            nbDirt--;
        }
    }

    void Update()
    {
        if (nbDirt == 0 && Input.GetKeyDown(KeyCode.Return))
        {
            Destroy(gameObject);
        }
        else if (Input.GetKeyDown(left ? KeyCode.RightArrow : KeyCode.LeftArrow))
        {
            left = !left;
            DeleteOneDirt();
        }
    }
}
