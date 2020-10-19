using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{

    public List<GameObject> selectedCards;

    [SerializeField]
    private int handLimit = 5;

    void Start()
    {
        selectedCards = new List<GameObject>();
    }

    void Update()
    {
        //potentially hand input
        //use raycast(?)
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Ray hit");
            CastRay();
        }
    }

    void CastRay()
    {
        Ray2D ray = new Ray2D(transform.position, transform.forward);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        Debug.Log(hit.collider.gameObject.name);
    }

    //
    void AddCard(GameObject card){
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
