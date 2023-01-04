// Caleb Smith
// 12/31/2022
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChessGame;

public class Captain : GamePiece
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
        else if (NextLocation.TileSector == TileSector)
        {
            if (NextLocation.TileIndex < TileIndex)
            {
                int i = TileIndex - 1;
                while (i > NextLocation.TileIndex)
                {
                    string objectName = TileSector.ToString();
                    objectName += Convert.ToString(i);
                    GameObject o = GameObject.Find($"{objectName}");
                    TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                    if (tile.IsOccupied == true)
                    {
                        return false;
                    }
                    i--;
                }
                return true;
            }
            if (NextLocation.TileIndex > TileIndex)
            {
                int i = TileIndex + 1;
                while (i < NextLocation.TileIndex)
                {
                    string objectName = TileSector.ToString();
                    objectName += Convert.ToString(i);
                    GameObject o = GameObject.Find($"{objectName}");
                    TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                    if (tile.IsOccupied == true)
                    {
                        return false;
                    }
                    i++;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
