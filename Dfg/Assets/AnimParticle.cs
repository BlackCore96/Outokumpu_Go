using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimParticle : MonoBehaviour
{
    public ParticleSystem particle1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        particle1.Play();
    }
}
