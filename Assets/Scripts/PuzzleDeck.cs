using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using System;

public class PuzzleDeck : MonoBehaviour {

    public Action ResetRing;
    
    //Our Puzzles for this Deck, played in ORDER
    [SerializeField]
    public List<SentencePuzzle> Puzzles;

    public List<GameObject> cards = new List<GameObject>();

    public float ringRadius;

    [Header("Card Prototypes")]
    [SerializeField]
    private CardTypesSO typeSO;
    [SerializeField]
    private GameObject cardPrefab;

    void Start(){

    }

    [Button]
    public void InitializePuzzle(){
       //initialize if necessary
        foreach ( SentencePuzzle sp in Puzzles ){
           
            sp.Init(); //initialize the SP

            //now initialize key cards from sp
            foreach ( string key in sp.Keys ){
                GameObject go = createCard(typeSO.FindCardTypeByName(key));
                go.transform.SetParent(transform);
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
    public void PlaceCards(){
        for (int i = 0 ; i < cards.Count ; i++ ){
            float a = i * Mathf.PI * 2f / cards.Count;
            Vector2 offset = new Vector2(ringRadius * Mathf.Cos(a), ringRadius * Mathf.Sin(a));
            Vector3 pos = new Vector3(transform.position.x + offset.x, transform.position.y + 2f * i / cards.Count, transform.position.z + offset.y);
            Quaternion rot =  Quaternion.Euler(0f, 90f - 360 * i / cards.Count, 180f);//face up by default
            //Y axis rotation to match the position on the ring
            //Z axis rotation to flip face up, an artifact of the prefab
            cards[i].transform.position = pos;
            cards[i].transform.localRotation = rot;
        }
    }

    [Button]
    public void FlipUpCards(){
        for (int i = 0 ; i < cards.Count ; i++ ){
            Quaternion rot =  Quaternion.Euler(0f, 90f - 360 * i / cards.Count, 180f);
            cards[i].transform.localRotation = rot;
        }
    }


}
