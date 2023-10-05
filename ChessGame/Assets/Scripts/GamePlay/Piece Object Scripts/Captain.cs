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
    public override bool MoveParameterCheck(TileBehaviour nextLocation, TileBehaviour currentLocation, int depth)
    {
        if (!OccupiedSpaceCheck(nextLocation))
        {
            return false;
        }
        if (depth <= 1 && MovePatterns.Radial(nextLocation, currentLocation))
        {
            return true;
        }
        else if (MovePatterns.Forward(nextLocation, currentLocation))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
