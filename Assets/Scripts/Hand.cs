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

    public Text SentenceText;
    public List<GameObject> HandCards;

    [SerializeField]
    public int HandCounter = 0;

    public Text PromptText;

    public Text Score;
    private int S;

    private SentencePuzzle sp;

    public List<Card> Key { get; private set; }

    enum CardModes {InRing , InHand};

    CardModes mode;

    private int targetlayer;

    public void SetPuzzle(SentencePuzzle puzzle){
        sp = puzzle;
        SentenceText.text = sp.PartialSentence;
    }

    void Start()
    {
        selectedCards = new List<Card>();
        SentenceText.GetComponent<Text>();
        //SentenceText.enabled = false;
        HandCards = new List<GameObject>();
        PromptText.GetComponent<Text>();
        S = 0;
        Score.GetComponent<Text>();

        mode = CardModes.InRing;
    }
    

    void Update()
    {

        
    }

        //GameObject CastRay
    public GameObject CastRay(int layer)
    {

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


    public void handleInput(){

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
                Card targetCard = target.GetComponent<Card>();
                if (sp.Keys.Contains(targetCard.Name)) // check if the card is in the key or not of the active puzzle
                {
                    AddCardToHand(targetCard);
                }
                else{
                    targetCard.Flip();// flip the card if it's wrong
                }
            }
            else if ((target != null) && (target.layer == 0))
            {
                Debug.Log("DONE");
                Debug.Log(mode);
                SubmitCardFromHand(target.GetComponent<Card>());
            }

        Score.text = S + "/4";
    }


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
            SwitchMode();//this cleanly switches us away from adding into the hand
        }
    }


    void SubmitCardFromHand(Card target)
    {
        if (target.gameObject.layer == 0)
        {
            sp.AddSelection(target);
            SentenceText.text = sp.PartialSentence;
        }
    }


    void AddCard(Card card){
        if (selectedCards.Count > CardSlots.Length){
            Debug.Log("Hand is full");
            return;
        }

        selectedCards.Add(card);
    }

    public void ClearHand(){

        //Restore the original cards
        foreach( Card c in selectedCards){
            
            c.gameObject.SetActive(true);
        }
        foreach( GameObject go in HandCards){
            GameObject.Destroy(go);
        }
        for (var i = 0; i < CardSlots.Length; i++){
            isFull[i] = false;
        }
        HandCounter = 0;

        sp.ClearSelections();
        selectedCards.Clear();

        SwitchMode();
    }

    
    void SwitchMode()
    {
        if (mode == CardModes.InRing)
        {
            mode = CardModes.InHand;
        }
        else mode = CardModes.InRing;
    }
}
