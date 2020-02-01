using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouclier : MonoBehaviour
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
    public Ship ship;

    Dictionary<State, KeyCode> keyCodes;
    GameObject capot, pile;
    readonly System.Random rand = new System.Random();

    static readonly KeyCode[] blocKeys = new KeyCode[] { KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D };
    static readonly KeyCode[] pileKeys = new KeyCode[] {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3,
        KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6,
        KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9
    };

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
                ship.repairShield();
                Destroy(gameObject);
                break;
        }
    }
}
