﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class DragDrop : MonoBehaviour
{
    Vector3 screenPoint;
    Vector3 offset;
    Vector3 originalPos;
    public int piece;

    void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().sortingOrder++;
        originalPos = transform.position;
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().sortingOrder--;
        Vector3 newPos = transform.position;
        if (newPos.x > 8 || newPos.x < 0 || newPos.y > 8 || newPos.y < 0)
        {
            transform.position = originalPos;
        }
        else
		{
            Move move = new Move(UItoBoardpos(originalPos), UItoBoardpos(newPos));

            if (GameManager.instance.MovePiece(move))
            {
                transform.position = GraphicsBoard.instance.TilePosition(move.newPos);
                PieceUIManager.instance.MovePiece(transform, move);
                GameManager.instance.InitializeTurn();
            }
            else transform.position = originalPos;
		}
	}
}
