using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAssembly : MonoBehaviour
{
    private int pieceCounter;
    LinkedList<Vector3> coordList = new LinkedList<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void increasePieceCounter(){
        pieceCounter++;
        Debug.Log(pieceCounter);
    }

    public void addCoordToList(Vector3 coordToAdd){
        coordList.AddLast(coordToAdd);
        Debug.Log(coordToAdd);
    }
}
