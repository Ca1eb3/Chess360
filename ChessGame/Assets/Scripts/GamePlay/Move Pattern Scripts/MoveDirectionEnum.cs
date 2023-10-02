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
        Forward, Backward, Clockwise, CounterClockwise, DClockwiseForward, DClockwiseBackward, DCounterClockwiseForward, DCounterClockwiseBackward
    }

    public static class MoveDirectionOperations
    {
        public static TileBehaviour Forward(TileBehaviour Tile)
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

        public static TileBehaviour Backward(TileBehaviour Tile)
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

        public static TileBehaviour Clockwise(TileBehaviour Tile)
        {
            int tileIndex = Tile.TileIndex;
            Sector tileSector = SectorOperations.AddSector(Tile.TileSector);
            TileBehaviour NextTile = GameObject.Find(tileSector.ToString() + tileIndex.ToString()).GetComponent("TileBehaviour") as TileBehaviour;
            return NextTile;
        }

        public static TileBehaviour CounterClockwise(TileBehaviour Tile)
        {
            int tileIndex = Tile.TileIndex;
            Sector tileSector = SectorOperations.SubtractSector(Tile.TileSector);
            TileBehaviour NextTile = GameObject.Find(tileSector.ToString() + tileIndex.ToString()).GetComponent("TileBehaviour") as TileBehaviour;
            return NextTile;
        }

        public static TileBehaviour DClockwiseForward(TileBehaviour Tile)
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

        public static TileBehaviour DClockwiseBackward(TileBehaviour Tile)
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

        public static TileBehaviour DCounterClockwiseForward(TileBehaviour Tile)
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

        public static TileBehaviour DCounterClockwiseBackward(TileBehaviour Tile)
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
