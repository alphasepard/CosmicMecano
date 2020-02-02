using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    // render
    public AnimationCurve shipOscilation;
    public float timeElapsed;
    public GameObject engineSpriteOn1, engineSpriteOn2, shieldSpriteOn, milkSpriteOn, milkTooltip, shieldTooltip, engineTooltip1, engineTooltip2;
    public GameObject engineFlame1, engineFlame2;
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
    public bool faultyEngine1 = false, faultyEngine2 = false, faultyShield, faultyMilk, reparing;
    public int shipLife = 8;
    public float reparingEngineNumber = 1, nextEngineFailure1, nextEngineFailure2, nextShieldFailure, nextMilkFailure, breakShieldTimer, breakEngineTimer1, breakEngineTimer2, breakMilkTimer;
    public int dps = 20, dpsIncrease = 20, breakFrequency = 25, breakIncrease = 10;

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
        repairEngine1();
        repairEngine2();
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

        // starting engine 1 repair
        else if (Input.GetKeyDown("e") && !reparing && faultyEngine1)
        {
            transform.position = new Vector2(-50, 0);
            transform.localScale = smallShip;
            reparing = true;
            reparingEngineNumber = 1;

            MiniGameUI.ShowControlsMoteur();
            reparingSystem = Instantiate(enginePrefab);
            reparingSystem.GetComponent<Moteur>().ship = this;
        }

        // starting engine 2 repair
        else if (Input.GetKeyDown("w") && !reparing && faultyEngine2)
        {
            transform.position = new Vector2(-50, 0);
            transform.localScale = smallShip;
            reparing = true;
            reparingEngineNumber = 2;

            MiniGameUI.ShowControlsMoteur();
            reparingSystem = Instantiate(enginePrefab);
            reparingSystem.GetComponent<Moteur>().ship = this;
        }

        // starting milk repair
        else if (Input.GetKeyDown("m") && !reparing && faultyMilk)
        {
            transform.position = new Vector2(-50, 0);
            transform.localScale = smallShip;
            reparing = true;

            MiniGameUI.ShowControlsMoteur();
            reparingSystem = Instantiate(milkPrefab);
            reparingSystem.GetComponent<Moteur>().ship = this;
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
        if (timeElapsed > nextEngineFailure1)
        {
            breakEngineTimer1 = 0;
            faultyEngine1 = true;
            engineFlame1.SetActive(false);
            engineSpriteOn1.SetActive(false);
            engineTooltip1.SetActive(true);
            nextEngineFailure1 = int.MaxValue;
        }

        // check for engine failure timer
        if (timeElapsed > nextEngineFailure2)
        {
            breakEngineTimer2 = 0;
            faultyEngine2 = true;
            engineFlame2.SetActive(false);
            engineSpriteOn2.SetActive(false);
            engineTooltip2.SetActive(true);
            nextEngineFailure2 = int.MaxValue;
        }

        // check for shield failure timer
        if (timeElapsed > nextShieldFailure)
        {
            breakShieldTimer = 0;
            faultyShield = true;
            shieldSpriteOn.SetActive(false);
            shieldTooltip.SetActive(true);
            nextShieldFailure = int.MaxValue;
        }

        // check for shield failure timer
        if (timeElapsed > nextMilkFailure)
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
        var TimerBeforeDamage = dps - (timeElapsed/dpsIncrease);
        if (TimerBeforeDamage < 5) TimerBeforeDamage = 5;
        if (faultyShield)
        {
            if (breakShieldTimer * 5 % 2 > 1) shieldSpriteOn.SetActive(false);
            else shieldSpriteOn.SetActive(true);
            if (breakShieldTimer > TimerBeforeDamage)
            {
                breakShieldTimer = 0;
                takeDamage();
            }
            else breakShieldTimer += Time.deltaTime;
        }
        if (faultyEngine1)
        {
            if (breakEngineTimer1 * 5 % 2 > 1) engineSpriteOn1.SetActive(false);
            else engineSpriteOn1.SetActive(true);
            if (breakEngineTimer1 > TimerBeforeDamage)
            {
                breakEngineTimer1 = 0;
                takeDamage();
            }
            else breakEngineTimer1 += Time.deltaTime;
        }
        if (faultyEngine2)
        {
            if (breakEngineTimer2 * 5 % 2 > 1) engineSpriteOn2.SetActive(false);
            else engineSpriteOn2.SetActive(true);
            if (breakEngineTimer2 > TimerBeforeDamage)
            {
                breakEngineTimer2 = 0;
                takeDamage();
            }
            else breakEngineTimer2 += Time.deltaTime;
        }
        if (faultyMilk)
        {
            if (breakMilkTimer * 5 % 2 > 1) milkSpriteOn.SetActive(false);
            else milkSpriteOn.SetActive(true);
            if (breakMilkTimer > TimerBeforeDamage)
            {
                breakMilkTimer = 0;
                //takeDamage();
            }
            else breakMilkTimer += Time.deltaTime;
        }
    }

    private float nextFailure()
    {
        var nextFailureTiming = Random.value * (breakFrequency - (timeElapsed / breakIncrease));
        if (nextFailureTiming < 5) nextFailureTiming = 5;
        return timeElapsed + nextFailureTiming;
    }

    public void repairEngine()
    {
        if (reparingEngineNumber == 1)
        {
            faultyEngine1 = false;
            breakEngineTimer1 = 0;
            engineSpriteOn1.SetActive(true);
            engineTooltip1.SetActive(false);
            engineFlame1.SetActive(true);
            nextEngineFailure1 = nextFailure();
        } else if (reparingEngineNumber == 2)
        {
            faultyEngine2 = false;
            breakEngineTimer2 = 0;
            engineSpriteOn2.SetActive(true);
            engineTooltip2.SetActive(false);
            engineFlame2.SetActive(true);
            nextEngineFailure2 = nextFailure();
        }
        exitMiniGame();
    }

    public void repairEngine1()
    {
        faultyEngine1 = false;
        breakEngineTimer1 = 0;
        engineSpriteOn1.SetActive(true);
        engineTooltip1.SetActive(false);
        nextEngineFailure1 = nextFailure();
        exitMiniGame();
    }

    public void repairEngine2()
    {
        faultyEngine2 = false;
        breakEngineTimer2 = 0;
        engineSpriteOn2.SetActive(true);
        engineTooltip2.SetActive(false);
        nextEngineFailure2 = nextFailure();
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