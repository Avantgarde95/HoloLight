using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandLight : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        transform.Rotate(Vector3.up, Space.World);
    }
}
