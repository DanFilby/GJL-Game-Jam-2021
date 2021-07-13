using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Color cornerColour;
    public GameObject cornerParent;


    void Start()
    {
        ChanageCornerColours(cornerColour);


    }


    void Update()
    {
        
    }


    /// <summary>
    /// Change colours of this enemy's corners
    /// </summary>
    public void ChanageCornerColours(Color newColour)
    {
        //gets each corner and changes its colour
        foreach(Transform corner in cornerParent.GetComponentInChildren<Transform>())
        {
            corner.gameObject.GetComponent<Renderer>().material.color = newColour;
        }
    }


}
