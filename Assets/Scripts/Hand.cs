using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


public class Hand : MonoBehaviour
{

    public List<Card> selectedCards;

    public Text[] CardName;
    private int NameLimit = 5;

    public GameObject Cards;

    [SerializeField]
    private int handLimit = 5;

    void Start()
    {
        selectedCards = new List<Card>();

        Cards.GetComponentInChildren<MeshRenderer>();
    }

    void Update()
    {
        //potentially hand input
        //use raycast(?)
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
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

                if (selectedCards.Count < 5){
                    selectedCards.Add(target);
                    Debug.Log($"Chose {target.Name} whose action is {target.Word}");
                    //CardName[0].text = target.Name;
                    //CardName.text = target.Name + " " + target.Word;
                }else{
                    ClearHand();
                    Debug.Log($"Max Hand Limit reached, clearing hand");
                    selectedCards.Add(target);
                    Debug.Log($"Chose {target.Name} whose action is {target.Word}");
                }
            }
        }

        // MeshRenderer rend = hit.transform.GetComponent<MeshRenderer>();
        
        // Debug.Log(rend);
        // if (rend == Cards.GetComponentInChildren<MeshRenderer>())
        // {
        //     Debug.Log(rend);
        // }

    }
    
    void SubmitHand(){
        //Do something with another thing that reads the hand
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
        selectedCards.Clear();
    }
}
