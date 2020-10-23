using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UccoMovement : MonoBehaviour
{
    bool moveRight = true;
    public float movementSpeed;
    // Update is called once per frame
    void Update()
    {
        if (moveRight)
        {
            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, Vector3.zero) >= 3)
            {
                moveRight = false;
            }
        }
        else
        {
            transform.Translate(-transform.right * movementSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, Vector3.zero) >= 3)
            {
                moveRight = true;
            }
        }
    }
}
