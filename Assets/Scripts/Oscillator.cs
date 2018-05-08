using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector = new Vector3(10f,10f,10f);
    [SerializeField] float period = 2f;

    [Range(0,1)][SerializeField] float movementFactor; // 0 for not moved, 1 for fully moved

    Vector3 startingPos;

	// Use this for initialization
	void Start ()
    {
        startingPos = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (period <= Mathf.Epsilon) { return; } // Protect against Period is 0 aka NaN
        float cycles = Time.time / period; // Grows continually from 0

        const float tau = Mathf.PI * 2; // About 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); // goes between -1 and +1

        movementFactor = rawSinWave / 2f + 0.5f; // without the + 0.5f, it goes between -0.5 and 0.5

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
	}
}
