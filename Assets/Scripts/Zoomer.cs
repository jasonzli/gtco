using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour
{
    public GameObject ZoomedCard;
    // Start is called before the first frame update
    void Start()
    {

        ZoomedCard.SetActive(false);
        //ZoomedCard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ZoomInCard();
            //show zoomed in card
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            ZoomOutCard();
            //remove zoomed in card
        }
    }

    void ZoomInCard()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            ZoomedCard.SetActive(true);
            MeshRenderer objectHit = hit.transform.gameObject.GetComponent<MeshRenderer>();

            Instantiate(objectHit.gameObject.GetComponent<MeshRenderer>(), ZoomedCard.transform);
            Debug.Log("Zoom In");

            /*if ((objectHit.name == "Front"))
            {
                Card target;
                target = objectHit.GetComponentInParent<Card>();
                Instantiate(target, ZoomedCard.transform.position, ZoomedCard.transform.rotation);
                Debug.Log("Zoom In");

                *//*if (target.tag == "Card")
                {
                    
                }*/

            /*if (hit.transform.tag == "Card")
            {
                Instantiate(target.gameObject.GetComponentInChildren<MeshRenderer>().material, ZoomedCard.transform.position, ZoomedCard.transform.rotation);
                Debug.Log("Zoom In");
            }*//*
        }*/


        }
    }

    void ZoomOutCard()
    {
        ZoomedCard.SetActive(false);
    }

}
