using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSequence : MonoBehaviour
{
    [SerializeField]
    SlideshowHolder Enter, Exit;

    [SerializeField]
    PuzzleDeck Deck;

    [SerializeField]
    SentencePuzzle activePuzzle;

    [SerializeField]
    int puzzleNumber;

    [SerializeField]
    PuzzleSequence NextPuzzle;


}
