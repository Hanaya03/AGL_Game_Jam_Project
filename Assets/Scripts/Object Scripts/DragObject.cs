using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
  private bool dragging = false;
  private Transform transformToMove;
  private Vector3 offset;

  void start(){

  }

  // Update is called once per frame
  void Update() {
    if (dragging) {
      // Move object, taking into account original offset.
      if (transform.parent != null){
        transform.parent.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
      } else {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
      }
    }
  }

  private void OnMouseDown() {
    // Record the difference between the objects centre, and the clicked point on the camera plane.
    if (transform.parent != null){
        offset = transform.parent.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
      } else {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);    
      }
    dragging = true;
    // gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
  }

  private void OnMouseUp() {
    // Stop dragging.
    dragging = false;
    // gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
  }
}
