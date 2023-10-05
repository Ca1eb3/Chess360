// Caleb Smith
// 12/31/2022
using ChessGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class General : GamePiece
{
    public override bool MoveParameterCheck(TileBehaviour nextLocation, TileBehaviour currentLocation, int depth)
    {
        if (!OccupiedSpaceCheck(nextLocation))
        {
            return false;
        }
        if (depth <= 1 && MovePatterns.Forward(nextLocation, currentLocation))
        {
            return true;
        }
        else if (MovePatterns.Radial(nextLocation, currentLocation))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
