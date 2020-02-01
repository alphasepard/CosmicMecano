using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Ship ship;

    protected void SetConsignes(string consignes)
    {
        var miniGameGameObject = GameObject.Find("MiniGame");
        if (!miniGameGameObject) return;

        miniGameGameObject.GetComponentInChildren<MiniGame>().SetConsign(consignes);
    }
}
