using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
	public float Value;
	public Zone Zone;
}

public class Map : MonoBehaviour
{
	public GridData[,] MapData;

	int _width, _height;

	[SerializeField]
	float _fadeRate;

	public void Init(int width, int height)
	{
		_width = width;
		_height = height;
		MapData = new GridData[width, height];
	}

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		for(var i = 0; i < _width; i++)
		{
			for(var j = 0; j < _height; j++)
			{
				MapData[i, j].Value = Mathf.Clamp01(MapData[i, j].Value - (_fadeRate * Time.deltaTime));
			}
		}
	}

	void OnDrawGizmos()
	{
		for(var i = 0; i < _width; i++)
		{
			for(var j = 0; j < _height; j++)
			{
				if(MapData[i, j].Zone == Zone.Field)
				{
					Gizmos.color = new Color(0f, MapData[i, j].Value * 255f, 0f);
				}
				else
				{
					Gizmos.color = new Color(MapData[i, j].Value * 255f, 0f, 0f);
				}
				Gizmos.DrawSphere(new Vector3(i, j), 0.5f);
			}
		}
	}
}
