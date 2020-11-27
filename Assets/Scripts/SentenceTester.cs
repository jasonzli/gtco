using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentenceTester : MonoBehaviour
{
    public SentencePuzzle sp;
    public List<CardSO> cardSOs;

    public Card c1,c2,c3;
    // Start is called before the first frame update
    void Start()
    {
        sp.Clear();
        sp.Init();

        sp.AddSelection(c1);
        Debug.Log(sp.PartialSentence);
        sp.AddSelection(c2);
        Debug.Log(sp.PartialSentence);
        sp.AddSelection(c3);
        Debug.Log(sp.PartialSentence);

        Debug.Log( sp.CheckKeys() );

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
