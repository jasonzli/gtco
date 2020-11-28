using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using System;


[CreateAssetMenu(fileName = "SentencePuzzle", menuName = "gtco/Sentence Puzzle", order = 2)]
public class SentencePuzzle : ScriptableObject {

    public Action PuzzleSolved;
    [Header("CSV Settings")]
    
    public TextAsset puzzleIdentityCSV;
    public bool ReadFromCSV = false;

    [Header("Puzzle Details")]
    //Example _ knows that the existence of _ _ from her being.
    [SerializeField]
    private string blankSentence;
    public string BlankSentence{ get {return blankSentence;} set {blankSentence = value;}}
    
    // Parvati knows that the existence of Mount Kailash extends from her being.
    //'	' ,<- the tab character
    [SerializeField]
    private string mythSentence;
    public string MythSentence{ get {return mythSentence;} set {mythSentence = value;}}
    
    
    // Data is an extension of a user’s behavior online on websites like Google’s.
    [SerializeField]
    private string dataSentence;
    public string DataSentence{ get {return dataSentence;} set {dataSentence = value;}}

    public string PartialSentence { get; private set;}

    public List<string> Keys;
    
    public List<string> Decoys;

    public List<Card> Selections;

    public bool Solved { get; private set;}

    void Start(){
        Init();
    }

    public void Clear(){
        DataSentence = "";
        MythSentence = "";
        BlankSentence = "";
        Keys.Clear();
        Selections.Clear();

    }
    public void Init(){
        if (ReadFromCSV) {
            Clear();
            CreateSentences(puzzleIdentityCSV);
        }

        UpdatePartialSentence();
        Debug.Log(PartialSentence);
    }

    public bool UpdatePartialSentence(){
        string result = BlankSentence;

        //selections should be in order
        //get the length of selections and then loop through the string and replace stuff
        int selectionLength = Selections.Count;
        for (int i = 0 ; i < selectionLength ; i++ ){
            result = ReplaceFirst(result, "_", Selections[i].Word);
        }

        PartialSentence = result;
        return true;
    }

    //add cards to the selection
    public bool AddSelection(Card choice){
        if (Selections.Count + 1  > Keys.Count) {
            Debug.Log("puzzle selections are full! you should clear before this!");
            return false;
        }
        if (!Keys.Contains(choice.Name)){
            Debug.Log("This key is invalid!");
            return false;
        }

        Selections.Add(choice);//add cards
        UpdatePartialSentence();//update partial sentence
        return true;
    }

    

    //check cards against key
    public bool CheckKeys(){
        int keyLen = Keys.Count;
        int validSelections = 0;
        for ( int i = 0 ; i < keyLen ; i++ ){
            //pass if the selection doesn't exist
            if (i >= Selections.Count){//if we are at the end of the selections count
                break;
            }

            //check against key in the same position
            //ORDER MATTERS
            if (Keys[i] == Selections[i].Name){
                validSelections++;
            }
        }

        //false if we are not matching
        if (validSelections != keyLen){
            Solved = false;
            return false;
        }

        //true if we are!
        Solved = true;
        return true;

    }

    //empty the choices
    public bool ClearSelections(){
        Selections.Clear();
        return true;
    }


    [Button]
    //deal with this later
    bool CreateSentences(TextAsset csv, char lineSeparater = '\n', char fieldSeparater = '	'){ 
        string[] columns = csv.text.Split(lineSeparater);
        foreach( string column in columns ){
            string[] sentenceData = column.Split(fieldSeparater);

            //myth, blank, data, keys
            MythSentence = sentenceData[0];
            BlankSentence = sentenceData[1];
            DataSentence = sentenceData[2];

            //Assemble Keys
            string[] keys = sentenceData[3].Split(',');
            int keyLen = keys.Length;
            for ( int i = 0 ; i < keyLen ; i++ ){
                Keys.Add(keys[i]);
            }

        }

        return true;
    }

    void ResetSelections(){
        Selections.Clear();
        Solved = false;
    }

    ///////////////////////
    ///
    ///   Utilies
    ///
    /////////////////////////////

    //non regex solution https://stackoverflow.com/questions/8809354/replace-first-occurrence-of-pattern-in-a-string
    public string ReplaceFirst(string text, string search, string replace)
    {
        int pos = text.IndexOf(search);
        if (pos < 0)
            {
                return text;
            }
        return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
    }
}
