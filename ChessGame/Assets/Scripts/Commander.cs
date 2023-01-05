// Caleb Smith
// 01/04/2022
using ChessGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : GamePiece
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
        if (NextLocation.TileIndex == TileIndex)
        {
            int n = SectorOperations.Subtract(TileSector);
            Sector i = (Sector)n;
            while (i != NextLocation.TileSector)
            {
                string objectName = i.ToString();
                objectName += Convert.ToString(TileIndex);
                GameObject o = GameObject.Find($"{objectName}");
                TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                if (tile.IsOccupied == true)
                {
                    break;
                }
                i = (Sector)SectorOperations.Subtract(i);
            }
            if (i.Equals(NextLocation.TileSector))
            {
                return true;
            }
            else
            {
                int l = SectorOperations.Add(TileSector);
                Sector j = (Sector)l;
                while (j != NextLocation.TileSector)
                {
                    string objectName = j.ToString();
                    objectName += Convert.ToString(TileIndex);
                    GameObject o = GameObject.Find($"{objectName}");
                    TileBehaviour tile = o.GetComponent("TileBehaviour") as TileBehaviour;
                    if (tile.IsOccupied == true)
                    {
                        break;
                    }
                    j = (Sector)SectorOperations.Add(j);
                }
                if (j.Equals(NextLocation.TileSector))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
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
        else if (MovePatterns.DiagonalMove(NextLocation, CurrentLocation))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
