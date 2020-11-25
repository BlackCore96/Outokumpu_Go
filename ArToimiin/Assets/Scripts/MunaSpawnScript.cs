using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunaSpawnScript : MonoBehaviour
{
    public GameObject spawnattavaUkko;
    AnimatorScript animatorScript;
    GroundScan groundScan;
    CatchScript catchScript;
    bool shrink;

    void Start()
    {
        groundScan = FindObjectOfType<GroundScan>();
        catchScript = FindObjectOfType<CatchScript>();
        spawnattavaUkko = groundScan.prefabCharacter;
        animatorScript = FindObjectOfType<AnimatorScript>();
    }

    private void Update()
    {
        if (shrink)
        {
            transform.localScale = transform.localScale - (Vector3.one * Time.deltaTime);
            if (transform.localScale.x <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnMouseDown()
    {
        GameObject spawnUkko = Instantiate(spawnattavaUkko, transform.position, transform.rotation);
        catchScript.hahmo = spawnUkko;
        animatorScript.dustTrailParticle = spawnUkko.gameObject.transform.Find("RunParticle").GetComponent<ParticleSystem>();
        animatorScript.catchParticle = spawnUkko.gameObject.transform.Find("CatchParticle").GetComponent<ParticleSystem>();
        GetComponent<Animator>().SetTrigger("animate");
        Invoke("Destroy", 1);
    }

    void Destroy()
    {
        shrink = true;
    }
}
