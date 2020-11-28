using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using System;


public class Hand : MonoBehaviour
{

    public List<Card> selectedCards;

    public bool[] isFull;
    public GameObject[] CardSlots;

    [SerializeField]
    private Reader reader;

    public Reader readerObject;
    public Text SentenceText;
    private int NameCounter = 0;

    public Text[] Names;

    [SerializeField]
    private int handLimit = 5;

    public List<GameObject> HandCards;

    [SerializeField]
    public int HandCounter = 0;

    public Text PromptText;

    public Text Score;
    private int S;

    [SerializeField]
    private SentencePuzzle sp;

    public List<Card> Key { get; private set; }

    enum CardModes {InRing , InHand};

    CardModes mode;

private int targetlayer;

    void Start()
    {
        selectedCards = new List<Card>();
        readerObject.Init();
        SentenceText.GetComponent<Text>();
        //SentenceText.enabled = false;
        HandCards = new List<GameObject>();
        PromptText.GetComponent<Text>();
        S = 0;
        Score.GetComponent<Text>();

        sp.ClearSelections();
        sp.Init();
        SentenceText.text = sp.PartialSentence;

        mode = CardModes.InRing;
        //at start
        //go through the readerObject's answerKey and create a list of keys
        //as keys are found and marked, remove the keys from that List
        //if the list is equal to zero, then we know we have found all of the reader's keys.
        //and that should tell the UI "hey I'm mdone wiht this reader!"
    }
    /*
    void HandleCardChoice(GameObject choice)
    {
        if (!handful)
        {
            AddCardToHand(go);
        }
        else
        {
            SubmitCardToSelection(go);
        }
    }
    */

    void Update()
    {
        //potentially hand input
        //use raycast(?)
        if (Input.GetMouseButtonDown(0))
        {
            if (mode == CardModes.InRing)
            {
                targetlayer = 8;
            }
            if (mode == CardModes.InHand)
            {
                targetlayer = 0;
            }
            //if (mode == "ring mode") targetlayer = 8;
            GameObject target = CastRay(targetlayer);
            if ((target != null) && (target.layer == 8))
            {
                AddCardToHand(target.GetComponent<Card>());
            }
            else if ((target != null) && (target.layer == 0))
            {
                Debug.Log("DONE");
                Debug.Log(mode);
                SubmitCardFromHand(target.GetComponent<Card>());
            }
            //if cast ray returns gameobject and not null
            //then do addcard to hande
            /*
             * GameObject go = CastRay("ring")
             * HandleCardChoice(go);
             */
            FlipCard();
        }

        if (Input.GetMouseButtonDown(2))
        {
            TurnCard();
        }

        if (NameCounter == 5)
        {
            Invoke("ChangeText" , 3.0f);
        }

        Score.text = S + "/4";
    }

    //GameObject CastRay
    public GameObject CastRay(int layer)
    {
        //This shouldn't be a 2D ray, we are working in 3D
        //It shouldn't be from transform, it should be from screen position.
        // Ray2D ray = new Ray2D(transform.position, transform.forward);
        // RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        //if we hit


        /*
         * raycast hit blah blah
         * if hit object later == layer paramter
         * return gameobject
         * 
         * something else handles what to do with that game object
         * probably still a function of the hand
         * but not this specific function
         * 
         */

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.transform.gameObject;
            if (objectHit.name == "Front" && objectHit.layer == layer)
            {
                GameObject target = objectHit.transform.parent.gameObject;
                return target;
            }
        }

