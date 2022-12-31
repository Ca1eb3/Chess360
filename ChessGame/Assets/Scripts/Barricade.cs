// Caleb Smith
// 12/31/2022
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChessGame;

public class Barricade : GamePiece
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool MoveParameterCheck()
    {
        if (Convert.ToInt32(NextLocation.TileSector) == SectorOperations.Subtract(TileSector) && NextLocation.TileIndex == TileIndex || Convert.ToInt32(NextLocation.TileSector) == SectorOperations.Add(TileSector) && NextLocation.TileIndex == TileIndex)
        {
            return true;
        }
        else if (NextLocation.TileSector == TileSector && NextLocation.TileIndex == TileIndex - 1 || NextLocation.TileSector == TileSector && NextLocation.TileIndex == TileIndex + 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
