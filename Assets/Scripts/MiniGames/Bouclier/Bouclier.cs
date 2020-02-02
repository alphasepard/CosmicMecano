using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouclier : Game
{
    public enum State
    {
        Initial,
        CapotOpen,
        PileTaken,
        PileReplaced,
        CapotClosed
    }

    public KeyCode blocKey, pileKey;
    public int selectedBloc, selectedPile;
    public State state;

    public AudioClip open, close, extract, place;

    Dictionary<State, KeyCode> keyCodes;
    GameObject capot, pile;
    AudioSource audioSource;
    readonly System.Random rand = new System.Random();

    static readonly KeyCode[] blocKeys = new KeyCode[] { KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D };
    static readonly KeyCode[] pileKeys = new KeyCode[] {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3,
        KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6,
        KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9
    };
    static string[] pileNames = new string[] { "small", "medium", "large" };
    static string[] blocNames = new string[] { "left", "right" };

    static Dictionary<KeyCode, KeyCode> keyCodeAliases = new Dictionary<KeyCode, KeyCode>
    {
        { KeyCode.Alpha1, KeyCode.Keypad1},
        { KeyCode.Alpha2, KeyCode.Keypad2},
        { KeyCode.Alpha3, KeyCode.Keypad3},
        { KeyCode.Alpha4, KeyCode.Keypad4},
        { KeyCode.Alpha5, KeyCode.Keypad5},
    };

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        var blocs = GetComponentsInChildren<Bloc>();
        selectedBloc = rand.Next(0, blocs.Length);
        var bloc = blocs[selectedBloc].gameObject;
        var piles = bloc.GetComponentsInChildren<Pile>();
        selectedPile = rand.Next(0, piles.Length);

        pile = piles[selectedPile].gameObject;
        capot = bloc.GetComponentInChildren<Capot>().gameObject;

        SetConsignes($"Open <color=#d917d8><b>{blocNames[selectedBloc]} panel</b></color>, <color=#d917d8><b>Extract</b></color> the empty battery, Replace it with a <color=#d917d8><b>{pileNames[selectedPile]} battery</b></color> and <color=#d917d8><b>Close</b></color> the panel.");

        blocKey = blocKeys[selectedBloc];
        pileKey = pileKeys[selectedPile];

        keyCodes = new Dictionary<State, KeyCode> {
            { State.Initial, blocKey },
            { State.CapotOpen, KeyCode.E },
            {State.PileTaken, pileKey },
            {State.PileReplaced, KeyCode.C },
            {State.CapotClosed, KeyCode.Return }
        };
    }

    // Update is called once per frame
    void Update()
    {
        var keyCode = keyCodes[state];
        var keyCodeAlias = keyCodeAliases[keyCode];
        if (Input.GetKeyDown(keyCode) || Input.GetKeyDown(keyCodeAlias)) Next();
        else if (state == State.PileTaken && Input.anyKeyDown) End();
    }

    void Next()
    {
        switch (state)
        {
            case State.Initial:
                Play(open);
                capot.SetActive(false);
                state = State.CapotOpen;
                break;
            case State.CapotOpen:
                Play(extract);
                pile.SetActive(false);
                state = State.PileTaken;
                break;
            case State.PileTaken:
                Play(place);
                pile.SetActive(true);
                state = State.PileReplaced;
                break;
            case State.PileReplaced:
                Play(close);
                capot.SetActive(true);
                state = State.CapotClosed;
                End();
                break;
        }
    }

    void End()
    {
        StartCoroutine(EndRoutine());
    }

    IEnumerator EndRoutine()
    {
        if(state == State.CapotClosed) {
            yield return new WaitForSeconds(0.2f);
            ship.repairShield();
        }
        else ship.errorDamageShip();

        Destroy(gameObject);
    }

    void Play(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
