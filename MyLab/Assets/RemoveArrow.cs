using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveArrow : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "arrow")
        {
            Destroy(collision.gameObject);
        }
    }
}
