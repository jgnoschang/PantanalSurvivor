using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "instantiateObj", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class SpawnManagerScriptableObject : ScriptableObject
{
    public bool isGround;
    public string prefabName;
    public GameObject objectToIntantiate;
    public Sprite spriteOfObject;
    [Header("terrain configuration")]
    [Tooltip("destroy terrain when over it")]
    public bool destroyTerrain;
    public bool instantiateInPlace;
    public bool water;
    public bool overTerrain;
    
}
