using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class PieceUIManager : MonoBehaviour
{
    #region Singleton

    public static PieceUIManager instance;

    void Awake()
    {
        instance = this;
        pieceUIs = new Transform[64];
    }

    #endregion

    public Sprite[] pieceSprites;
    public GameObject piecePrefab;
    public Transform[] pieceUIs;

    // Update is called once per frame
    void Start()
    {
        
    }

    public void PlacePiece(int piece, int pos)
	{
        GameObject pieceToSpawn = Instantiate(piecePrefab);
        pieceToSpawn.transform.parent = transform;
        SpriteRenderer pieceRenderer = pieceToSpawn.GetComponent<SpriteRenderer>();
        GraphicsBoard.instance.tiles[pos].GetComponent<BoardTile>().piece = piece;
        
        switch(piece)
		{
            case 9:
                pieceRenderer.sprite = pieceSprites[0];
                pieceToSpawn.name = "BlancRei";
                break;
            case 10:
                pieceRenderer.sprite = pieceSprites[1];
                pieceToSpawn.name = "BlancPeo";
                break;
            case 11:
                pieceRenderer.sprite = pieceSprites[2];
                pieceToSpawn.name = "BlancCavall";
                break;
            case 12:
                pieceRenderer.sprite = pieceSprites[3];
                pieceToSpawn.name = "BlancAlfil";
                break;
            case 13:
                pieceRenderer.sprite = pieceSprites[4];
                pieceToSpawn.name = "BlancTorre";
                break;
            case 14:
                pieceRenderer.sprite = pieceSprites[5];
                pieceToSpawn.name = "BlancReina";
                break;

            case 17:
                pieceRenderer.sprite = pieceSprites[6];
                pieceToSpawn.name = "NegreRei";
                break;
            case 18:
                pieceRenderer.sprite = pieceSprites[7];
                pieceToSpawn.name = "NegrePeo";
                break;
            case 19:
                pieceRenderer.sprite = pieceSprites[8];
                pieceToSpawn.name = "NegreCavall";
                break;
            case 20:
                pieceRenderer.sprite = pieceSprites[9];
                pieceToSpawn.name = "NegreAlfil";
                break;
            case 21:
                pieceRenderer.sprite = pieceSprites[10];
                pieceToSpawn.name = "NegreTorre";
                break;
            case 22:
                pieceRenderer.sprite = pieceSprites[11];
                pieceToSpawn.name = "NegreReina";
                break;
        }

        pieceUIs[pos] = pieceToSpawn.transform;
        pieceToSpawn.GetComponent<DragDrop>().piece = piece;
        pieceToSpawn.transform.position = BoardtoUIpos(pos);
	}

    public void MovePiece(Transform piece, Move move)
	{
        pieceUIs[move.originalPos] = null;
        if (pieceUIs[move.newPos] != null) Destroy(pieceUIs[move.newPos].gameObject);
        pieceUIs[move.newPos] = piece;
	}
}
