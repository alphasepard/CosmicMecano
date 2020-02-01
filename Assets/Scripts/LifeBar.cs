using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{

    public GameObject firstLifePointPrefab, middleLifePointPrefab, lastLifePointPrefab;
    public int maxLifePoints;
    int lifePoints;
    private GameObject[] lifePointGameObjects;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void RemoveLifePoint()
    {
        if (lifePoints == 0) return;
        lifePoints--;
        Destroy(lifePointGameObjects[lifePoints]);
    }

    public void Init()
    {
        while (lifePoints > 0) RemoveLifePoint();

        lifePoints = maxLifePoints;
        lifePointGameObjects = new GameObject[lifePoints];
        var offset = 0f;
        for (var i = 0; i < lifePoints; i++)
        {
            var prefab = middleLifePointPrefab;
            var pas = 0.67f;

            if (i == 0) prefab = firstLifePointPrefab;
            else if (i == 1) pas = 0.75f;
            else if (i == lifePoints - 1) prefab = lastLifePointPrefab;

            offset += pas;

            lifePointGameObjects[i] = Instantiate(prefab, transform);
            lifePointGameObjects[i].transform.localPosition = new Vector3(offset, 0, 0);
        }
    }
}
