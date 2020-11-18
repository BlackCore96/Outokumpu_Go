using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunaSpawnScript : MonoBehaviour
{
    public GameObject spawnattavaUkko;
    AnimatorScript animatorScript;
    GroundScan groundScan;
    CatchScript catchScript;

    void Start()
    {
        groundScan = FindObjectOfType<GroundScan>();
        catchScript = FindObjectOfType<CatchScript>();
        spawnattavaUkko = groundScan.prefabCharacter;
        animatorScript = FindObjectOfType<AnimatorScript>();
    }

    private void OnMouseDown()
    {
        GameObject spawnUkko = Instantiate(spawnattavaUkko, transform.position, transform.rotation);
        catchScript.hahmo = spawnUkko;
        animatorScript.dustTrailParticle = spawnUkko.gameObject.transform.Find("RunParticle").GetComponent<ParticleSystem>();
        animatorScript.catchParticle = spawnUkko.gameObject.transform.Find("CatchParticle").GetComponent<ParticleSystem>();
        Destroy(gameObject);
    }
}
