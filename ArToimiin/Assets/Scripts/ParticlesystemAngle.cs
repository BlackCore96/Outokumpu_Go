using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesystemAngle : MonoBehaviour
{
    public GameObject target;
    public Transform pSystem;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("ParticleTarget");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToTarget = (target.transform.position - pSystem.position).normalized;
        Quaternion rottation = Quaternion.LookRotation(directionToTarget, Vector3.forward);
        transform.rotation = rottation;
    }
}
