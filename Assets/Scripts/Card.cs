﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    
    [SerializeField]
    private CardSO[] cardTypes;
    CardSO properties;
    public CardSO Properties {
        get {return properties;}
        set {properties = value;}
    }

    [SerializeField]
    public float rotationSpeed = 60f;

    [SerializeField]
    private Renderer frontRenderer;

    private float accumulatedSpin = 0;
    MaterialPropertyBlock block;

    //properties from the scriptable object instead of being stored here
    public string Name{ get {return properties.cardName;}}
    public string Word{ get {return properties.cardWord;}}
    
    // Start is called before the first frame update
    void Start()
    {
        //ApplyProperties();
    
    }



    //run this function when we get a CardSO that we want to apply data to
    public void ApplyProperties(CardSO prop){
        Properties = prop;
        this.name = Name;
        block = new MaterialPropertyBlock();
        frontRenderer.GetPropertyBlock(block);
        block.SetTexture("_MainTex", properties.frontImage);
        frontRenderer.SetPropertyBlock(block);
    }//we don't clone anymore, we get cards and then we apply properties

    // Update is called once per frame
    void Update()
    {
        
    }


    //No longer needed Spinning function
    public void Spinning(){
        StartCoroutine(Spin());
    }

    public IEnumerator Spin(){
        while (true){

            accumulatedSpin += rotationSpeed * Time.deltaTime;

            transform.rotation =  Quaternion.Euler(0f , 0f, accumulatedSpin);
            yield return null;
        }

        // transform.rotation = Quaternion.Euler(0f , 0f, 180f);

        // accumulatedSpin = 0;
    }
}
