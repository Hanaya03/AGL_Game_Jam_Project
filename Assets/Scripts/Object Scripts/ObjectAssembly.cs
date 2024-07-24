using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAssembly : MonoBehaviour
{
    private bool isBroken = false;
    private bool isAssembled = true;
    // Start is called before the first frame update
    void Start()
    {
        isAssembled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // If the Collider2D component is enabled on the collided object
        if (coll.gameObject.layer ==  LayerMask.NameToLayer("Ground") && !isAssembled)
        {
            isAssembled = true;
            Debug.Log("Valid Collision!");
            coll.gameObject.GetComponent<DragObject>().enabled = false;
            coll.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            coll.gameObject.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
        }
    }
}
