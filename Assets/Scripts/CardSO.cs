using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Card", menuName = "gtco/CardSO", order = 0)]
public class CardSO : ScriptableObject {
    
    public string cardName;
    public string cardWord;
    public Texture frontImage;

}