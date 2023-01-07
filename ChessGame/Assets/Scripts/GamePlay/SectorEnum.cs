// Caleb Smith
// 12/26/2022
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessGame
{
    public enum Sector
    {
        A, B, C, D, E, F, G, H, I, J, K, L
    }

    public static class SectorOperations
    {
        public static int Subtract(Sector sector)
        {
            if (Convert.ToInt32(sector - 1) == -1)
            {
                sector = Sector.L;
                return Convert.ToInt32(sector);
            }
            else
            {
                return Convert.ToInt32(sector - 1);
            }
        }

        public static int Add(Sector sector)
        {
            if (Convert.ToInt32(sector + 1) == 12)
            {
                sector = Sector.A;
                return Convert.ToInt32(sector);
            }
            else
            {
                return Convert.ToInt32(sector + 1);
            }
        }
    }
}
