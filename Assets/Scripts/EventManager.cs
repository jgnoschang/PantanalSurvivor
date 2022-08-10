using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    InventoryUI inv;
    CraftSystem crafts;
    void Start()
    {
        inv = FindObjectOfType<InventoryUI>();
        crafts = FindObjectOfType<CraftSystem>();

        crafts.updatedInv += inv.UpdateUI;
        crafts.ativedItems += inv.AtiveActItems;
        crafts.desativedItems += inv.InativeActItems;

       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
