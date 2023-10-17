// Caleb Smith
// 12/31/2022
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChessGame;

public class Barricade : GamePiece
{
    public override bool MoveParameterCheck(TileBehaviour nextLocation, TileBehaviour currentLocation, int depth)
    {
        if (nextLocation.IsOccupied == false && depth <= 1)
        {
            if (MovePatterns.Radial(nextLocation, currentLocation) || MovePatterns.Forward(nextLocation, currentLocation))
            {
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

    public override void UpdateSceneStatus(GameData data)
    {
            this.gameObject.transform.SetAsLastSibling();
    }
}
