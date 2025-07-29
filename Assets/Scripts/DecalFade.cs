using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalFade : MonoBehaviour
{
    public float seconds = 1.0f;
    private DecalProjector proj;
    private Material projMat;

    // Start is called before the first frame update
    void Start()
    {
        proj = transform.GetComponent<DecalProjector>();
        projMat = new Material(proj.material);
        projMat.SetFloat("_Alpha", 1);
        transform.GetComponent<DecalProjector>().material = projMat;
        StartCoroutine(Fade());
    }

    // Update is called once per frame
    IEnumerator Fade()
    {
        float initialAlpha = projMat.GetFloat("_Alpha");

        float elapsedTime = 0f;

        while (elapsedTime < seconds)
        {
            elapsedTime += Time.deltaTime;
            projMat.SetFloat("_Alpha", Mathf.Lerp(1, 0, elapsedTime / seconds));
            yield return null;
        }

        Destroy(gameObject);

        yield return null;
    }
}
