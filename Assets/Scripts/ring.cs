using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ring : MonoBehaviour
{
    
    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField]
    [Range(1f,100f)]
    private float radius = 100f;

    [SerializeField]
    [Range(1,20)]
    private int numberOfCards = 10;

    public float spinSpeed = 60f;

    public float randomDir;

    public List<GameObject> cards;

    void Start()
    {
        //randomDir = Random.Range(0f,1f) > .5 ? 1 : -1;
        cards = new List<GameObject>();
        for (int i = 0; i < numberOfCards; i++){
            //create card
            GameObject go = GameObject.Instantiate(cardPrefab);
            //create position transform
            float a = i * Mathf.PI *2f / numberOfCards;
            Vector3 pos = new Vector3( transform.position.x  +radius * Mathf.Cos(a) ,transform.position.y + (float)i /numberOfCards, transform.position.z+radius * Mathf.Sin(a) );
            go.transform.position = pos;
            go.GetComponent<Card>().rotationSpeed = spinSpeed;
            if ( Random.Range(0f,1f) > .75){
                go.GetComponent<Card>().Spinning();
            }
            cards.Add(go);
        }


    }

    // Update is called once per frame
    void Update()
    {
        //this is a set of code to move the cards to the correct radius
        int count =0;
        foreach( GameObject card in cards){
            float a = count * Mathf.PI *2f / numberOfCards + .4f*Time.time*randomDir;
            count++;
            Vector3 pos = new Vector3( transform.position.x  +radius * Mathf.Cos(a) ,transform.position.y+ (float)count /numberOfCards, transform.position.z+radius * Mathf.Sin(a) );
            card.transform.position = pos;
        }
    }
}
