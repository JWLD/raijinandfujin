using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Zone
{
	None,
	Field,
	Town,
}

public class GridTrigger : MonoBehaviour
{
	public int x, y;

	[SerializeField]
	Zone _zone;

	Map _map;
	float _incrementAmount = 0.1f;

	// Use this for initialization
	void Start()
	{
		if(_zone != Zone.Town)
		{
			_zone = Zone.Field;
		}

		_map = FindObjectOfType<Map>();
		_map.MapData[x, y] = new GridData { Value = 0f, Zone = _zone };
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void OnTriggerStay()
	{
		if(_zone == Zone.None)
		{
			return;
		}

		_map.MapData[x, y].Value = Mathf.Clamp01(_map.MapData[x, y].Value + _incrementAmount);
	}
}
