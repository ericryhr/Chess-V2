using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Piece
{
    public const int None = 0;
    public const int Rei = 1;
    public const int Peo = 2;
    public const int Cavall = 3;
    public const int Alfil = 4;
    public const int Torre = 5;
    public const int Reina = 6;

    public const int Blanc = 8;
    public const int Negre = 16;

    const int typeMask = 0b00111;
    const int blancMask = 0b01000;
    const int negreMask = 0b10000;
    const int colorMask = blancMask | negreMask;

    public static bool EsColor(int piece, int color)
	{
        return (piece & colorMask) == color;
	}

    public static bool EsPeçaDeslliçant(int piece)
	{
        return (piece & 0b100) != 0;
	}

    public static bool EsTipus(int piece, int tipus)
	{
        return (piece & typeMask) == tipus;
	}
}
