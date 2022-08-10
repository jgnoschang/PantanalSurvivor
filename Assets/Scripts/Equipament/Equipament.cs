using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipament",menuName = "Invetory/Equipament")]
public class Equipament : Item
{
    public EquipamentSlot equipSlot;
    public int armorModifier;
    public int damageModifier;
    public override void Use()
    {
        base.Use();
    }
    public enum EquipamentSlot { Head, Chest, Legs, Weapon, Shield, Feet }

}
