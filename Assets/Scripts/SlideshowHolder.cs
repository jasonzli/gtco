using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyButtons;
using System;

public class SlideshowHolder : MonoBehaviour
{

    public Action EndShow;

    [SerializeField]
    List<Texture2D> slides;

    [SerializeField]
    RawImage imageUI;

    [SerializeField]
    int slidePosition = 0;

    public bool Finished{get; private set;}

    // // Start is called before the first frame update
    // void Start()
    // {
    //     Debug.Assert(imageUI != null);
    //     imageUI.enabled = false;
    // }

    [Button]
    public void StartSlides(){
        imageUI.enabled = true;
        slidePosition = 0;
        Finished = false;
        imageUI.texture = slides[slidePosition];
    }

    [Button]
    public bool AdvanceSlides(){

        slidePosition = Mathf.Clamp(slidePosition + 1,0, slides.Count);//just to keep the number sane

        if (slidePosition >= slides.Count){
            EndSlides();
            return true;
        }else {
            imageUI.texture = slides[slidePosition];
            return false;
        }

    }

    public void EndSlides(){
        imageUI.enabled = false;
        Finished = true;
        EndShow?.Invoke();
        EndShow = null;
    }

    public void test(){
        Debug.Log("hello action");
    }



}
