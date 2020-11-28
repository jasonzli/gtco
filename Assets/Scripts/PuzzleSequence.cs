using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class PuzzleSequence : MonoBehaviour
{
    [SerializeField]
    SlideshowHolder Enter;//, Exit;

    [SerializeField]
    PuzzleDeck Deck;

    [SerializeField]
    SentencePuzzle activePuzzle;
    [SerializeField]
    int puzzleNumber;

    [SerializeField]
    PuzzleSequence NextPuzzle;

    [SerializeField]
    private bool lastPuzzle = false;
    public bool LastPuzzle{ get {return lastPuzzle;} set {lastPuzzle = value;}}

    //this is the actual thing that should have any Start() level behavior
    void OnEnable(){
        //Start the slide show for the intro
        Enter.StartSlides();
        //Exit.EndShow += LoadNextPuzzleDeck;

        //Set the puzzle number to 0\
        puzzleNumber = 0;
        activePuzzle = Deck.Puzzles[puzzleNumber];
        activePuzzle.PuzzleSolved += AdvancePuzzles;

        StartPuzzles();
    }

    //Initialize the deck with the puzzles in it
    [Button]
    void StartPuzzles(){
        Deck.InitializePuzzle();
        Deck.PlaceCards();
    }

    void Update(){

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
