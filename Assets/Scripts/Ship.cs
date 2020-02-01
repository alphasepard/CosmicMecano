using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public AnimationCurve shipOscilation;
    public Vector2 shipScale;
    public float timeElapsed;

    void Start()
    {
        shipOscilation = new AnimationCurve();
        shipScale = new Vector2(1,1);
        shipOscilation.AddKey(0, -0.02f);
        shipOscilation.AddKey(1, 0.02f);
        shipOscilation.postWrapMode = WrapMode.PingPong;
        shipOscilation.preWrapMode = WrapMode.PingPong;
        timeElapsed = 0f;
    }
   
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (Input.GetKeyDown("space"))
        {
            transform.position = new Vector2(-50,0);
            transform.localScale = new Vector2(1.5f, 1.5f);
        } 
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            transform.position = new Vector2(-28,0);
            transform.localScale = new Vector2(3f, 3f);
        }

        transform.position = new Vector2(transform.position.x, transform.position.y+shipOscilation.Evaluate(timeElapsed));
    }
}