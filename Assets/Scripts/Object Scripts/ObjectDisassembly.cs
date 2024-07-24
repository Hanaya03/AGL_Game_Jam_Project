using System;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectDisassembly : MonoBehaviour
{

    [SerializeField] private int width;
    [SerializeField] private int length;
    [SerializeField] private GameObject unitSquare;
    [SerializeField] private GameObject placeholder;
    [SerializeField] private GameObject[] unitArray;
    [SerializeField] private GameObject[] pieceArray;

    private enum Shape
    {
        Box = 0,
        Cross = 1,

        Bridge = 2,
        Tee = 3,
        Boot = 4,

        Boat = 5,
        Shoe = 6,
        Raft = 7,

        Platform = 8,
        Block = 9
    }
    
    // Start is called before the first frame update
    void Start()
    {
        width = Mathf.RoundToInt(gameObject.transform.localScale.x);
        length = Mathf.RoundToInt(gameObject.transform.localScale.y);
        Disassemble();
        Squarizer();
    }

    private void ShapeChooserThrees(int index1, int index2, int index3)
    {
        Shape chosenShape = Shape.Box;
        //var randomNum = Random.Range(0, 4);
        //chosenShape = (Shape)randomNum; 
        GameObject[] blockArray =
        {
            unitArray[index1], unitArray[index1 + 1], unitArray[index1 + 2],
            unitArray[index2], unitArray[index2 + 1], unitArray[index2 + 2],
            unitArray[index3], unitArray[index3 + 1], unitArray[index3 + 2]
        };
        Reassemble(chosenShape, blockArray);
    }

    private void Reassemble(Shape chosenShape, GameObject[] blockArray)
    {
        if (chosenShape == Shape.Box)
        {
            var box = Instantiate(placeholder);
            box.name = new string("Object Piece");
            var middleBlock = blockArray[4];
            box.transform.position = middleBlock.transform.position;
            box.GetComponent<BoxCollider2D>().size = new Vector2(3, 3);
            for (int i = 0; i < 9; i++)
            {
                if (i != 4)
                {
                    blockArray[i].transform.SetParent(box.transform);
                    blockArray[i].transform.localScale = new Vector2(1, 1);
                }
                else
                {
                    var block = Instantiate(placeholder);
                    block.name = new string("Object Piece");
                    block.transform.position = middleBlock.transform.position;
                    middleBlock.transform.SetParent(block.transform);
                    middleBlock.transform.localScale = new Vector2(1, 1);
                }
            }
        }
    }

    private void Squarizer()
    {
        if (width % 3 == 0 && length % 3 == 0)
        {
            var index1 = 0;
            var index2 = width;
            var index3 = 2 * width;
            var indexV = 0;
            ShapeChooserThrees(index1, index2, index3);
            while (indexV < length)
            {
                while (index1 + 3 < width + width * indexV)
                {
                    index1 += 3; index2 += 3; index3 += 3;
                    ShapeChooserThrees(index1, index2, index3);
                }

                indexV += 3;
                index1 += 2 * width;
                index2 += 2 * width;
                index3 += 2 * width;
            }
            
            
        }
        else if ((width % 3 == 0 && length > 2) || (length % 3 == 0 && width > 2))
        {
            
        }
        else
        {
            
        }
    }
    
    private void Disassemble()
    {
        var positioningArray = new Vector2[width , length];
        float positionX, startingX, positionY;
        startingX = (float)(-(width / 2) + 0.5);
        positionY = (float)(length / 2 - 0.5);
        positionX = startingX;
        // ReSharper disable once PossibleLossOfFraction
        
        for (int j = 0; j < length; j++)
        {
            for (int i = 0; i < width; i++)
            {
                positioningArray[i, j] = new Vector2(positionX, positionY);
                positionX++;
            }

            positionX = (float)startingX;
            positionY--;
        }

        unitArray = new GameObject[width * length];
        int counter = 0;
        for (int j = 0; j < length; j++)
        {
            for (int i = 0; i < width; i++)
            {
                var position = positioningArray[i, j];
                unitArray[counter] = Instantiate(unitSquare, position, Quaternion.Euler(0, 0, 0), gameObject.transform);
                var newScaleX = ((float)(1) / width);
                var newScaleY = ((float)(1) / length);
                unitArray[counter].name = new string("block " + counter);
                unitArray[counter++].transform.localScale = new Vector2(newScaleX, newScaleY);
            }
        }
    }
    
    // HEADER : Give Methods
}
