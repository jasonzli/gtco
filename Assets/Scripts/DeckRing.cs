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
public class DeckRing : MonoBehaviour
{
    [SerializeField]
    private List<CardSO> cardTypes;
    private Dictionary<string, int> cardBreakdown; //uses cardType.Name as keys

    // the card object we use to create cards and then manually add the SO
    public GameObject cardPrefab; 
    [SerializeField]
    public float radius;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
