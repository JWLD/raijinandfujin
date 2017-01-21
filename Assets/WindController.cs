using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour 
{
	[SerializeField]
	GameObject[] windNodes;

	void Start ()
	{
		// point each node to the next
		for (int i = 0; i < windNodes.Length; i++)
		{
			if (i != windNodes.Length - 1)
			{
				Vector3 relativePos = windNodes[i+1].transform.position - windNodes[i].transform.position;
				Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.down);
				windNodes[i].transform.rotation = rotation;
			}
			else
			{
				windNodes[i].transform.rotation = windNodes[i-1].transform.rotation;
			}
		}
	}
}
