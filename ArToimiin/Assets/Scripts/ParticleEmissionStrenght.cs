using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmissionStrenght : MonoBehaviour
{
    public ParticleSystem ps;
    CatchScript catchScript;
    public float strenght;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        catchScript = FindObjectOfType<CatchScript>();
    }

    // Update is called once per frame
    void Update()
    {
        var em = ps.emission;
        em.enabled = true;

        em.rateOverTime = strenght * catchScript.progress;
    }
}
