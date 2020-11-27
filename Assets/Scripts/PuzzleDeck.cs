using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class PuzzleDeck : MonoBehaviour {
    
    //Our Puzzles for this Deck, played in ORDER
    [SerializeField]
    public List<SentencePuzzle> Puzzles;

    public List<GameObject> cards = new List<GameObject>();

    public DeckRing ring;

    [Header("Card Prototypes")]
    [SerializeField]
    private CardTypesSO typeSO;
    [SerializeField]
    private GameObject cardPrefab;

    void Start(){

        //initialize if necessary
        foreach ( SentencePuzzle sp in Puzzles ){
           
            sp.Init(); //initialize the SP

            //now initialize key cards from sp
            foreach ( string key in sp.Keys ){
                GameObject go = createCard(typeSO.FindCardTypeByName(key));
                cards.Add(go);
            }
        }

    }

    GameObject createCard(CardSO properties){
        GameObject go = Instantiate(cardPrefab);
        go.name = properties.name;
        go.GetComponent<Card>().ApplyProperties(properties);
        return go;
    }

    [Button]
    void SetRing(){
        ring.DeleteCards();
        ring.cards = cards;
    }

}
