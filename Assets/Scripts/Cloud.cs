using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
	Renderer _renderer;

	// Use this for initialization
	void Start()
	{
		_renderer = GetComponent<Renderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if(!_renderer.isVisible)
		{
			Destroy(gameObject);
		}
	}
}
