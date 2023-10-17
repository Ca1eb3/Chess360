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
        if (depth <= 1 && MovePatterns.Forward(nextLocation, currentLocation))
        {
            if (OccupiedSpaceCheck(nextLocation))
            {
                if (CanAttackCheck(nextLocation))
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
                return true;
            }
        }
        else if (MovePatterns.Radial(nextLocation, currentLocation))
        {
            if (OccupiedSpaceCheck(nextLocation))
            {
                if (CanAttackCheck(nextLocation))
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
                return true;
            }
        }
        else
        {
            return false;
        }
    }
}
