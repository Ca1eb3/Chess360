// Caleb Smith
// 01/04/2023
using ChessGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilot : GamePiece
{
    public override void SetBitValue()
    {
        BitValue = new BitArray(5);
        BitValue.Set(0, true);
        if (Color == PieceColor.Black)
        {
            BitValue.Set(1, true);
        }
        BitValue.Set(2, true);
        BitValue.Set(4, true);
    }

    public override bool MoveParameterCheck(TileBehaviour nextLocation, TileBehaviour currentLocation, int depth)
    {
        if (MovePatterns.Diagonal(nextLocation, currentLocation))
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
