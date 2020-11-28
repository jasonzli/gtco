using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

[CreateAssetMenu(fileName = "CardTypesSO", menuName = "gtco/CardTypesSO", order = 4)]
public class CardTypesSO : ScriptableObject {
    public List<CardSO> types;

    
    public CardSO FindCardTypeByName(string name){
        CardSO type = null;
        for( int i = 0; i < types.Count; i++){
            if (types[i].cardName == name){
                type = types[i];
            }
        }
        return type;
    }

}