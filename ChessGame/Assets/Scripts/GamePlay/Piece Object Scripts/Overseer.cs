// Caleb Smith
// 01/07/2023
using ChessGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overseer : GamePiece
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool MoveParameterCheck(TileBehaviour nextLocation, TileBehaviour currentLocation)
    {
        if (MovePatterns.SingleRadial(nextLocation, currentLocation) || MovePatterns.SingleForward(nextLocation, currentLocation) || MovePatterns.SingleDiagonal(nextLocation, currentLocation))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
