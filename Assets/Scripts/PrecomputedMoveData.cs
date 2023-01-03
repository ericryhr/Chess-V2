using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrecomputedMoveData
{
    // N, S, E, W, NW, SW, NE, SE
    public static readonly int[] DirectionOffsets = { 8, -8, 1, -1, 7, -7, 9, -9 };
    public static readonly int[][] NumSquaresToEdge;
	//Començant per a dalt a la dreta en sentit horari
	public static readonly int[] HorseOffsets = { 17, 10, -6, -15, -17, -10, 6, 15 };
	public static readonly int[][] HorseJumpsEdge;

    static PrecomputedMoveData()
	{
		NumSquaresToEdge = new int[64][];
		for (int columna = 0; columna < 8; columna++)
		{
			for (int fila = 0; fila < 8; fila++)
			{
				int n = 7 - fila;
				int s = fila;
				int e = 7 - columna;
				int w = columna;

				int index = fila * 8 + columna;

				NumSquaresToEdge[index] = new int[8]{
					n,
					s,
					e,
					w,
					System.Math.Min(n, w),
					System.Math.Min(s, e),
					System.Math.Min(n, e),
					System.Math.Min(s, w)
				};
			}
		}

		HorseJumpsEdge = new int[64][];
		for (int columna = 0; columna < 8; columna++)
		{
			for (int fila = 0; fila < 8; fila++)
			{
				int index = fila * 8 + columna;

				int primer = (index <= 46) ? 1 : 0;
				int segon = (columna <= 5 && fila <= 6) ? 1 : 0;
				int tercer = (columna <= 4 && fila >= 1) ? 1 : 0;
				int quart = (columna <= 6 && fila >= 2) ? 1 : 0;
				int cinque = (columna >= 1 && fila >= 2) ? 1 : 0;
				int sise = (columna >= 2 && fila >= 1) ? 1 : 0;
				int sete = (columna >= 2 && fila <= 6) ? 1 : 0;
				int vuite = (columna >= 1 && fila <= 5) ? 1 : 0;

				HorseJumpsEdge[index] = new int[8]
				{
					primer,
					segon,
					tercer,
					quart,
					cinque,
					sise,
					sete,
					vuite
				};
			}
		}
	}
}
