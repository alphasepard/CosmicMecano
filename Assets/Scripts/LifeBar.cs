using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{

    public GameObject lifePointPrefab;
    public int lifePoints;
    private GameObject[] lifePointGameObjects;

    // Start is called before the first frame update
    void Start()
    {
        lifePointGameObjects = new GameObject[lifePoints];
        for (var i = 0; i < lifePoints; i++)
        {
            lifePointGameObjects[i] = Instantiate(
                lifePointPrefab,
                new Vector3(i * 0.6f, 0, 0),
                Quaternion.identity,
                transform
            );
        }
    }

    public void RemoveLifePoint()
    {
        if (lifePoints == 0) return;
        lifePoints--;
        Destroy(lifePointGameObjects[lifePoints]);
    }
}
