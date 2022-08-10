using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CraftReference",menuName ="Mecânicas Test/CraftReference",order =0)]
public class Crafts : ScriptableObject
{
    public string[] itemNames;
    public Item newItem;
    public int itemAmount;
    public int qtdRec;
}
