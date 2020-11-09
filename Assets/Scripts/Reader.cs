using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

/*
The Reader class is designed to read the cards submitted by the hand and compare it against
a known series of combinations.

if ABCDE comes through, then we see if we can find it in our keys.
if found, great! that's a win!
if not found, boo, we tell them fuck off
*/
public class Reader : MonoBehaviour
{
    
    public Dictionary<string,string> answerKey;//int is temporary

    void Awake(){
        answerKey = File.ReadLines("answerKey.csv").Select( line => line.Split(',')).ToDictionary( line => line[0], line => line[1]);
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
            return true;
        }

        Debug.Log($"{key} not found");
        return false;
    }
}
