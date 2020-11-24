using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmissionStrenght : MonoBehaviour
{
    public ParticleSystem ps;
    new Camera camera;
    float distance = .65f;
    CatchScript catchScript;
    public float strenght;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        catchScript = FindObjectOfType<CatchScript>();
        camera = Camera.main;
    }

    void Update()
    {
        var em = ps.emission;
        em.enabled = true;
        var shape = ps.shape;

        float distanceToCamera = Vector3.Distance(camera.transform.position, transform.position);

        shape.angle = Mathf.Rad2Deg * Mathf.Atan2(distance, distanceToCamera);
        em.rateOverTime = strenght * catchScript.progress * 10;
    }
}
