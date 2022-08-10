using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{
    public List<Item> craftItems = new List<Item>(); // itens que ir�o aparecer no menu
    public InventorySlot[] slots;
    public InventorySlot resutSlot;
    public List<Crafts> allItemsCanMake = new List<Crafts>();// Lista de todos os itens poss�veis a serem criados
    public Inventory myInventory; // invet�rio utilizado pelo jogador

    public delegate void desativeItemsDel();
    public event desativeItemsDel desativedItems;

    public delegate void ativeItemsDel();
    public event desativeItemsDel ativedItems;

    public delegate void updateInvDel();
    public event updateInvDel updatedInv;
  

    public void InitCraft() => desativedItems?.Invoke(); // Desativa todos os itens

    public void ExitCraft()
    {
        ativedItems?.Invoke(); // Reativa o uso dos itens por click

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
    public void RemoveItems()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].OnRemoveButton();
            craftItems.RemoveAt(i);
        }
    }

    public void MakeNewItem()
    {
        if (craftItems.Count >0)
        {
        
            bool result = false;
            for (int i = 0; i < allItemsCanMake.Count; i++)
            {
                string[] names = allItemsCanMake[i].itemNames;
                int amount = allItemsCanMake[i].qtdRec;
                for (int j = 0; j < names.Length; j++)
                {
                    if (j > craftItems.Count)
                    {

                        return;
                    }
                    if(craftItems[j].name== names[j]&& craftItems[j].itemAmount==amount|| craftItems.Count > 1)
                    {
                       
                        result = true;
                    }
                    else
                    {
                        print("Quantidade Insuficeiente");
                        result = false;
                        break;
                    }
                    if (result == true)
                    {
                        myInventory.Add(allItemsCanMake[i].newItem);
                        updatedInv?.Invoke();
                        resutSlot.AddItem(allItemsCanMake[i].newItem);
                        resutSlot.cantUse = true;
                        slots[i].ClearSlot();
                        break;
                    }
                    else
                    {
                        print("A Receita est� errada");
                    }

                }
                
            }
        }
        else
        {
            print("Voc� n tem itens suficientes");
        }
    }
   
}
