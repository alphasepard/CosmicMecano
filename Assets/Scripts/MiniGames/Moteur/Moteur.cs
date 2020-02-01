using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moteur : MonoBehaviour
{
    public bool left = true;
    public int nbDirt;
    GameObject brosse;
    MiniGame miniGame;

    private void Start()
    {
        nbDirt = transform.childCount;
        brosse = GetComponentInChildren<Brosse>().gameObject;
        miniGame = GameObject.Find("MiniGame").GetComponent<MiniGame>();
        //miniGame.SetConsign("Clean up engine aeration");
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
            var position = brosse.transform.localPosition;
            brosse.transform.localPosition = new Vector3(-1 * position.x, position.y, position.z);
            if (nbDirt == 0) Destroy(brosse);
        }
    }

    void End()
    {
        Destroy(gameObject);
    }
}
