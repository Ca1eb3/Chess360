using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour
{
    // class variables
    private GameObject[] Occupation = new GameObject[1] { null };

    // properties
    public enum Sector
    {
        A, B, C, D, E, F, G, H, I, J, K, L
    }

    [Header("Status")]
    public bool IsOccupied;
    public Sector TileSector;
    public int TileIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckOccupation()
    {
        if (Occupation[0] = null)
        {
            IsOccupied = false;
        }
        else
        {
            IsOccupied = true;
        }
    }
}
