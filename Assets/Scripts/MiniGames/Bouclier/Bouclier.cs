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

    Dictionary<State, KeyCode> keyCodes;
    GameObject capot, pile;
    readonly System.Random rand = new System.Random();

    static readonly KeyCode[] blocKeys = new KeyCode[] { KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D };
    static readonly KeyCode[] pileKeys = new KeyCode[] {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3,
        KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6,
        KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9
    };
    static string[] pileNames = new string[] { "small", "medium", "large" };
    static string[] blocNames = new string[] { "left", "right" };

    // Start is called before the first frame update
    void Start()
    {
        var blocs = GetComponentsInChildren<Bloc>();
        selectedBloc = rand.Next(0, blocs.Length);
        var bloc = blocs[selectedBloc].gameObject;
        var piles = bloc.GetComponentsInChildren<Pile>();
        selectedPile = rand.Next(0, piles.Length);

        pile = piles[selectedPile].gameObject;
        capot = bloc.GetComponentInChildren<Capot>().gameObject;

        SetConsignes($"Change {pileNames[selectedPile]} battery of {blocNames[selectedBloc]} panel.");

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
        if (Input.GetKeyDown(keyCodes[state])) Next();
        else if (Input.anyKeyDown) End();
    }

    void Next()
    {
        switch (state)
        {
            case State.Initial:
                capot.SetActive(false);
                state = State.CapotOpen;
                break;
            case State.CapotOpen:
                pile.SetActive(false);
                state = State.PileTaken;
                break;
            case State.PileTaken:
                pile.SetActive(true);
                state = State.PileReplaced;
                break;
            case State.PileReplaced:
                capot.SetActive(true);
                state = State.CapotClosed;
                break;
            case State.CapotClosed:
                End();
                break;
        }
    }

    void End()
    {
        if (state == State.CapotClosed) ship.repairShield();
        else ship.errorDamageShip();

        Destroy(gameObject);
    }
}
