using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
The Reader class is designed to read the cards submitted by the hand and compare it against
a known series of combinations.

if ABCDE comes through, then we see if we can find it in our keys.
if found, great! that's a win!
if not found, boo, we tell them fuck off
*/
public class Reader : MonoBehaviour
{
    
    public Dictionary<string,int> answerKey;//int is temporary

    //this function takes an arbitrary set of selections and then *does something else*
    public bool ReadHand(List<Card> hand){


        string key = "";

        //this is a naive (incorrect) key assembly
        foreach (Card c in hand){
            key += c.Name;
        }
        //if we find one, return true or otherwise we exit.
        if (answerKey.ContainsKey(key)){
            return true;
        }

        return false;
    }
}
