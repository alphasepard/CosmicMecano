using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{

    public GameObject lifePointPrefab;
    public Sprite firstLifePointSprite, middleLifePointSprite, lastLifePointSprite, middleLifePointSpriteLow, firstLifePointSpriteLow, middleLifePointSpriteCritical, firstLifePointSpriteCritical;
    public int maxLifePoints;
    int lifePoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private Sprite whichMiddleLifePointSprite()
    {
        if (lifePoints <= (maxLifePoints / 3))
            return middleLifePointSpriteCritical;
        else if (lifePoints <= (maxLifePoints / 3) * 2)
            return middleLifePointSpriteLow;
        else return middleLifePointSprite;
    }

    private Sprite whichFirstLifePointSprite()
    {
        if (lifePoints <= (maxLifePoints / 3))
            return firstLifePointSpriteCritical;
        else if (lifePoints <= (maxLifePoints / 3) * 2)
            return firstLifePointSpriteLow;
        else return firstLifePointSprite;
    }

    public void RemoveLifePoint()
    {
        if (lifePoints == 0) return;
        lifePoints--;

        Destroy(transform.GetChild(lifePoints).gameObject);

        for (var i = 0; i < lifePoints; i++)
        {
            var sprite = i == 0 ? whichFirstLifePointSprite() : whichMiddleLifePointSprite();
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = sprite;
        }
        
    }

    public void Init()
    {
        while (lifePoints > 0) RemoveLifePoint();

        lifePoints = maxLifePoints;

        var offset = 0f;

        for (var i = 0; i < lifePoints; i++)
        {

            var instance = Instantiate(lifePointPrefab, transform);

            var sprite = middleLifePointSprite;

            var pas = 0.67f;

            if (i == 0) sprite = firstLifePointSprite;
            else if (i == 1) pas = 0.75f;
            else if (i == lifePoints - 1) sprite = lastLifePointSprite;

            offset += pas;

            instance.GetComponent<SpriteRenderer>().sprite = sprite;

            instance.transform.localPosition = new Vector3(offset, 0, 0);
        }
    }
}
