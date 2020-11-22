using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


public class Hand : MonoBehaviour
{
    //private int LayerMask1 = 1 << 7;
    public List<Card> selectedCards;

    public bool[] isFull;
    public GameObject[] CardSlots;

    [SerializeField]
    private Reader reader;

    public Reader readerObject;
    public Text SentenceText;
    private int NameCounter = 0;

    public Text[] Names;

    public GameObject Cards;

    [SerializeField]
    private int handLimit = 5;

    public GameObject[] HandCards;
    private int HandCounter = 0;

    void Start()
    {
        selectedCards = new List<Card>();
        readerObject.Init();
        SentenceText.GetComponent<Text>();
        SentenceText.enabled = false;

        Cards.GetComponentInChildren<MeshRenderer>();

        for (int i = 0; i < Names.Length; i++)
        {
            Names[i].enabled = false;
        }

        for (int i = 0; i < HandCards.Length; i++)
        {
            HandCards[i].SetActive(false);
        }

        //at start
        //go through the readerObject's answerKey and create a list of keys
        //as keys are found and marked, remove the keys from that List
        //if the list is equal to zero, then we know we have found all of the reader's keys.
        //and that should tell the UI "hey I'm mdone wiht this reader!"
    }

    void Update()
    {
        //potentially hand input
        //use raycast(?)
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }

        if (Input.GetMouseButtonDown(1)) 
        {
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

        if (HandCounter == 5)
        {
            /*if (readerObject.answerKey.ContainsKey(string key))
            {
                SentenceText.text = $"{ readerObject.answerKey}";
                print("working");
            }*/
            //SentenceText.enabled = true;
        }
    }

    void CastRay()
    {
        //This shouldn't be a 2D ray, we are working in 3D
        //It shouldn't be from transform, it should be from screen position.
        // Ray2D ray = new Ray2D(transform.position, transform.forward);
        // RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        //if we hit 

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;

            if (objectHit.name == "Front"){
                
                Card target;
                target = objectHit.parent.GetComponent<Card>();

                if (selectedCards.Count < 5)
                {
                    selectedCards.Add(target);
                    Debug.Log($"Chose {target.Name} whose action is {target.Word}");

                    for (int i = 0; i < CardSlots.Length; i++) // needed to make sure only one card is added per click, but is because we raycast in update loop
                    {
                        if ((isFull[i] == false) && (target.gameObject.layer == 8))
                        {

                            GameObject NewCard = Instantiate(target.gameObject, CardSlots[i].transform.position, CardSlots[i].transform.rotation);
                            NewCard.GetComponent<Card>().ApplyProperties(target.Properties);//use apply property to change the card.
                            isFull[i] = true;
                            HandCounter = HandCounter + 1;
                            NewCard.gameObject.layer = 0;
                            NewCard.gameObject.transform.GetChild(0).gameObject.layer = 0;
                            NewCard.gameObject.transform.GetChild(1).gameObject.layer = 0;
                            //target.tag = "Untagged";
                            target.gameObject.SetActive(false);
                            break;
                            /*if (target.tag == "Card")
                            {
                                
                            }*/
                        }
                    }
                    /*if (target.Name == "Parvati")
                    {
                        SentenceText.text = "Parvati ______ ______'s services to protect her right to privacy at Mt Kailash but _______ can still ______ it because _______ is his servant and not Parvati’s.";
                        NameCounter = 1;
                        print(NameCounter);
                    }

                    if (target.Name == "Seven")
                    {
                        if (NameCounter == 1)
                        {
                            SentenceText.text = "Parvati uses ______'s services to protect her right to privacy at Mt Kailash but _______ can still ______ it because _______ is his servant and not Parvati’s.";
                            NameCounter = 2;
                            print(NameCounter);
                        }
                    }

                    if (target.Name == "Nandi")
                    {
                        if (NameCounter == 2)
                        {
                            SentenceText.text = "Parvati uses Nandi's services to protect her right to privacy at Mt Kailash but _______ can still ______ it because Nandi is his servant and not Parvati’s.";
                            NameCounter = 3;
                            print(NameCounter);
                        }
                    }

                    if (target.Name == "Queen")
                    {
                        if (NameCounter == 3)
                        {
                            SentenceText.text = "Parvati uses Nandi's services to protect her right to privacy at Mt Kailash but _______ can still access it because Nandi is his servant and not Parvati’s.";
                            NameCounter = 4;
                            print(NameCounter);
                        }
                    }

                    if (target.Name == "Shiva")
                    {
                        if (NameCounter == 4)
                        {
                            SentenceText.text = "Parvati uses Nandi's services to protect her right to privacy at Mt Kailash but Shiva can still access it because Nandi is his servant and not Parvati’s.";
                            NameCounter = 5;
                            print(NameCounter);
                        }
                    }*/

                    //Text = _______ ______ ______'s services to protect her right to privacy at Mt Kailash but _______ can still ______ it because _______ is his servant and not ______’s.
                    //Text full = Parvati uses Nandi's services to protect her right to Privacy at Mt Kailash but Shiva can still access it because Nandi is his servant and not Parvati’s.

                }
                else
                {
                    ClearHand();
                    Debug.Log($"Max Hand Limit reached, clearing hand");
                    selectedCards.Add(target);
                    Debug.Log($"Chose {target.Name} whose action is {target.Word}");
                }
            }
        }
    }

    void FlipCard()
    {
        RaycastHit hit2;
        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray2, out hit2))
        {
            Transform objectHit = hit2.transform;

            if ((objectHit.name == "Front") || (objectHit.name == "Back"))
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

        //reader.ReadHand(hand) or something similar
        //if valid do something with it in ui text
        //if not then do something else
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
        readerObject.ReadHand(selectedCards);///THIS ABSOLUTELY SHOULD NOT BE HERE
        selectedCards.Clear();
    }
}
