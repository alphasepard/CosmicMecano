using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    // render
    public AnimationCurve shipOscilation;
    public float timeElapsed;
    public GameObject engineSpriteOn, shieldSpriteOn;
    public MiniGame MiniGameUI;
    public LifeBar lifeRenderer;
    public Vector2 smallShip = new Vector2(2.5f, 2.5f);
    public Vector2 bigShip = new Vector2(5, 5);

    // Game object
    public GameObject enginePrefab, shieldPrefab, reparingSystem;

    // logic
    public bool faultyEngine, faultyShield, reparing;
    public int shipLife = 8;
    public float nextEngineFailure, nextShieldFailure;

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
        repairEngine();
        repairShield();
        lifeRenderer.maxLifePoints = shipLife;
        lifeRenderer.Init();
    }
   
    void Update()
    {
        checkFailure();

        // starting shield repair
        if (Input.GetKeyDown("s") && !reparing && faultyShield)
        {
            transform.position = new Vector2(-50,0);
            transform.localScale = smallShip;
            reparing = true;

            MiniGameUI.ShowControlsBouclier();
            reparingSystem = Instantiate(shieldPrefab);
            reparingSystem.GetComponent<Bouclier>().ship = this;
        }

        // starting engine repair
        else if (Input.GetKeyDown("e") && !reparing && faultyEngine)
        {
            transform.position = new Vector2(-50,0);
            transform.localScale = smallShip;
            reparing = true;

            MiniGameUI.ShowControlsMoteur();
            reparingSystem = Instantiate(enginePrefab);
            reparingSystem.GetComponent<Moteur>().ship = this;
        } 

        // leaving repair menu
        else if (Input.GetKeyDown(KeyCode.Return) && reparing)
        {   
            transform.position = new Vector2(-28,0);
            transform.localScale = bigShip;
            reparing = false;
            MiniGameUI.HideControls();
        }

        // applying ship shaking
        timeElapsed += Time.deltaTime;
        transform.position = new Vector2(transform.position.x, transform.position.y+shipOscilation.Evaluate(timeElapsed*15));
    }

    private void checkFailure()
    {
        // check for engine failure timer
        if (timeElapsed > nextEngineFailure)
        {
            faultyEngine = true;
            engineSpriteOn.SetActive(false);
            nextEngineFailure = int.MaxValue;
        }

        // check for shield failure timer
        if (nextShieldFailure < timeElapsed)
        {
            faultyShield = true;
            shieldSpriteOn.SetActive(false);
            nextShieldFailure = int.MaxValue;
        }
    }

    private float nextFailure()
    {
        return timeElapsed + 5 + (Random.value * 4);
    }

    public void repairEngine()
    {
        faultyEngine = false;
        engineSpriteOn.SetActive(true);
        nextEngineFailure = nextFailure();
    }
    public void repairShield()
    {
        faultyShield = false;
        shieldSpriteOn.SetActive(true);
        nextShieldFailure = nextFailure();
    }

    public void damageShip()
    {
        lifeRenderer.RemoveLifePoint();
    }
}