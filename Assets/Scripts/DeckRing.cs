using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
The ring should have the following properties
 * a set of cards to choose from
 * how many of each card to show
 * a radial size
 * a location
 * possibly an ability to move
*/
using UnityEngine;

//[CreateAssetMenu(fileName = "DeckRing", menuName = "gtco/DeckRing", order = 1)]
public class DeckRing : MonoBehaviour {
    
    public List<CardSO> cardTypes;
    public  Dictionary<string, int> cardBreakdown; //uses cardType.Name as keys

    // the card object we use to create cards and then manually add the SO
    public GameObject cardPrefab; 

    public List<GameObject> cards = new List<GameObject>();

    [SerializeField]
    public float spiralRotation = 1f;
    [SerializeField]
    public float size = 4f;
    [SerializeField]
    [Range(0.01f, 20f)]
    public float k = .1f;

    CardSO FindCardTypeByName(string name, List<CardSO> list){
        CardSO type = null;
        for( int i = 0; i < list.Count; i++){
            if (list[i].cardName == name){
                type = list[i];
            }
        }
        return type;
    }

    void createCard(){//https://stackoverflow.com/questions/141088/what-is-the-best-way-to-iterate-over-a-dictionary
        foreach (var cardPair in cardBreakdown){
            for( int i = 0; i <  cardPair.Value; i++){
                GameObject o = Instantiate(cardPrefab);
                o.transform.SetParent(transform);
                o.name = cardPair.Key;
                o.GetComponent<Card>().Properties = FindCardTypeByName(cardPair.Key, cardTypes);
                cards.Add(o);
            }
        }
    }

    
    void PlaceCards(){
        //form a ring around the parent
        //also apply the matrix to the position.
        for (int i = 0; i < cards.Count; i++){
          //  float a = i * Mathf.PI * 2f / cards.Count * spirals;
           // float r = radius * i / cards.Count;
           // float r = radius * Mathf.Exp(a * .25f) * i/ cards.Count;
            float a = Mathf.Log(k* spiralRotation * (i+.01f) / cards.Count)/k; 
            float r = size * i / cards.Count;
            Vector2 offset = new Vector2( r * Mathf.Cos(a), r * Mathf.Sin(a));
            Vector3 pos = new Vector3(transform.position.x + offset.x, transform.position.y + 2f*(1f-i/cards.Count), transform.position.z + offset.y);
            cards[i].transform.position = pos;
            
            //don't worry about matrix application because we are properly parented now
            // Matrix4x4 parentMat = transform.localToWorldMatrix;
            // cards[i].transform.position = parentMat.MultiplyVector(pos);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        cardBreakdown = SetCardDictionary();
        createCard();
        PlaceCards();
    }

    void Update(){
        PlaceCards();
    }
    Dictionary<string,int> SetCardDictionary()//might need json.NET, until then...
    {
        Dictionary<string,int> d = new Dictionary<string, int>();
        d.Add("Ace", 2);
        d.Add("Jack", 3);
        d.Add("King", 4);
        d.Add("Nandi", 1);
        d.Add("Nine", 2);
        d.Add("Parvati", 4);
        d.Add("Queen",3);
        d.Add("Seven", 2);
        d.Add("Shiva", 1);
        d.Add("Ten",3);
        


        return d;
    }
    
}
