using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    // render
    public AnimationCurve shipOscilation;
    public float timeElapsed;
    public GameObject engineSpriteOn, shieldSpriteOn, milkSpriteOn, milkTooltip, shieldTooltip, engineTooltip;
    public MiniGame MiniGameUI;
    public LifeBar lifeRenderer;
    public Vector2 smallShip = new Vector2(2.5f, 2.5f);
    public Vector2 bigShip = new Vector2(5, 5);

    // camera shaking
    public Camera gameCamera;
    public float shake, decreaseFactor, shakeAmount;

    // Game object
    public GameObject enginePrefab, shieldPrefab, milkPrefab, reparingSystem;

    // logic
    public bool faultyEngine, faultyShield, faultyMilk, reparing;
    public int shipLife = 8;
    public float nextEngineFailure, nextShieldFailure, nextMilkFailure, breakShieldTimer, breakEngineTimer, breakMilkTimer;

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
        repairMilk();
        lifeRenderer.maxLifePoints = shipLife;
        lifeRenderer.Init();
    }

    void Update()
    {
        checkFailure();
        checkDamage();
        checkLife();

        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("Menu");

        // starting shield repair
        if (Input.GetKeyDown("s") && !reparing && faultyShield)
        {
            transform.position = new Vector2(-50, 0);
            transform.localScale = smallShip;
            reparing = true;

            MiniGameUI.ShowControlsBouclier();
            reparingSystem = Instantiate(shieldPrefab);
            reparingSystem.GetComponent<Bouclier>().ship = this;
        }

        // starting engine repair
        else if (Input.GetKeyDown("e") && !reparing && faultyEngine)
        {
            transform.position = new Vector2(-50, 0);
            transform.localScale = smallShip;
            reparing = true;

            MiniGameUI.ShowControlsMoteur();
            reparingSystem = Instantiate(enginePrefab);
            reparingSystem.GetComponent<Moteur>().ship = this;
        }

        // starting milk repair
        else if (Input.GetKeyDown("m") && !reparing && faultyEngine)
        {
            transform.position = new Vector2(-50, 0);
            transform.localScale = smallShip;
            reparing = true;

            MiniGameUI.ShowControlsMoteur();
            reparingSystem = Instantiate(milkPrefab);
            //reparingSystem.GetComponent<Moteur>().ship = this;
        }

        // applying ship shaking
        timeElapsed += Time.deltaTime;
        transform.position = new Vector2(transform.position.x, transform.position.y + shipOscilation.Evaluate(timeElapsed * 15));

        //Camera Shaking
        if (shake > 0)
        {
            gameCamera.transform.localPosition = Random.insideUnitCircle * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;
        }
    }

    private void checkFailure()
    {
        // check for engine failure timer
        if (timeElapsed > nextEngineFailure)
        {
            breakEngineTimer = 0;
            faultyEngine = true;
            engineSpriteOn.SetActive(false);
            engineTooltip.SetActive(true);
            nextEngineFailure = int.MaxValue;
        }

        // check for shield failure timer
        if (nextShieldFailure < timeElapsed)
        {
            breakShieldTimer = 0;
            faultyShield = true;
            shieldSpriteOn.SetActive(false);
            shieldTooltip.SetActive(true);
            nextShieldFailure = int.MaxValue;
        }

        // check for shield failure timer
        if (nextMilkFailure < timeElapsed)
        {
            breakMilkTimer = 0;
            faultyMilk = true;
            milkSpriteOn.SetActive(false);
            milkTooltip.SetActive(true);
            nextMilkFailure = int.MaxValue;
        }
    }

    private void checkDamage()
    {
        if (faultyShield)
        {
            if (breakShieldTimer * 5 % 2 > 1) shieldSpriteOn.SetActive(false);
            else shieldSpriteOn.SetActive(true);
            if (breakShieldTimer > 5)
            {
                breakShieldTimer = 0;
                takeDamage();
            }
            else breakShieldTimer += Time.deltaTime;
        }
        if (faultyEngine)
        {
            if (breakEngineTimer * 5 % 2 > 1) engineSpriteOn.SetActive(false);
            else engineSpriteOn.SetActive(true);
            if (breakEngineTimer > 5)
            {
                breakEngineTimer = 0;
                takeDamage();
            }
            else breakEngineTimer += Time.deltaTime;
        }
        if (faultyMilk)
        {
            if (breakMilkTimer * 5 % 2 > 1) milkSpriteOn.SetActive(false);
            else milkSpriteOn.SetActive(true);
            if (breakMilkTimer > 5)
            {
                breakMilkTimer = 0;
                takeDamage();
            }
            else breakMilkTimer += Time.deltaTime;
        }
    }

    private float nextFailure()
    {
        return timeElapsed + 5 + (Random.value * 4);
    }

    public void repairEngine()
    {
        faultyEngine = false;
        breakEngineTimer = 0;
        engineSpriteOn.SetActive(true);
        engineTooltip.SetActive(false);
        nextEngineFailure = nextFailure();
        exitMiniGame();
    }
    public void repairShield()
    {
        faultyShield = false;
        breakShieldTimer = 0;
        shieldSpriteOn.SetActive(true);
        shieldTooltip.SetActive(false);
        nextShieldFailure = nextFailure();
        exitMiniGame();
    }

    public void repairMilk() {
        faultyMilk = false;
        breakMilkTimer = 0;
        milkSpriteOn.SetActive(true);
        milkTooltip.SetActive(false);
        nextMilkFailure = nextFailure();
        exitMiniGame();
    }

    public void errorDamageShip()
    {
        exitMiniGame();
        takeDamage();
    }

    public void exitMiniGame()
    {
        transform.position = new Vector2(-28, 0);
        transform.localScale = bigShip;
        reparing = false;
        MiniGameUI.HideControls();
    }

    public void checkLife()
    {
        if (shipLife < 1) SceneManager.LoadScene("GameOver");
    }

    public void takeDamage()
    {
        shake = 0.5f;
        lifeRenderer.RemoveLifePoint();
        shipLife--;
    }
}