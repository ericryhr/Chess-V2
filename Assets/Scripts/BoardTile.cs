using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    public int piece = 0;
	public int index;

	void OnMouseEnter()
	{
		//print("entra a " + transform.name);
	}

	void OnMouseExit()
	{
		//print("surt de " + transform.name);
	}
}
