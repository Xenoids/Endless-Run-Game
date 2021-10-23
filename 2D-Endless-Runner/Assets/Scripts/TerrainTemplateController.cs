using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTemplateController : MonoBehaviour
{

    private const float debugLineH = 10.0f;

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position + Vector3.up * debugLineH / 2, transform.position + Vector3.down * debugLineH / 2, Color.green);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
