using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunaSpawnScript : MonoBehaviour
{
    public GameObject spawnattavaUkko;
    GroundScan groundScan;
    CatchScript catchScript;

    void Start()
    {
        groundScan = FindObjectOfType<GroundScan>();
        catchScript = FindObjectOfType<CatchScript>();
        spawnattavaUkko = groundScan.prefabCharacter;
    }

    private void OnMouseDown()
    {
        GameObject spawnUkko = Instantiate(spawnattavaUkko, transform.position, transform.rotation);
        catchScript.hahmo = spawnUkko;
        Destroy(gameObject);
    }
}
