using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class TileManager : MonoBehaviour
{
    public SpawnManagerScriptableObject scriptableObject;
    public SpawnManagerScriptableObject overObject;
    public type tipo;
    public List <SkipDay>  actions = new List<SkipDay>();
    [SerializeField] private bool watered;
    [SerializeField] private bool weeded;
    [SerializeField] public Color wateredColor;
    [SerializeField] public Color weededColor;


    public delegate void SkipDay();


    private void Start()
    {
    }

    public void checkRound()
    {
        if (watered && weeded) {
            gameObject.GetComponent<Renderer>().material.color += wateredColor;
            watered = false;
            gameObject.GetComponent<Renderer>().material.color += weededColor;
            weeded = false;
        }
        if (overObject != null && watered) Instantiate(overObject.objectToIntantiate, transform);
    }
    public void RemoveWeed() {
        gameObject.GetComponent<Renderer>().material.color += weededColor;
        weeded = true;

    }
    public bool Watered() {
        return watered;
    
    }
    public bool Weeded() {
        return weeded;
    
    }
    
    public void water ()
    {
        gameObject.GetComponent<Renderer>().material.color -= wateredColor;
        watered = true;
    }

public enum type
    {
        ground = 0, tree = 1, plant = 2

    }

}
