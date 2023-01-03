using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PrecomputedMoveData;

public class MoveGenerator
{
    List<Move> moves;
	int friendlyColor;
	int oponentColor;

    public void GenerateMoves()
	{
		Initialize();

		for (int originalPos = 0; originalPos < 64; originalPos++)
		{
			int piece = Board.Square[originalPos];
			if(Piece.EsColor(piece, Board.ColorToMove()))
			{
				if(Piece.EsPeçaDeslliçant(piece))
				{
					GenerateSlidingMoves(originalPos, piece);
				}
				if(Piece.EsTipus(piece, Piece.Cavall))
				{
					GenerateHorseMoves(originalPos, piece);
				}
				if(Piece.EsTipus(piece, Piece.Rei))
				{
					GenerateKingMoves(originalPos, piece);
				}
				if(Piece.EsTipus(piece, Piece.Peo))
				{
					GeneratePawnMoves(originalPos, piece);
				}
			}
		}
	}

	void Initialize()
	{
		moves = new List<Move>();
		friendlyColor = Board.ColorToMove();
		oponentColor = Board.OponentColor();
	}

	void GenerateSlidingMoves(int originalPos, int piece)
	{
		int startDirIndex = (Piece.EsTipus(piece, Piece.Alfil)) ? 4 : 0;
		int endDirIndex = (Piece.EsTipus(piece, Piece.Torre)) ? 4 : 8;

		for (int direction = startDirIndex; direction < endDirIndex; direction++)
		{
			for (int n = 0; n < NumSquaresToEdge[originalPos][direction]; n++)
			{
				int endPos = originalPos + DirectionOffsets[direction] * (n + 1);
				int pieceOnEndPos = Board.Square[endPos];

				//if blocked by friendly
				if (Piece.EsColor(pieceOnEndPos, friendlyColor)) break;

				moves.Add(new Move(originalPos, endPos));

				//if blocked by enemy
				if (Piece.EsColor(pieceOnEndPos, oponentColor)) break;
			}
		}
	}

	void GenerateHorseMoves(int originalPos, int piece)
	{
		for (int direction = 0; direction < 8; direction++)
		{
			if(HorseJumpsEdge[originalPos][direction] != 0)
			{
				int endPos = originalPos + HorseOffsets[direction];
				int pieceOnEndPos = Board.Square[endPos];

				if (!Piece.EsColor(pieceOnEndPos, friendlyColor))
				{
					moves.Add(new Move(originalPos, endPos));
				}
			}
		}
	}

	void GenerateKingMoves(int originalPos, int piece)
	{
		for (int direction = 0; direction < 8; direction++)
		{
			if(NumSquaresToEdge[originalPos][direction] > 0)
			{
				int endPos = originalPos + DirectionOffsets[direction];
				int pieceOnEndPos = Board.Square[endPos];

				if (!Piece.EsColor(pieceOnEndPos, friendlyColor))
				{
					moves.Add(new Move(originalPos, endPos));
				}
			}
		}
	}

	void GeneratePawnMoves(int originalPos, int piece)
	{
		//Pendent de Modificacio
		int filaDobleAvancModifier;
		int inverseModifier;
		int diagonalModifier;

		if(Piece.EsColor(piece, Piece.Blanc))
		{
			filaDobleAvancModifier = 0;
			inverseModifier = 1;
			diagonalModifier = 0;
		}
		else
		{
			filaDobleAvancModifier = 40;
			inverseModifier = -1;
			diagonalModifier = 1;
		}

		//Moviments endavant
		int endPos = originalPos + 8 * inverseModifier;
		int pieceOnEndPos = Board.Square[endPos];

		if (pieceOnEndPos == 0)
		{
			moves.Add(new Move(originalPos, endPos));
			if (originalPos >= 8 + filaDobleAvancModifier && originalPos <= 15 + filaDobleAvancModifier)
			{
				endPos = originalPos + 16 * inverseModifier;
				pieceOnEndPos = Board.Square[endPos];

				if (pieceOnEndPos == 0)
				{
					moves.Add(new Move(originalPos, endPos));
				}
			}
		}

		//Moviments laterals
		for (int i = 0; i <= 2; i += 2)
		{
			if (NumSquaresToEdge[originalPos][i + 4 + diagonalModifier] > 0)
			{
				endPos = originalPos + (i + 7) * inverseModifier;
				pieceOnEndPos = Board.Square[endPos];

				if (Piece.EsColor(pieceOnEndPos, oponentColor))
				{
					moves.Add(new Move(originalPos, endPos));
				}
			}
		}
	}

	public bool ContainsMove(Move move)
	{
		return moves.Contains(move);
	}
}

public struct Move
{
    public readonly int originalPos;
    public readonly int newPos;

	public Move(int originalPos, int newPos)
	{
		this.originalPos = originalPos;
		this.newPos = newPos;
	}
}
