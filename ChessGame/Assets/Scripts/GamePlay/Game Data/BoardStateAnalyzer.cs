using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class BoardStateAnalyzer
{
    public BitArray BoardState = new BitArray(484);
    public int PieceEvaluation = 0;
    public double BoardEvaluation = 0;

    // Setup is called at the start of the game
    public void SetUp(List<GamePiece> gamePieces)
    {
        foreach (GamePiece piece in gamePieces)
        {
            piece.SetBitValue();
            int indexing = 4;
            indexing += (int)piece.TileSector * 40;
            indexing += (piece.TileIndex - 1) * 5;
            for (int i = 0; i < 5; i++)
            {
                BoardState.Set(indexing + i, Convert.ToBoolean(piece.BitValue.Get(0 + i)));
            }
        }
        CalculateBoardEvaluation();
    }

    public void UpdateBoardState(GamePiece piece, BitArray checkControl)
    {
        // procedure:
        // update 4 control bits
        // set the 5 bit sequence for the piece's current location equal to the 5 bit sequence for it's previous location
        // set the 5 bit sequence for the piece's previous location equal to 00000
        // flips the turn bit
        BoardState.Set(0, !BoardState.Get(0));
        // update the check control bits
        for (int i = 1; i < 4; i++)
        {
            BoardState.Set(i, checkControl.Get(i - 1));
        }
        // index the previous location
        int indexingPrev = 4;
        indexingPrev += (int)piece.PreviousLocation.TileSector * 40;
        indexingPrev += (piece.PreviousLocation.TileIndex - 1) * 5;

        // index the current location
        int indexingCur = 4;
        indexingCur += (int)piece.TileSector * 40;
        indexingCur += (piece.TileIndex - 1) * 5;

        // update the current location and previous location of the moved piece
        for (int i = 0; i < 5; i++)
        {
            BoardState.Set(indexingPrev + i, false);
            BoardState.Set(indexingCur + i, Convert.ToBoolean(piece.BitValue.Get(0 + i)));
        }
        CalculateBoardEvaluation();
    }

    public void CalculateBoardEvaluation()
    {
        BoardEvaluation = 0;
        PieceEvaluation = 0;
        if (BoardState[0] == false)
        {
            BoardEvaluation += .01; 
        }
        else
        {
            BoardEvaluation -= .01;
        }
        if (BoardState[1] == true)
        {
            if (BoardState[3] == true)
            {
                BoardEvaluation = -1.0;
                return;
            }
            else
            {
                BoardEvaluation = 1.0;
                return;
            }
        }
        else if (BoardState[2] == true) 
        {
            if (BoardState[3] == true)
            {
                BoardEvaluation -= .2;
            }
            else
            {
                BoardEvaluation += .2;
            }
        }
        for (int i = 4; i < 484; i = i + 5)
        {
            double boardValue = 0;
            int pieceValue = 0;
            if (BoardState[i] == false)
            {
                continue;
            }

            char[] bits = new char[3];
            int k = 0;
            for (int j = i + 2; j <= i + 4; j++)
            {
                bits[k] = BoardState[j] ? '1' : '0'; // '1' if the bit is set, '0' if not
                k++;
            }
            string pieceString = new string(bits);

            switch (pieceString)
            {
                case "000":
                    break;

                case "001":
                    break;

                case "010":
                    boardValue += .02;
                    pieceValue += 1;
                    break;

                case "011":
                    boardValue += .04;
                    pieceValue += 2;
                    break;

                case "100":
                    boardValue += .06;
                    pieceValue += 3;
                    break;

                case "101":
                    boardValue += .10;
                    pieceValue += 5;
                    break;

                case "110":
                    boardValue += .16;
                    pieceValue += 8;
                    break;

                case "111":
                    break;

                default:
                    break;
            }

            if (BoardState[i + 1] == true)
            {
                boardValue = boardValue * -1;
                pieceValue = pieceValue * -1;
            }
            BoardEvaluation += boardValue;
            PieceEvaluation += pieceValue;
        }
    }
}
