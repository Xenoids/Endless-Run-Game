using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveController : MonoBehaviour
{
    [Header("Position")]
    public Transform p;
    public float hOffset;

    private void Update()
    {
        // masukin posisi awal ke newPos
        Vector3 newPos = transform.position;

        // jumlahkan dan masukin ke pos baru
        newPos.x = p.position.x + hOffset;

        // update posnya
        transform.position = newPos;
    }
}
