using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ObjectGridSnap : MonoBehaviour
{
    void Update()
    {
        // Quick n dirty snapping for testing purposes
        var position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        position.z = Mathf.Round(position.z);
        transform.position = position;
    }
}
