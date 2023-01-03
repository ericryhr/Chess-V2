using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion
    //Starting Fen: rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
    //Sliding Test: 8/1b3r2/8/6b1/1R3B2/3q1B2/1rQ3R1/8 w - - 0 1
    //Sliding + Horse Test: 8/1bn2r2/3N4/3N2b1/1R3B2/3q1B2/1rQ2nR1/8 w - - 0 1
    public string startingFEN = "8/1bn2r2/3N4/3N2b1/1R3B2/3q1B2/1rQ2nR1/8 w - - 0 1";
    MoveGenerator moveGenerator;
    // Start is called before the first frame update
    void Start()
    {
        LoadPositionFromFen(startingFEN);
        moveGenerator = new MoveGenerator();
        InitializeTurn();
    }

    void LoadPositionFromFen(string fen)
	{
        Dictionary<char, int> pieceTypeFromSymbol = new Dictionary<char, int>()
        {
            ['k'] = Piece.Rei,
            ['p'] = Piece.Peo,
            ['n'] = Piece.Cavall,
            ['b'] = Piece.Alfil,
            ['r'] = Piece.Torre,
            ['q'] = Piece.Reina,
        };

        string fenBoard = fen.Split(' ')[0];
        int columna = 0, fila = 7;

		foreach (char symbol in fenBoard)
		{
            if (symbol == '/')
            {
                columna = 0;
                fila--;
            }
            else
            {
                if (char.IsDigit(symbol)) columna += (int)char.GetNumericValue(symbol);
                else
                {
                    int pieceColor = (char.IsUpper(symbol)) ? Piece.Blanc : Piece.Negre;
                    int pieceType = pieceTypeFromSymbol[char.ToLower(symbol)];
                    Board.PlacePiece(pieceType | pieceColor, fila * 8 + columna);
                    PieceUIManager.instance.PlacePiece(pieceType | pieceColor, fila * 8 + columna);
                    columna++;
                }
            }
		}
	}

    public void InitializeTurn()
	{
        moveGenerator.GenerateMoves();
        Board.ChangeTurn();
	}

    public bool MovePiece(Move move)
	{
        if (moveGenerator.ContainsMove(move))
        {
            Board.MovePiece(move);
            return true;
        }
        else return false;
	}
}

/*
 * TODO LIST:
 * 
 * Moviments peo
 * Transformacions peo
 * Enroc
 * En passant
 * Escac
 * Peces fixades
 * IA
 * Matar el jordi
 * 
*/