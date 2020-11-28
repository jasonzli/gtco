using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyButtons;

public class SlideshowHolder : MonoBehaviour
{

    [SerializeField]
    List<Texture2D> slides;

    [SerializeField]
    RawImage imageUI;

    [SerializeField]
    int slidePosition = 0;

    // Start is called before the first frame update
    void Start()
    {
        imageUI.enabled = false;
    }

    [Button]
    void StartSlides(){
        imageUI.enabled = true;
        slidePosition = 0;
        imageUI.texture = slides[slidePosition];
    }

    [Button]
    void AdvanceSlides(){

        slidePosition = Mathf.Clamp(slidePosition + 1,0, slides.Count);//just to keep the number sane

        if (slidePosition >= slides.Count){
            EndSlides();

        }else {
            imageUI.texture = slides[slidePosition];
        }


    }

    void EndSlides(){
        imageUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
