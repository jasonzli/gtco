using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public enum InputState{
    SLIDE_STATE,
    PUZZLE_STATE,
    SOLVE_STATE
}

public class PuzzleSequence : MonoBehaviour
{
    
    [Header("Puzzle Fields")]
    [SerializeField]
    PuzzleDeck Deck;

    [SerializeField]
    SlideshowHolder Enter, Exit;


    [SerializeField]
    Hand player;

    [SerializeField]
    private bool lastPuzzle = false;
    public bool LastPuzzle{ get {return lastPuzzle;} set {lastPuzzle = value;}}

    [Header("Debug Fields, do not modify")]
    [SerializeField]
    SentencePuzzle activePuzzle;
    [SerializeField]
    int puzzleNumber;

    [SerializeField]
    private InputState handState = InputState.SLIDE_STATE;

    [SerializeField]
    PuzzleSequence NextPuzzle;


    //this is the actual thing that should have any Start() level behavior
    void OnEnable(){
        //Start the slide show for the intro
        Enter.StartSlides();
        Enter.EndShow += StartPuzzles;
        //Exit.EndShow += LoadNextPuzzleDeck;

        //Set the puzzle number to 0\
        puzzleNumber = 0;
        activePuzzle = Deck.Puzzles[puzzleNumber];
        activePuzzle.PuzzleSolved += AdvancePuzzles;

        //this is where we set up the Hand
        SetHandPuzzle();
        player.SolvedPuzzle += () => {handState = InputState.SOLVE_STATE;};//this is jank, but we use this to show the data text
        player.HandClearing += Deck.FlipUpCards;
        activePuzzle.ClearSelections();

    }

    void SetHandPuzzle(){
        player.SetPuzzle(activePuzzle);
    }

    //Initialize the deck with the puzzles in it
    [Button]
    void StartPuzzles(){
        Enter.EndShow = null;
        Deck.InitializePuzzle();
        Deck.PlaceCards();
        SwitchState(InputState.PUZZLE_STATE);
    }

    void SwitchState(InputState state){
        handState = state;
    }

    void Update(){
        switch (handState){
            case (InputState.SLIDE_STATE):
                SlideInputs();
                break;
            case (InputState.PUZZLE_STATE):
                PuzzleInputs();
                break;
            case (InputState.SOLVE_STATE):
                SolvedInputs();
                break;
            default:
                break;
        }
    }

    void SlideInputs(){
        if (Input.GetMouseButtonDown(0)){
            AdvanceSlides();
        }

    }

    void PuzzleInputs(){
        if (Input.GetMouseButtonDown(0)){
            player.handleInput();
        }
    }

    void SolvedInputs(){
        if (Input.GetMouseButtonDown(0)){
            Debug.Log("1");
            player.TransitionPuzzle();
            AdvancePuzzles();
        }
    }

    [Button]
    bool CheckPuzzles(){
        bool allSolved = true;
        for( int i = 0 ; i < Deck.Puzzles.Count ; i++){
            if (!Deck.Puzzles[i].Solved) {
                allSolved = false;
                break;
            }
        }
        Debug.Log(allSolved);
        return allSolved;


    }

    //This function fires when the active puzzle is done.
    //it advances the puzzle number
    //if we are at the end of the puzzles, then we start the end show,
    //or we move onto the next puzzle
    [Button]
    void AdvancePuzzles(){
        activePuzzle.PuzzleSolved = null;//clear event subscription;
        //clear the hand subscription;
        // and update the hand with the new puzzle


        puzzleNumber += 1;
        if(puzzleNumber >= Deck.Puzzles.Count){
            player.SolvedPuzzle = null;
            if (LastPuzzle){
                Exit.StartSlides();
            }else{
                LoadNextPuzzleDeck();//no endshow
            }
            SwitchState(InputState.SLIDE_STATE);
            return;
        }else{
            activePuzzle = Deck.Puzzles[puzzleNumber];
            activePuzzle.PuzzleSolved += AdvancePuzzles;
            SwitchState(InputState.PUZZLE_STATE);
            player.SetPuzzle(activePuzzle);
        }
    }
    [Button]
    void AdvanceSlides(){
        if (Enter.Finished){
            if (LastPuzzle){
                Exit.AdvanceSlides();
            }
        }else{
            Enter.AdvanceSlides();
        }
    }

    //might be buggy
    [Button]
    void LoadNextPuzzleDeck(){
        if( LastPuzzle ){
            //do something else?
        }
        NextPuzzle.gameObject.SetActive(true);
      //  NextPuzzle.OnEnable();
        transform.gameObject.SetActive(false);
    }
}
