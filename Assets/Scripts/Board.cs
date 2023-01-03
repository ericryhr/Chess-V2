using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Board
{
    public static int[] Square;
	static int colorToMove;
	static int oponentColor;

	static Board()
	{
		Square = new int[64];
		colorToMove = 8;
		oponentColor = 16;
	}

	public static void PlacePiece(int piece, int pos)
	{
		Square[pos] = piece;
	}

	public static void ChangeTurn()
	{
		if(colorToMove == 8)
		{
			colorToMove = 16;
			oponentColor = 8;
		}
		else
		{
			colorToMove = 8;
			oponentColor = 16;
		}
	}

	public static void MovePiece(Move move)
	{
		//Comprovacions si la partida ha acabat, si s'ha de transformar un peo
		Square[move.newPos] = Square[move.originalPos];
		Square[move.originalPos] = 0;
	}

	public static int ColorToMove()
	{
		return colorToMove;
	}

	public static int OponentColor()
	{
		return oponentColor;
	}
}
