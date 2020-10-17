using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    
    [SerializeField]
    private Texture2D[] textures;
    //list of textures
    //in reality a texture should have a state quality to it?
    //or perhaps a property rather than a repeated reference

    [SerializeField]
    public float rotationSpeed = 60f;

    [SerializeField]
    private Renderer frontRenderer;

    private float accumulatedSpin = 0;
    MaterialPropertyBlock block;
    // Start is called before the first frame update
    void Start()
    {
        block  = new MaterialPropertyBlock();
        frontRenderer.GetPropertyBlock(block);
        Debug.Log((Random.Range(0,textures.Length)));
        Debug.Log(textures[Random.Range(0,textures.Length)]);
        block.SetTexture("_MainTex", textures[Random.Range(0,textures.Length)]);
        // frontRenderer.material.SetTexture("_MainTex",textures[Random.Range(0,textures.Length)]);
        // frontRenderer.material.SetColor("_Color",Color.black);
        frontRenderer.SetPropertyBlock(block);
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