        return null;
    }
    /*if (objectHit.name == "Front")
    {

        Card target;
        target = objectHit.parent.GetComponent<Card>();
    }*/
    //sp.AddSelection(target);
    //SentenceText.text = sp.PartialSentence;
    //sp.UpdatePartialSentence();
    /*if (target.gameObject.layer == 0)
    {
        //void SubmitCardFromHand(Card target){
        sp.AddSelection(target);
        SentenceText.text = sp.PartialSentence;
    }*/

    //void AddCardToHand(Card target){
    /*if (selectedCards.Count < 5)
    {
        selectedCards.Add(target);
        Debug.Log($"Chose {target.Name} whose action is {target.Word}");

        for (int i = 0; i < CardSlots.Length; i++) // needed to make sure only one card is added per click, but is because we raycast in update loop
        {
            //this isFull array is a problem, but jason is not sure how.
            if ((isFull[i] == false) && (target.gameObject.layer == 8))
            {

                GameObject NewCard = Instantiate(target.gameObject, CardSlots[i].transform.position, CardSlots[i].transform.rotation);
                NewCard.GetComponent<Card>().ApplyProperties(target.Properties);//use apply property to change the card.
                isFull[i] = true;
                HandCounter = HandCounter + 1;
                NewCard.gameObject.layer = 0;
                NewCard.gameObject.transform.GetChild(0).gameObject.layer = 0;
                NewCard.gameObject.transform.GetChild(1).gameObject.layer = 0;

                HandCards.Add(NewCard);
                target.gameObject.SetActive(false);
                break;
            }
        }

    }
    else
    {
        ClearHand();
        Debug.Log($"Max Hand Limit reached, clearing hand");
        selectedCards.Add(target);
        Debug.Log($"Chose {target.Name} whose action is {target.Word}");
    }*/


    void AddCardToHand(Card target)
    {
       
            selectedCards.Add(target);
            Debug.Log($"Chose {target.Name} whose action is {target.Word}");

            for (int i = 0; i < CardSlots.Length; i++) // needed to make sure only one card is added per click, but is because we raycast in update loop
            {
                //this isFull array is a problem, but jason is not sure how.
                if ((isFull[i] == false))
                {

                    GameObject NewCard = Instantiate(target.gameObject, CardSlots[i].transform.position, CardSlots[i].transform.rotation);
                    NewCard.GetComponent<Card>().ApplyProperties(target.Properties);//use apply property to change the card.
                    isFull[i] = true;
                    HandCounter = HandCounter + 1;
                    NewCard.gameObject.layer = 0;
                    NewCard.gameObject.transform.GetChild(0).gameObject.layer = 0;
                    NewCard.gameObject.transform.GetChild(1).gameObject.layer = 0;
                    HandCards.Add(NewCard);
                    target.gameObject.SetActive(false);
                    break;
                }
            }

        if (selectedCards.Count == sp.Keys.Count)
        {
            SwitchMode();
        }
    }

    void SwitchMode()
    {
        if (mode == CardModes.InRing)
        {
            mode = CardModes.InHand;
        }
        else mode = CardModes.InRing;
    }
    void RemoveFromHand (Card target)
    {
        ClearHand();
        Debug.Log($"Max Hand Limit reached, clearing hand");
        selectedCards.Add(target);
        Debug.Log($"Chose {target.Name} whose action is {target.Word}");
    }

    void SubmitCardFromHand(Card target)
    {
        if (target.gameObject.layer == 0)
        {
            sp.AddSelection(target);
            SentenceText.text = sp.PartialSentence;
        }
    }

    void FlipCard()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            if (/*(objectHit.name == "Front") || */(objectHit.name == "Back"))
            {
                Card target;

                target = objectHit.parent.GetComponent<Card>();

                target.transform.Rotate(180.0f, 0.0f, 0.0f);
            }
        }
    }

    void TurnCard ()
    {
        RaycastHit hit3;
        Ray ray3 = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray3, out hit3))
        {
            Transform objectHit = hit3.transform;

            if (objectHit.name == "Front") //|| (objectHit.name == "Back"))
            {
                Card target;

                target = objectHit.parent.GetComponent<Card>();

                target.transform.Rotate(0.0f, -180.0f, 0.0f);
            }
        }
    }

    void ChangeText ()
    {
        for (int i = 0; i < Names.Length; i++)
        {
            Names[i].enabled = true;
        }
        SentenceText.text = "User uses Smart Device which sends their audio recordings to Amazon/Google despite Users saying ‘No’ in the first place, because they want to improve their AI services.";
    }
    
    void SubmitHand(){
        //Do something with another thing that reads the hand
        print("WORKING");
        /*if (selectedCards.Count == 5)
        {
            SentenceText.enabled = true;
            SentenceText.text = reader.ReadHand(Key);

        }*/
        //reader.ReadHand(hand) or something similar
        //if valid do something with it in ui text
        //if not then do something else

        //This should be where the List UI check of the answer key choices should be

        readerObject.ReadHand(selectedCards);///THIS ABSOLUTELY SHOULD NOT BE HERE

    }

    void AddCard(Card card){
        if (selectedCards.Count > handLimit){
            Debug.Log("Hand is full");
            return;
        }

        selectedCards.Add(card);
    }

    void RemoveCard(GameObject card){
        //harder
        //have to compare card to list
        //get the index
        //remove at index
        //and then the next card has to be added in that index
        //so this is confusing

        //possible need for Set datatype
    }

    public void ClearHand(){
        //empty the hand object

        SubmitHand();

        //Restore the original cards
        foreach( Card c in selectedCards){
            
            c.gameObject.SetActive(true);
            c.transform.localRotation = Quaternion.identity;
        }
        foreach( GameObject go in HandCards){
            GameObject.Destroy(go);
        }
        for (var i = 0; i < CardSlots.Length; i++){
            isFull[i] = false;
        }
        HandCounter = 0;
        //SentenceText.enabled = false;
        selectedCards.Clear();
    }
}
