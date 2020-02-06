using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{

    public GameObject firstLifePointPrefab, middleLifePointPrefab, lastLifePointPrefab, middleLifePointPrefabLow, firstLifePointPrefabLow, middleLifePointPrefabCritical, firstLifePointPrefabCritical;
    public int maxLifePoints;
    public int lifePoints;
    private GameObject[] lifePointGameObjects;

    private void Start()
    {
        
    }

    // Start is called before the first frame update
    void Update()
    {
        lifePoints = maxLifePoints;
        Init();
    }

    public void RemoveLifePoint()
    {
        if (lifePoints == 0) return;
        lifePoints--;
        Destroy(lifePointGameObjects[lifePoints]);
    }

    private GameObject whichMiddleLifePointPrefab()
    {
        if (lifePoints < (maxLifePoints / 3))
            return middleLifePointPrefabCritical;
        else if (lifePoints < (maxLifePoints / 3) * 2)
            return middleLifePointPrefabLow;
        else return middleLifePointPrefab;
    }

    private GameObject whichFirstLifePointPrefab()
    {
        if (lifePoints < (maxLifePoints / 3))
            return firstLifePointPrefabCritical;
        else if (lifePoints < (maxLifePoints / 3) * 2)
            return firstLifePointPrefabLow;
        else return firstLifePointPrefab;
    }

    private void lifeBar()
    {
        while(lifePoints < maxLifePoints) RemoveLifePoint();

        Debug.Log("LIFE BAR");
        var offset = 0f;
        for (var i = 0; i < lifePoints; i++)
        {
            //RemoveLifePoint();
            var prefab = whichMiddleLifePointPrefab();

            var pas = 0.67f;
            if (i == 0) prefab = whichFirstLifePointPrefab();
            else if (i == 1) pas = 0.75f;
            else if (i == lifePoints - 1) prefab = lastLifePointPrefab;

            offset += pas;


            if (lifePointGameObjects[i])
            {
                Destroy(lifePointGameObjects[i]);
                Debug.Log("COUCOU");
            }
            lifePointGameObjects[i] = Instantiate(prefab, transform);
            lifePointGameObjects[i].transform.localPosition = new Vector3(offset, 0, 0);
        }
    }

    public void Init()
    {
        //while (lifePoints > 0)
        //{
        //    Debug.Log("coucou 1");
        //    RemoveLifePoint();
        //}

        Debug.Log("coucou 2");
        //lifePoints = ;
        lifePointGameObjects = new GameObject[lifePoints];

        lifeBar();
    }
}

