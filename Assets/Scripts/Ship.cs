using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    // render
    public AnimationCurve shipOscilation;
    public float timeElapsed;
    public bool reparing;

    // Game object
    public GameObject enginePrefab;
    public GameObject shieldPrefab;
    public GameObject reparingSystem;

    // logic
    public bool faultyEngine;
    public bool faultyShield;
    public int shipLife = 8;

    void Start()
    {
        // ship shaking animation
        shipOscilation = new AnimationCurve();
        shipOscilation.AddKey(0, -0.01f);
        shipOscilation.AddKey(1, 0.01f);
        shipOscilation.postWrapMode = WrapMode.PingPong;
        shipOscilation.preWrapMode = WrapMode.PingPong;
        timeElapsed = 0f;

        // setting up logic
        reparing = false;
        faultyEngine = false;
        faultyShield = false;
    }
   
    void Update()
    {
        // starting shield repair
        if (Input.GetKeyDown("s") && !reparing)
        {
            transform.position = new Vector2(-50,0);
            transform.localScale = new Vector2(3, 3);
            reparing = true;
            
            reparingSystem = Instantiate(shieldPrefab, new Vector3(-10, 0, 0), new Quaternion());
            reparingSystem.GetComponent<Bouclier>().ship = this;
        }

        // starting engine repair
        else if (Input.GetKeyDown("e") && !reparing)
        {
            transform.position = new Vector2(-50,0);
            transform.localScale = new Vector2(3, 3);
            reparing = true;

            reparingSystem = Instantiate(enginePrefab, new Vector3(-10, 0, 0), new Quaternion());
            reparingSystem.GetComponent<Moteur>().ship = this;
        } 

        // leaving repair menu
        else if (Input.GetKeyDown(KeyCode.Return) && reparing)
        {   
            transform.position = new Vector2(-28,0);
            transform.localScale = new Vector2(5, 5);
            reparing = false;
        }

        // applying ship shaking
        timeElapsed += Time.deltaTime * 15;
        transform.position = new Vector2(transform.position.x, transform.position.y+shipOscilation.Evaluate(timeElapsed));
    }

    public void repairEngine()
    {
        faultyEngine = false;
    }
    public void repairShield()
    {
        faultyShield = false;
    }
}