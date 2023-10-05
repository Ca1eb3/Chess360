// Caleb Smith
// 01/04/2023
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessGame
{
    public static class MovePatterns
    {
        public static bool Forward(TileBehaviour targetLocation, TileBehaviour currentLocation)
        {
            if (targetLocation.TileSector == currentLocation.TileSector && targetLocation.TileIndex == currentLocation.TileIndex - 1 || targetLocation.TileSector == currentLocation.TileSector && targetLocation.TileIndex == currentLocation.TileIndex + 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Radial(TileBehaviour targetLocation, TileBehaviour currentLocation)
        {
            if (Convert.ToInt32(targetLocation.TileSector) == SectorOperations.Subtract(currentLocation.TileSector) && targetLocation.TileIndex == currentLocation.TileIndex || Convert.ToInt32(targetLocation.TileSector) == SectorOperations.Add(currentLocation.TileSector) && targetLocation.TileIndex == currentLocation.TileIndex)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Diagonal(TileBehaviour targetLocation, TileBehaviour currentLocation)
        {
            if (Convert.ToInt32(targetLocation.TileSector) == SectorOperations.Subtract(currentLocation.TileSector) && targetLocation.TileIndex == currentLocation.TileIndex - 1 || Convert.ToInt32(targetLocation.TileSector) == SectorOperations.Add(currentLocation.TileSector) && targetLocation.TileIndex == currentLocation.TileIndex - 1 || Convert.ToInt32(targetLocation.TileSector) == SectorOperations.Subtract(currentLocation.TileSector) && targetLocation.TileIndex == currentLocation.TileIndex + 1 || Convert.ToInt32(targetLocation.TileSector) == SectorOperations.Add(currentLocation.TileSector) && targetLocation.TileIndex == currentLocation.TileIndex + 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}