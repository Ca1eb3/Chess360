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

    public override bool MoveParameterCheck(TileBehaviour nextLocation, TileBehaviour currentLocation)
    {
        if (MovePatterns.SingleRadial(nextLocation, currentLocation))
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
