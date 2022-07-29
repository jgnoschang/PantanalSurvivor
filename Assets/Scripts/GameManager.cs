using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{   
    [SerializeField] private GridBuildingSystem buildingSystem;
    private List<TileManager> tiles;
    [SerializeField] private Text textRounds;

    void Start()
    {
        buildingSystem = FindObjectOfType<GridBuildingSystem>();
        // buildingSystem.GetGridObjects(tiles);
    }

    void Update()
    {
        
    }
}
