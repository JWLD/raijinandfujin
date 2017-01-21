using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnTriggers : MonoBehaviour {

    public GameObject trigger;

	// Use this for initialization
    void Awake()
    {
        trigger.SetActive(true);

    }
	void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
