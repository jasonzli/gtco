using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public enum InputState{
    SLIDE_STATE,
    PUZZLE_STATE
}

public class PuzzleSequence : MonoBehaviour
{
    [SerializeField]
    SlideshowHolder Enter;//, Exit;

    [SerializeField]
    PuzzleDeck Deck;

    [SerializeField]
    Hand player;

    [SerializeField]
    SentencePuzzle activePuzzle;
    [SerializeField]
    int puzzleNumber;

    [SerializeField]
    private InputState handState = InputState.SLIDE_STATE;

    [SerializeField]
    PuzzleSequence NextPuzzle;

    [SerializeField]
    private bool lastPuzzle = false;
    public bool LastPuzzle{ get {return lastPuzzle;} set {lastPuzzle = value;}}

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
        puzzleNumber += 1;
        if(puzzleNumber >= Deck.Puzzles.Count){
            LoadNextPuzzleDeck();//no endshow
            return;
        }else{
            activePuzzle = Deck.Puzzles[puzzleNumber];
            activePuzzle.PuzzleSolved += AdvancePuzzles;
        }
    }
    [Button]
    void AdvanceSlides(){
        if (Enter.Finished){
          //  Exit.AdvanceSlides();
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
