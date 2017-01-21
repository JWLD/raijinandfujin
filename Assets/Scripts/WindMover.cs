using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMover : MonoBehaviour
{
	[SerializeField]
	float _forceMultiplier;
	[SerializeField]
	[Range(0.0f, 1.0f)]
	float _forceDrag;

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.enabled = false;
    }

	void OnTriggerEnter(Collider col)
	{
		var cloudRB = col.gameObject.GetComponent<Rigidbody>();

		// add drag force
		var drag = -cloudRB.velocity * _forceDrag;
		cloudRB.AddForce(drag, ForceMode.Impulse);

		// add wind force
		var force = transform.forward.normalized * _forceMultiplier;
		cloudRB.AddForce(force);
	}
}
