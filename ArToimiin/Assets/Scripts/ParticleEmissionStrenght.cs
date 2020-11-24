using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmissionStrenght : MonoBehaviour
{
    public ParticleSystem ps;
    public float strenght;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var em = ps.emission;
        em.enabled = true;

        em.rateOverTime = 1 * strenght;
    }
}
