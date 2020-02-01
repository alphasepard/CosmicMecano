using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public AnimationCurve shipOscilation;
    public Vector2 shipScale;
    public float timeElapsed;
    public bool reparing;
    public GameObject moteurPrefab;
    public GameObject bouclierPrefab;

    void Start()
    {
        shipOscilation = new AnimationCurve();
        shipScale = new Vector2(1,1);
        shipOscilation.AddKey(0, -0.02f);
        shipOscilation.AddKey(1, 0.02f);
        shipOscilation.postWrapMode = WrapMode.PingPong;
        shipOscilation.preWrapMode = WrapMode.PingPong;
        timeElapsed = 0f;
        reparing = false;
    }
   
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (Input.GetKeyDown("s") && !reparing)
        {
            transform.position = new Vector2(-50,0);
            transform.localScale = new Vector2(3, 3);
            reparing = true;

            Instantiate(bouclierPrefab, new Vector3(-10, 0, 0), new Quaternion());
        }
        if (Input.GetKeyDown("e") && !reparing)
        {
            transform.position = new Vector2(-50,0);
            transform.localScale = new Vector2(3, 3);
            reparing = true;

            Instantiate(moteurPrefab, new Vector3(-10, 0, 0), new Quaternion());
        } 
        else if (Input.GetKeyDown(KeyCode.Return) && reparing)
        {   
            transform.position = new Vector2(-28,0);
            transform.localScale = new Vector2(5, 5);
            reparing = false;
        }

        transform.position = new Vector2(transform.position.x, transform.position.y+shipOscilation.Evaluate(timeElapsed));
    }
}