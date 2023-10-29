// Caleb Smith
// 01/07/2023
using ChessGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overseer : GamePiece
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
        BitValue.Set(3, true);
        BitValue.Set(4, true);
    }

    public override bool MoveParameterCheck(TileBehaviour nextLocation, TileBehaviour currentLocation, int depth)
    {
        if (depth <= 1)
        {
            if (MovePatterns.Radial(nextLocation, currentLocation) || MovePatterns.Forward(nextLocation, currentLocation) || MovePatterns.Diagonal(nextLocation, currentLocation))
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
        else
        {
            return false;
        }
    }
}
