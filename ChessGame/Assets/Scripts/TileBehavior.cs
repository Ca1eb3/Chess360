// Caleb Smith
// 12/26/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour
{
    // class variables
    private GamePiece[] OccupyingObject = new GamePiece[1] { null };

    // properties
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

    public void UpdateStatus()
    {
        UpdateOccupation();
    }

    public void UpdateOccupation()
    {
        if (OccupyingObject[0] == null)
        {
            IsOccupied = false;
        }
        else
        {
            IsOccupied = true;
        }
    }

    public bool UpdateOccupyingObject(GamePiece piece)
    {
        if (!IsOccupied)
        {

            return true;
        }
        else
        {
            return false;
        }
    }
}
