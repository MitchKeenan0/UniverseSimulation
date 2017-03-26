using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matter : MonoBehaviour {

    public float mass = 1.0f;
	public float range = 1000.0f;
	float dist = 0.0f;
    float g = 0.0f;
	Rigidbody rb;
 	
	void Start ()
    {
		rb = GetComponent<Rigidbody>();
	}

    void Update ()
    {
        Vector3 localPos = transform.position;

        // get array of nearby objects
		Collider[] surroundingObjs = Physics.OverlapSphere(localPos, range);
		if (surroundingObjs.Length > 0)
		{
            // attract to all nearby objects
            foreach (Collider col in surroundingObjs)
			{
				Vector3 colPos = col.transform.position;
                Vector3 toObj = (colPos - localPos).normalized;
                dist = (colPos - localPos).magnitude;
				g = Mathf.Clamp(mass / (dist*dist), 0.001f, range);
				rb.AddForce(toObj * g);
			}
		}
	}
}
