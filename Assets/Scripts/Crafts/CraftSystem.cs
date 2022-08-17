using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{
    public List<Item> craftItems = new List<Item>(); // itens que irão aparecer no menu
    public InventorySlot[] slots;
    public InventorySlot resutSlot;
    public List<Crafts> allItemsCanMake = new List<Crafts>();// Lista de todos os itens possíveis a serem criados
    public Inventory myInventory; // invetário utilizado pelo jogador

    public delegate void desativeItemsDel();
    public event desativeItemsDel desativedItems;

    public delegate void ativeItemsDel();
    public event desativeItemsDel ativedItems;

    public delegate void updateInvDel();
    public event updateInvDel updatedInv;
  

    public void InitCraft() => desativedItems?.Invoke(); // Desativa todos os itens

    public void ExitCraft()
    {
        ativedItems?.Invoke();

        if (craftItems.Count <= 0)
        {
            return;
        }
        for (int i = 0; i < craftItems.Count; i++)
        {
            myInventory.Add(craftItems[i]);
            slots[i].ClearSlot();
        }
        craftItems.Clear();
       
        updatedInv?.Invoke();
     

    }
    public void GetItem(Item i)
    {
        if (craftItems.Count > 8)
        {
            myInventory.Add(i);
            updatedInv?.Invoke();
            return;
        }
        craftItems.Add(i);

        for (int j = 0; j < craftItems.Count; j++)
        {
            slots[j].AddItem(craftItems[j]);
        }
    }

   

    public void MakeNewItem()
    {
        if (craftItems.Count >1)
        {
        
            bool result = false;
            for (int i = 0; i < allItemsCanMake.Count; i++)
            {
                string[] namesReceita = allItemsCanMake[i].itemNames;
                print("AAAAAAAAA");
                for (int j = 0; j < namesReceita.Length; j++)
                {
                    if (j> craftItems.Count)
                    {
                        return;
                    }
                    if (craftItems[j].name == namesReceita[j])
                    {

                        result = true;
                    }
                    else
                    {
                        result = false;

                        break;
                    }
                    print("aaa");
                }
                    if (result == true)
                    {
                        myInventory.Add(allItemsCanMake[i].newItem);
                        updatedInv?.Invoke();
                        resutSlot.AddItem(allItemsCanMake[i].newItem);
                        resutSlot.cantUse = true;
                       for (int e = 0; e < slots.Length; e++)
                        {
                            slots[e].ClearSlot();
                        }
                        craftItems.Clear();
                        updatedInv?.Invoke();
                        break;
                    }
                   
            }
        }
        else
        {
            print("Você n tem itens suficientes");
        }
    }
   
}
