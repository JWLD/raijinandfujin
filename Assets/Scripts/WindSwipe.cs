using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSwipe : MonoBehaviour {

    public Texture Tex1;
    public Texture Tex2;
    public Renderer rend;
    public float rate;

    private bool runningWSF = false;
    private bool runningWEF = false;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!runningWSF)
            StartCoroutine(WindStartFade());
	}

    IEnumerator WindStartFade()
    {
        runningWSF = true;
        rend.material.SetTexture("_DissolveTexture", Tex1);
        float i = 0;
        while (i<1)
        {
            i += Time.deltaTime * rate;

            rend.material.SetFloat("_DissolveAmount", Mathf.Lerp(0, 1, i));
            //if (i > 0.5f && !runningWEF)
            //    StartCoroutine(WindEndFade());
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(WindEndFade());
       // runningWSF = false;
    }

    IEnumerator WindEndFade()
    {
       // runningWEF = true;
        rend.material.SetTexture("_DissolveTexture", Tex2);
        float i = 0;
        while (i < 1)
        {
            i += Time.deltaTime * rate;

            rend.material.SetFloat("_DissolveAmount", Mathf.Lerp(1, 0, i));

            yield return new WaitForEndOfFrame();
        }
        runningWSF = false;
        //  runningWEF = false;
    }
}
