using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunaSpawnScript : MonoBehaviour
{

    public GameObject spawnattavaUkko;
    GroundScan groundScan;

    // Start is called before the first frame update
    void Awake()
    {
        groundScan = FindObjectOfType<GroundScan>();
        spawnattavaUkko = groundScan.prefabCharacter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Instantiate(spawnattavaUkko, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
