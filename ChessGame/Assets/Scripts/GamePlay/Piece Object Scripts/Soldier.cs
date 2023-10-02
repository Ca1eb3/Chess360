// Caleb Smith
// 01/07/2023
using ChessGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : GamePiece
{
    public override bool MoveParameterCheck(TileBehaviour nextLocation, TileBehaviour currentLocation)
    {
        if (!OccupiedSpaceCheck(nextLocation))
        {
            return false;
        }
        if (MovePatterns.SingleRadial(nextLocation, currentLocation) || MovePatterns.SingleForward(nextLocation, currentLocation) || MovePatterns.SingleDiagonal(nextLocation, currentLocation))
        {
            return true;
        }
        else if (MovePatterns.SoldierBarricadeJump(nextLocation, currentLocation))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
