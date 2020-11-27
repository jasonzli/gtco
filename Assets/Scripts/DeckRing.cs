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


//[CreateAssetMenu(fileName = "DeckRing", menuName = "gtco/DeckRing", order = 1)]
public class DeckRing : MonoBehaviour {
    
    public List<CardSO> cardTypes;
    public  Dictionary<string, int> cardBreakdown; //uses cardType.Name as keys

    // the card object we use to create cards and then manually add the SO
    public GameObject cardPrefab; 

    public List<GameObject> cards = new List<GameObject>();

    [SerializeField]
    private Hand hand;

    [SerializeField]
    public float radius = 1f;

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
                CardSO prop =  FindCardTypeByName(cardPair.Key, cardTypes);
                o.GetComponent<Card>().ApplyProperties( prop );
                cards.Add(o);
            }
        }
    }

    public void ResetCardRotations(){
        foreach ( GameObject card in cards){
            card.transform.localRotation = Quaternion.identity;
        }
    }
    
    void PlaceCards(){
        //form a ring around the parent
        //also apply the matrix to the position.
        for (int i = 0; i < cards.Count; i++)
        {

            float a = i * Mathf.PI * 2f / cards.Count;
            Vector2 offset = new Vector2(radius * Mathf.Cos(a), radius * Mathf.Sin(a));
            Vector3 pos = new Vector3(transform.position.x + offset.x, transform.position.y + 2f * i / cards.Count, transform.position.z + offset.y);
            cards[i].transform.position = pos;
            cards[i].transform.rotation = Quaternion.Euler(0.0f, 180.0f, -180.0f);



            //don't worry about matrix application because we are properly parented now
            // Matrix4x4 parentMat = transform.localToWorldMatrix;
            // cards[i].transform.position = parentMat.MultiplyVector(pos);
        }
        //FlippedCards();
    }

    void FlippedCards()
    {
        if (hand.HandCounter == 0)
        {
            cards[0].transform.rotation = Quaternion.Euler(0.0f, 180.0f, -180.0f);
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
        d.Add("AceC", 1);
        d.Add("NineC", 2);
        d.Add("NineD", 2);
        d.Add("NineS", 2);
        d.Add("EightD", 2);
        d.Add("EightH", 2);
        d.Add("EightC", 2);
        d.Add("QueenC",3);
        d.Add("QueenH",3);
        d.Add("SevenH", 2);
        d.Add("KingH", 1);
        d.Add("SixC",2);
        d.Add("AceS", 1);
        


        return d;
    }

    public bool DeleteCards(){
        foreach( GameObject c in cards ){
            Destroy(c);
        }
        return true;
    }
    
}

/*
//  float a = i * Mathf.PI * 2f / cards.Count * spirals;
           // float r = radius * i / cards.Count;
           // float r = radius * Mathf.Exp(a * .25f) * i/ cards.Count;
            float a = Mathf.Log(k* spiralRotation * (i+.01f) / cards.Count)/k; 
            float r = size * i / cards.Count;
            Vector2 offset = new Vector2( r * Mathf.Cos(a), r * Mathf.Sin(a));
            Vector3 pos = new Vector3(transform.position.x + offset.x, transform.position.y + 2f*(1f-i/cards.Count), transform.position.z + offset.y);
           
*/