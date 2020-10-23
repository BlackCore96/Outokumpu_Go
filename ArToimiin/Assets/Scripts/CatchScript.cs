using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchScript : MonoBehaviour
{
    public GameObject hahmo;
    public GameObject hahmo2;
    RaycastHit hit;

    public void Button()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            if (hit.collider.gameObject.Equals(hahmo))
            
            {
                Destroy(hit.collider.gameObject);
            }

            if (hit.collider.gameObject.Equals(hahmo2))

            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
