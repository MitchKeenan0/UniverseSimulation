using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InitialSpawn : MonoBehaviour {


    public float size = 1000.0f;
	public int population = 1000;
	public GameObject[] matter;
	public bool bRandomSize = true;

    Camera cam;
    public float camMoveSpeed = 100.0f;
    

	void Start ()
    {
		cam = FindObjectOfType<Camera>();

		// distribute matter throughout size's area
		for (int i = 0; i < population; i++)
		{
            Vector3 SpawnPos;
            SpawnPos.x = Random.Range(size, -size);
            SpawnPos.y = Random.Range(size, -size);
            SpawnPos.z = Random.Range(size, -size);
            int rando = Random.Range(0, matter.Length);
            GameObject newMatter = Instantiate(matter[rando], SpawnPos, Quaternion.identity);

            if (bRandomSize)
            {
                float randomScalar = Random.Range(1.0f, 3.0f);
                newMatter.transform.localScale *= randomScalar;
            }

            // Create 2% 'tracer' matter
            if (Random.Range(1, 100) >= 98)
            {
                newMatter.AddComponent<TrailRenderer>();
                newMatter.GetComponent<TrailRenderer>().time = 20.0f;
                newMatter.GetComponent<TrailRenderer>().endWidth = 0.0f;
            }
		}

	}

	void Update()
	{
        // zoom control
		float zoomControl = Input.GetAxis("Mouse ScrollWheel");
		if (zoomControl > 0)
        {
            cam.fieldOfView -= 10;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 1.0f, 100.0f);
        }
		else if (zoomControl < 0)
        {
            cam.fieldOfView += 10;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 1.0f, 170.0f);
        }


        // turn control
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        bool moving = Input.GetButton("Fire3");

        if (y > 0 || y < 0)
        {
            if (moving)
                cam.transform.Translate(cam.transform.forward * y * camMoveSpeed);
            else
                cam.transform.Rotate(Vector3.right, y * -1);
        }

        if (x > 0 || x < 0)
        {
            if (moving)
                cam.transform.Translate(cam.transform.right * x * camMoveSpeed);
            else
                cam.transform.Rotate(Vector3.up, x);
        }
    }
}
