// Caleb Smith
// 09/24/2023
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChessGame;

namespace ChessGame
{
    public enum MoveDirection
    {
        Forward = 0, Backward = 4, Clockwise = 2, CounterClockwise = 6, DClockwiseForward = 1, DClockwiseBackward = 3, DCounterClockwiseForward = 7, DCounterClockwiseBackward = 5, None = -1
    }

    public static class MoveDirectionOperations
    {
        // might be a good idea to create a new method that implements one of these methods to add a layer of abstraction to the move operations
        public static MoveDirection FindMoveDirection(TileBehaviour StartTile, TileBehaviour EndTile)
        {
            if (Forward(StartTile) == EndTile)
            {
                return MoveDirection.Forward;
            }
            if (Backward(StartTile) == EndTile)
            {
                return MoveDirection.Backward;
            }
            if (Clockwise(StartTile) == EndTile)
            {
                return MoveDirection.Clockwise;
            }
            if (CounterClockwise(StartTile) == EndTile)
            {
                return MoveDirection.CounterClockwise;
            }
            if (DClockwiseBackward(StartTile) == EndTile)
            {
                return MoveDirection.DClockwiseBackward;
            }
            if (DClockwiseForward(StartTile) == EndTile)
            {
                return MoveDirection.DClockwiseForward;
            }
            if (DCounterClockwiseForward(StartTile) == EndTile)
            {
                return MoveDirection.DCounterClockwiseForward;
            }
            if (DCounterClockwiseBackward(StartTile) == EndTile)
            {
                return MoveDirection.DCounterClockwiseBackward;
            }
            else
            {
                return MoveDirection.None;
            }
        }
        public static TileBehaviour MoveOperator(TileBehaviour Tile, MoveDirection Direction)
        {
            switch (Direction)
            {
                case MoveDirection.Forward:
                    Tile = Forward(Tile);
                    return Tile;
                case MoveDirection.Backward:
                    Tile = Backward(Tile);
                    return Tile;
                case MoveDirection.Clockwise:
                    Tile = Clockwise(Tile);
                    return Tile;
                case MoveDirection.CounterClockwise:
                    Tile = CounterClockwise(Tile);
                    return Tile;
                case MoveDirection.DCounterClockwiseForward:
                    Tile = DCounterClockwiseForward(Tile);
                    return Tile;
                case MoveDirection.DClockwiseForward:
                    Tile = DClockwiseForward(Tile);
                    return Tile;
                case MoveDirection.DClockwiseBackward:
                    Tile = DClockwiseBackward(Tile);
                    return Tile;
                case MoveDirection.DCounterClockwiseBackward:
                    Tile = DCounterClockwiseBackward(Tile);
                    return Tile;
                default: 
                    return null;
            }
        }
        private static TileBehaviour Forward(TileBehaviour Tile)
        {
            if (Tile.TileIndex + 1 > 8)
            {
                return null;
            }
            int tileIndex = Tile.TileIndex + 1;
            Sector tileSector = Tile.TileSector;
            TileBehaviour NextTile = GameObject.Find(tileSector.ToString() + tileIndex.ToString()).GetComponent("TileBehaviour") as TileBehaviour;
            return NextTile;
        }

        private static TileBehaviour Backward(TileBehaviour Tile)
        {
            if (Tile.TileIndex - 1 < 1)
            {
                return null;
            }
            int tileIndex = Tile.TileIndex - 1;
            Sector tileSector = Tile.TileSector;
            TileBehaviour NextTile = GameObject.Find(tileSector.ToString() + tileIndex.ToString()).GetComponent("TileBehaviour") as TileBehaviour;
            return NextTile;
        }

        private static TileBehaviour Clockwise(TileBehaviour Tile)
        {
            int tileIndex = Tile.TileIndex;
            Sector tileSector = SectorOperations.AddSector(Tile.TileSector);
            TileBehaviour NextTile = GameObject.Find(tileSector.ToString() + tileIndex.ToString()).GetComponent("TileBehaviour") as TileBehaviour;
            return NextTile;
        }

        private static TileBehaviour CounterClockwise(TileBehaviour Tile)
        {
            int tileIndex = Tile.TileIndex;
            Sector tileSector = SectorOperations.SubtractSector(Tile.TileSector);
            TileBehaviour NextTile = GameObject.Find(tileSector.ToString() + tileIndex.ToString()).GetComponent("TileBehaviour") as TileBehaviour;
            return NextTile;
        }

        private static TileBehaviour DClockwiseForward(TileBehaviour Tile)
        {
            if (Tile.TileIndex + 1 > 8)
            {
                return null;
            }
            int tileIndex = Tile.TileIndex + 1;
            Sector tileSector = SectorOperations.AddSector(Tile.TileSector);
            TileBehaviour NextTile = GameObject.Find(tileSector.ToString() + tileIndex.ToString()).GetComponent("TileBehaviour") as TileBehaviour;
            return NextTile;
        }

        private static TileBehaviour DClockwiseBackward(TileBehaviour Tile)
        {
            if (Tile.TileIndex - 1 < 1)
            {
                return null;
            }
            int tileIndex = Tile.TileIndex - 1;
            Sector tileSector = SectorOperations.AddSector(Tile.TileSector);
            TileBehaviour NextTile = GameObject.Find(tileSector.ToString() + tileIndex.ToString()).GetComponent("TileBehaviour") as TileBehaviour;
            return NextTile;
        }

        private static TileBehaviour DCounterClockwiseForward(TileBehaviour Tile)
        {
            if (Tile.TileIndex + 1 > 8)
            {
                return null;
            }
            int tileIndex = Tile.TileIndex + 1;
            Sector tileSector = SectorOperations.SubtractSector(Tile.TileSector);
            TileBehaviour NextTile = GameObject.Find(tileSector.ToString() + tileIndex.ToString()).GetComponent("TileBehaviour") as TileBehaviour;
            return NextTile;
        }

        private static TileBehaviour DCounterClockwiseBackward(TileBehaviour Tile)
        {
            if (Tile.TileIndex - 1 < 1)
            {
                return null;
            }
            int tileIndex = Tile.TileIndex - 1;
            Sector tileSector = SectorOperations.SubtractSector(Tile.TileSector);
            TileBehaviour NextTile = GameObject.Find(tileSector.ToString() + tileIndex.ToString()).GetComponent("TileBehaviour") as TileBehaviour;
            return NextTile;
        }
    }
}
