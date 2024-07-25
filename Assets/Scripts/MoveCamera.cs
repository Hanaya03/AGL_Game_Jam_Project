using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform camPos;
    private float timeToInc;
    private Vector3 camPosTarget;
    private Vector3 camPosIncrement = new Vector3(1, 0, 0);
    private Vector3 currentVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        timeToInc = Time.time + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
            timeToInc = Time.time + 1;
            camPosTarget = camPos.position + camPosIncrement;
            camPos.position = Vector3.SmoothDamp(camPos.position, camPosTarget, ref currentVelocity, .5f);

    }
}
