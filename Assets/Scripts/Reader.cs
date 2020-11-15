using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;

/*
The Reader class is designed to read the cards submitted by the hand and compare it against
a known series of combinations.

if ABCDE comes through, then we see if we can find it in our keys.
if found, great! that's a win!
if not found, boo, we tell them fuck off
*/

[CreateAssetMenu(fileName = "Reader", menuName = "gtco/Reader", order = 1)]
public class Reader : ScriptableObject
{

    public Text Card_Text;

    public TextAsset answerCSV;
    public Dictionary<string,string> answerKey;

    public void Init(){
        Debug.Log("Running SO");
        //separate textAsset into multiple lines
        //http://www.theappguruz.com/blog/unity-csv-parsing-unity
        answerKey = ReadCSV(answerCSV);

        //https://stackoverflow.com/questions/9791393/loading-a-csv-file-into-dictionary-i-keep-getting-the-error-cannot-convert-fr
        //this works if you want to 
        //answerKey = File.ReadLines(answerCSV.text.Select( line => line.Split(',')).ToDictionary( line => line[0].ToString(), line => line[1].ToString());
    }

    Dictionary<string,string> ReadCSV(TextAsset csv, char lineSeparater = '\n', char fieldSeparater = ','){
        
        Dictionary<string,string> result = new Dictionary<string, string>();

        string[] records = csv.text.Split(lineSeparater);

        foreach( string record in records ){
            string[] fields = record.Split(fieldSeparater);

            result.Add(fields[0],fields[1]);
        }

        return result;
    }

    //this function takes an arbitrary set of selections and then *does something else*
    public bool ReadHand(List<Card> hand){


        string key = "";

        //this is a naive (incorrect) key assembly
        foreach (Card c in hand){
            key += c.Name;
        }
        
        //if we find one, return true or otherwise we exit.
        if (answerKey.ContainsKey(key)){
            Debug.Log($"{key} found, the answer is {answerKey[key]}");
            Card_Text.text = ($"{answerKey[key]}");
            return true;
        }

        Debug.Log($"{key} not found");
        return false;
    }
}
