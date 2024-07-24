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
    private int counter;
    
    private enum Shape
    {
        Box = 0,
        Cross = 1,

        Bucket = 2,
        Tee = 3,
        Boot = 4,

        Boat = 5,
        TopHat = 6,
        Raft = 7,
        Shoe = 8,

        Platform = 9,
        Block = 10
    }
    
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        width = Mathf.RoundToInt(gameObject.transform.localScale.x);
        length = Mathf.RoundToInt(gameObject.transform.localScale.y);
        Disassemble();
        Squarizer();
    }

    void Update()
    {
    }
    
    private void ShapeChooserThrees(int index1, int index2, int index3)
    {
        Shape chosenShape;
        int randomNum = Random.Range(0, 7);
        chosenShape = (Shape) randomNum; 
        GameObject[] blockArray =
        {
            unitArray[index1], unitArray[index1 + 1], unitArray[index1 + 2],
            unitArray[index2], unitArray[index2 + 1], unitArray[index2 + 2],
            unitArray[index3], unitArray[index3 + 1], unitArray[index3 + 2]
        };
        Reassemble(chosenShape, blockArray);
    }

    private void ShapeChooserTwos(int index1, int index2, bool horizontal, int cutoff = -1)
    {
        Shape chosenShape;
        GameObject[] blockArray;
        if (cutoff == -1)
        {
            cutoff = width;
        }
        if (horizontal)
        {
            while (index2 < ((length - 1) * width) + cutoff)
            {
                if (index2 + 2 < ((length - 1) * width) + cutoff)
                {
                    chosenShape = (Shape) Random.Range(5,10);
                    if ((int)chosenShape < 8)
                    {
                        blockArray = new[]
                        {
                            unitArray[index1], unitArray[index1 + 1], unitArray[index1 + 2],
                            unitArray[index2], unitArray[index2 + 1], unitArray[index2 + 2],
                        };
                        index1 += 3;
                        index2 += 3;
                    }
                    else
                    {
                        blockArray = new[]
                        {
                            unitArray[index1], unitArray[index1 + 1],
                            unitArray[index2], unitArray[index2 + 1],
                        };
                        index1 += 2;
                        index2 += 2;
                    }
                    ReassembleX2(chosenShape, blockArray);
                }
                if (index1 + 1 < ((length - 1) * width))
                {
                    chosenShape = (Shape) Random.Range(8,10);
                    blockArray = new[]
                    {
                        unitArray[index1], unitArray[index1 + 1],
                        unitArray[index2], unitArray[index2 + 1]
                    };
                    index1 += 2;
                    index2 += 2;
                    ReassembleX2(chosenShape, blockArray);
                }
                else
                {
                    chosenShape = (Shape) Random.Range(8,10);
                    blockArray = new[]
                    {
                        unitArray[index1],
                        unitArray[index2],
                    };
                    Reassemble(chosenShape, blockArray);
                    index1 ++;
                    index2 ++;
                    break;
                }
            }
        }
        else
        {
            while (index2 < (length * width))
            {
                if (index2 + (2 * width) < (length * width))
                {
                    chosenShape = (Shape) Random.Range(5,10);
                    if ((int)chosenShape < 8)
                    {
                        blockArray = new[]
                        {
                            unitArray[index1], unitArray[index1 + width], unitArray[index1 + 2 * width],
                            unitArray[index2], unitArray[index2 + width], unitArray[index2 + 2 * width],
                        };
                        index1 += 3;
                        index2 += 3;
                    }
                    else
                    {
                        blockArray = new[]
                        {
                            unitArray[index1], unitArray[index1 + width],
                            unitArray[index2], unitArray[index2 + width],
                        };
                        index1 += 2;
                        index2 += 2;
                    }
                    ReassembleX2(chosenShape, blockArray);
                }
                else if (index2 + width < (length * width))
                {
                    chosenShape = (Shape) Random.Range(8,10);
                    blockArray = new[]
                    {
                        unitArray[index1], unitArray[index1 + width],
                        unitArray[index2], unitArray[index2 + width]
                    };
                    index1 += 2;
                    index2 += 2;
                    ReassembleX2(chosenShape, blockArray);
                }
                else
                {
                    chosenShape = (Shape) Random.Range(8,10);
                    blockArray = new[]
                    {
                        unitArray[index1],
                        unitArray[index2],
                    };
                    Reassemble(chosenShape, blockArray);
                    index1 ++;
                    index2 ++;
                    break;
                }
            }
        }
    }

    private void ShaperChooserOnes(int index, bool horizontal, int cutoff = -1)
    {
        Shape chosenShape;
        if (cutoff == -1)
        {
            cutoff = width;
        }
        if (horizontal)
        {
            while (index < ((length - 1) * width) + cutoff)
            {
                if (index + 2 < ((length - 1) * width) + cutoff)
                {
                    int randomNum = Random.Range(8, 11);
                    switch (randomNum)
                    {
                        case 8:
                            Reassemble((Shape)randomNum, new []{unitArray[index], unitArray[index + 1], unitArray[index + 2]});
                            index += 3;
                            break;
                        case 9:
                            Reassemble((Shape)randomNum, new []{unitArray[index], unitArray[index + 1]});
                            index += 2;
                            break;
                        case 10:
                            Reassemble((Shape)randomNum, new []{unitArray[index]});
                            index ++;
                            break;
                    }
                }
                if (index + 1 < ((length - 1) * width) + cutoff)
                {
                    int randomNum = Random.Range(9, 11);
                    switch (randomNum)
                    {
                        case 9:
                            Reassemble((Shape)randomNum, new []{unitArray[index], unitArray[index + 1]});
                            index += 2;
                            break;
                        case 10:
                            Reassemble((Shape)randomNum, new []{unitArray[index]});
                            index ++;
                            break;
                    }
                }
                else
                {
                    Reassemble(Shape.Block, new []{unitArray[index]});
                    index++;
                    break;
                }
            }
        }
        else
        {
            while (index < (length * width))
            {
                if (index + (2 * width) < (length * width))
                {
                    int randomNum = Random.Range(8, 11);
                    switch (randomNum)
                    {
                        case 8:
                            Reassemble((Shape)randomNum, new []{unitArray[index], unitArray[index + width], unitArray[index + 2 * width]});
                            index += 3 * width;
                            break;
                        case 9:
                            Reassemble((Shape)randomNum, new []{unitArray[index], unitArray[index + width]});
                            index += 2 * width;
                            break;
                        case 10:
                            Reassemble((Shape)randomNum, new []{unitArray[index]});
                            index += width;
                            break;
                    }
                }
                else if (index + width < (length * width))
                {
                    int randomNum = Random.Range(9, 11);
                    switch (randomNum)
                    {
                        case 9:
                            Reassemble((Shape)randomNum, new []{unitArray[index], unitArray[index + width]});
                            index += 2 * width;
                            break;
                        case 10:
                            Reassemble((Shape)randomNum, new []{unitArray[index]});
                            index += width;
                            break;
                    }
                }
                else
                {
                    Reassemble(Shape.Block, new []{unitArray[index]});
                    index += width;
                    break;
                }
            }
        }
    }

    private void Reassemble(Shape chosenShape, GameObject[] blockArray)
    {
        switch (chosenShape)
        {
            case Shape.Box:
            {
                var box = Instantiate(placeholder);
                box.name = new string("Object Piece");
                var middleBlock = blockArray[4];
                box.transform.position = middleBlock.transform.position;
                for (int i = 0; i < 9; i++)
                {
                    if (i != 4)
                    {
                        blockArray[i].transform.SetParent(box.transform);
                        blockArray[i].transform.localScale = new Vector2(1, 1);
                        blockArray[i].GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    else
                    {
                        var block = Instantiate(placeholder);
                        block.name = new string("Object Piece");
                        block.transform.position = middleBlock.transform.position;
                        middleBlock.transform.SetParent(block.transform);
                        middleBlock.transform.localScale = new Vector2(1, 1);
                        middleBlock.GetComponent<SpriteRenderer>().color = Color.blue;
                    }
                }

                break;
            }
            case Shape.Cross:
            {
                var cross = Instantiate(placeholder);
                cross.name = new string("Object Piece");
                cross.transform.position = blockArray[4].transform.position;
                for (var i = 0; i < 9; i++)
                {
                    if (i != 0 && i != 2 && i != 6 && i != 8)
                    {
                        blockArray[i].transform.SetParent(cross.transform);
                        blockArray[i].transform.localScale = new Vector2(1, 1);
                        blockArray[i].GetComponent<SpriteRenderer>().color = Color.yellow;
                    }
                    else
                    {
                        var block = Instantiate(placeholder);
                        block.name = new string("Object Piece");
                        block.transform.position = blockArray[i].transform.position;
                        blockArray[i].transform.SetParent(block.transform);
                        blockArray[i].transform.localScale = new Vector2(1, 1);
                        blockArray[i].GetComponent<SpriteRenderer>().color = Color.green;
                    }
                }

                break;
            }
            case Shape.Bucket:
            {
                var shape = Instantiate(placeholder);
                shape.name = new string("Object Piece");
                shape.transform.position = blockArray[4].transform.position;
                
                var block = Instantiate(placeholder);
                block.name = new string("Object Piece");
                block.transform.position = blockArray[4].transform.position;
                
                for (var i = 0; i < 9; i++)
                {
                    if (i != 1 && i != 4)
                    {
                        blockArray[i].transform.SetParent(shape.transform);
                        blockArray[i].transform.localScale = new Vector2(1, 1);
                        blockArray[i].GetComponent<SpriteRenderer>().color = Color.black;
                    }
                    else
                    {
                        blockArray[i].transform.SetParent(block.transform);
                        blockArray[i].transform.localScale = new Vector2(1, 1);
                        blockArray[i].GetComponent<SpriteRenderer>().color = Color.gray;
                    }
                }

                break;
            }
            case Shape.Tee:
            {
                var shape = Instantiate(placeholder);
                shape.name = new string("Object Piece");
                shape.transform.position = blockArray[4].transform.position;
                
                var block1 = Instantiate(placeholder);
                block1.name = new string("Object Piece");
                block1.transform.position = blockArray[6].transform.position;
                
                var block2 = Instantiate(placeholder);
                block2.name = new string("Object Piece");
                block2.transform.position = blockArray[8].transform.position;
                
                for (var i = 0; i < 9; i++)
                {
                    if (i != 3 && i != 5 && i != 6 && i != 8)
                    {
                        blockArray[i].transform.SetParent(shape.transform);
                        blockArray[i].transform.localScale = new Vector2(1, 1);
                    }
                    else if (i == 3 || i == 6)
                    {
                        blockArray[i].transform.SetParent(block1.transform);
                        blockArray[i].transform.localScale = new Vector2(1, 1);
                        blockArray[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                    }
                    else
                    {
                        blockArray[i].transform.SetParent(block2.transform);
                        blockArray[i].transform.localScale = new Vector2(1, 1);
                        blockArray[i].GetComponent<SpriteRenderer>().color = Color.magenta;
                    }
                }

                break;
            }
            case Shape.Boot:
            {
                var shape = Instantiate(placeholder);
                shape.name = new string("Object Piece");
                shape.transform.position = blockArray[4].transform.position;

                var block = Instantiate(placeholder);
                block.name = new string("Object Piece");
                block.transform.position = blockArray[5].transform.position;

                for (var i = 0; i < 9; i++)
                {
                    if (i != 2 && i != 5 && i != 1 && i != 4)
                    {
                        blockArray[i].transform.SetParent(shape.transform);
                        blockArray[i].transform.localScale = new Vector2(1, 1);
                        blockArray[i].GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    else 
                    {
                        blockArray[i].transform.SetParent(block.transform);
                        blockArray[i].transform.localScale = new Vector2(1, 1);
                        blockArray[i].GetComponent<SpriteRenderer>().color = Color.yellow;
                    }
                }
                break;
            }
            case Shape.Boat:
            {
                var shape = Instantiate(placeholder);
                shape.name = new string("Object Piece");
                shape.transform.position = blockArray[4].transform.position;
                int num = Random.Range(1, 3);
                GameObject block1 = null;
                GameObject block2 = null;
                GameObject block3 = null;
                switch (num)
                {
                    case 1:
                        block1 = Instantiate(placeholder);
                        shape.name = new string("Object Piece");
                        shape.transform.position = blockArray[1].transform.position;
                        
                        block2 = Instantiate(placeholder);
                        shape.name = new string("Object Piece");
                        shape.transform.position = blockArray[4].transform.position;

                        break;
                    case 2 :
                        block1 = Instantiate(placeholder);
                        shape.name = new string("Object Piece");
                        shape.transform.position = blockArray[4].transform.position;
                        
                        block2 = Instantiate(placeholder);
                        shape.name = new string("Object Piece");
                        shape.transform.position = blockArray[0].transform.position;
                        
                        block3 = Instantiate(placeholder);
                        shape.name = new string("Object Piece");
                        shape.transform.position = blockArray[2].transform.position;

                        break;
                }

                for (var i = 0; i < 9; i++)
                {
                    if (i != 0 && i != 1 && i != 2 && i != 4)
                    {
                        blockArray[i].transform.SetParent(shape.transform);
                        blockArray[i].transform.localScale = new Vector2(1, 1);
                        blockArray[i].GetComponent<SpriteRenderer>().color = Color.blue;
                    }
                    else 
                    {
                        if (num == 1)
                        {
                            if (i == 0 || i == 1 || i == 2)
                            {
                                blockArray[i].transform.SetParent(block1.transform);
                            }
                            else
                            {
                                blockArray[i].transform.SetParent(block2.transform);
                            }
                            blockArray[i].transform.localScale = new Vector2(1, 1); 
                            blockArray[i].GetComponent<SpriteRenderer>().color = Color.green;
                        }
                        else if (num == 2)
                        {
                            if (i == 1 || i == 4)
                            {
                                blockArray[i].transform.SetParent(block1.transform);
                            }
                            else if (i == 0) 
                            {
                                blockArray[i].transform.SetParent(block2.transform);
                            }
                            else
                            {
                                blockArray[i].transform.SetParent(block3.transform);
                            }
                            blockArray[i].transform.localScale = new Vector2(1, 1); 
                            blockArray[i].GetComponent<SpriteRenderer>().color = Color.magenta;
                        }
                    }
                }
                break;
            }
            case Shape.TopHat:
            {
                var shape = Instantiate(placeholder);
                shape.name = new string("Object Piece");
                shape.transform.position = blockArray[7].transform.position;
                int num = Random.Range(1, 3);
                GameObject block1 = null;
                GameObject block2 = null;
                GameObject block3 = null;
                switch (num)
                {
                    case 1:
                        block1 = Instantiate(placeholder);
                        shape.name = new string("Object Piece");
                        shape.transform.position = blockArray[1].transform.position;
                        
                        block2 = Instantiate(placeholder);
                        shape.name = new string("Object Piece");
                        shape.transform.position = blockArray[3].transform.position;
                        
                        block3 = Instantiate(placeholder);
                        shape.name = new string("Object Piece");
                        shape.transform.position = blockArray[5].transform.position;

                        break;
                    case 2 :
                        block1 = Instantiate(placeholder);
                        shape.name = new string("Object Piece");
                        shape.transform.position = blockArray[1].transform.position;
                        
                        block2 = Instantiate(placeholder);
                        shape.name = new string("Object Piece");
                        shape.transform.position = blockArray[3].transform.position;
                        
                        block3 = Instantiate(placeholder);
                        shape.name = new string("Object Piece");
                        shape.transform.position = blockArray[5].transform.position;

                        break;
                }

                for (var i = 0; i < 9; i++)
                {
                    if (i != 0 && i != 1 && i != 2 && i != 3 && i != 5)
                    {
                        blockArray[i].transform.SetParent(shape.transform);
                        blockArray[i].transform.localScale = new Vector2(1, 1);
                        blockArray[i].GetComponent<SpriteRenderer>().color = Color.gray;
                    }
                    else 
                    {
                        if (num == 1)
                        {
                            if (i == 0 || i == 1 || i == 2)
                            {
                                blockArray[i].transform.SetParent(block1.transform);
                            }
                            else if (i == 3)
                            {
                                blockArray[i].transform.SetParent(block2.transform);
                            }
                            else
                            {
                                blockArray[i].transform.SetParent(block3.transform);
                            }
                            blockArray[i].transform.localScale = new Vector2(1, 1); 
                            blockArray[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                        }
                        else if (num == 2)
                        {
                            if (i == 0 || i == 3)
                            {
                                blockArray[i].transform.SetParent(block1.transform);
                            }
                            else if (i == 2 || i == 5) 
                            {
                                blockArray[i].transform.SetParent(block2.transform);
                            }
                            else
                            {
                                blockArray[i].transform.SetParent(block3.transform);
                            }
                            blockArray[i].transform.localScale = new Vector2(1, 1); 
                            blockArray[i].GetComponent<SpriteRenderer>().color = Color.black;
                        }
                    }
                }
                break;
            }   
            case Shape.Raft:
            {
                var shape = Instantiate(placeholder);
                shape.name = new string("Object Piece");
                var middleBlock = blockArray[1];
                shape.transform.position = middleBlock.transform.position;
                for (int i = 0; i < 3; i++)
                {
                    blockArray[i].transform.SetParent(shape.transform);
                    blockArray[i].transform.localScale = new Vector2(1, 1);
                    blockArray[i].GetComponent<SpriteRenderer>().color = Color.red;
                }
                break;
            }    
            case Shape.Platform:
            {
                var shape = Instantiate(placeholder);
                shape.name = new string("Object Piece");
                var middleBlock = blockArray[0];
                shape.transform.position = middleBlock.transform.position;
                for (int i = 0; i < 2; i++)
                {
                    blockArray[i].transform.SetParent(shape.transform);
                    blockArray[i].transform.localScale = new Vector2(1, 1);
                    blockArray[i].GetComponent<SpriteRenderer>().color = Color.blue;
                }
                break;
            }
            case Shape.Block:
            {
                var shape = Instantiate(placeholder);
                shape.name = new string("Object Piece");
                var middleBlock = blockArray[0];
                shape.transform.position = middleBlock.transform.position;
                blockArray[0].transform.SetParent(shape.transform);
                blockArray[0].transform.localScale = new Vector2(1, 1);
                blockArray[0].GetComponent<SpriteRenderer>().color = Color.green;
                if (blockArray.Length > 1)
                {
                    var block1 = Instantiate(placeholder);
                    block1.name = new string("Object Piece");
                    block1.transform.position = blockArray[1].transform.position;
                    blockArray[1].transform.SetParent(shape.transform);
                    blockArray[1].transform.localScale = new Vector2(1, 1);
                    blockArray[1].GetComponent<SpriteRenderer>().color = Color.green;
                }
                break;
            }
        }
    }

    private void ReassembleX2(Shape chosenShape, GameObject[] blockArray)
    {
        GameObject shape;
        GameObject block1 = null;
        GameObject block2 = null;
        GameObject block3 = null;
        switch (chosenShape)
        {
            case Shape.TopHat:
                shape = Instantiate(placeholder);
                shape.name = new string("Object Piece");
                shape.transform.position = blockArray[4].transform.position;
                block1 = null;
                block2 = null;
                block3 = null;
                block1 = Instantiate(placeholder);
                block1.name = new string("Object Piece");
                block1.transform.position = blockArray[1].transform.position;
                        
                block2 = Instantiate(placeholder);
                block2.name = new string("Object Piece");
                block2.transform.position = blockArray[3].transform.position;
                
                for (var i = 0; i < 6; i++)
                {
                    if (i != 0 && i != 2)
                    {
                        blockArray[i].transform.SetParent(shape.transform);
                        blockArray[i].transform.localScale = new Vector2(1, 1);
                        blockArray[i].GetComponent<SpriteRenderer>().color = Color.gray;
                    }
                    else 
                    {
                        if (i == 0)
                        {
                            blockArray[i].transform.SetParent(block1.transform);
                            blockArray[i].transform.localScale = new Vector2(1, 1); 
                            blockArray[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                        }

                        else if (i == 2)
                        {
                            blockArray[i].transform.SetParent(block2.transform);
                            blockArray[i].transform.localScale = new Vector2(1, 1); 
                            blockArray[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                        }
                    }
                }
                break;
            case Shape.Boat:
                shape = Instantiate(placeholder);
                shape.name = new string("Object Piece");
                shape.transform.position = blockArray[4].transform.position;
                GameObject block = null;
                block = Instantiate(placeholder);
                block.name = new string("Object Piece");
                block.transform.position = blockArray[1].transform.position;
                
                for (var i = 0; i < 6; i++)
                {
                    if (i != 1)
                    {
                        blockArray[i].transform.SetParent(shape.transform);
                        blockArray[i].transform.localScale = new Vector2(1, 1);
                        blockArray[i].GetComponent<SpriteRenderer>().color = Color.gray;
                    }
                    else 
                    {
                        blockArray[i].transform.SetParent(block.transform);
                        blockArray[i].transform.localScale = new Vector2(1, 1); 
                        blockArray[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                    }
                }
                break;
            case Shape.Raft:
                shape = Instantiate(placeholder);
                shape.name = new string("Object Piece");
                shape.transform.position = blockArray[4].transform.position;
                block1 = null;
                block2 = null;
                block1 = Instantiate(placeholder);
                block1.name = new string("Object Piece");
                block1.transform.position = blockArray[0].transform.position;
                block2 = Instantiate(placeholder);
                block2.name = new string("Object Piece");
                block2.transform.position = blockArray[0].transform.position;
                
                for (var i = 0; i < 6; i++)
                {
                    if (i != 0 || i != 1 || i != 2)
                    {
                        blockArray[i].transform.SetParent(shape.transform);
                        blockArray[i].transform.localScale = new Vector2(1, 1);
                        blockArray[i].GetComponent<SpriteRenderer>().color = Color.gray;
                    }
                    else 
                    {
                        if (i == 0)
                        {
                            blockArray[i].transform.SetParent(block1.transform);
                            blockArray[i].transform.localScale = new Vector2(1, 1); 
                            blockArray[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                        }
                        else
                        {
                            blockArray[i].transform.SetParent(block2.transform);
                            blockArray[i].transform.localScale = new Vector2(1, 1); 
                            blockArray[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                        }
                        
                    }
                }
                break;
            case Shape.Shoe:
                shape = Instantiate(placeholder);
                shape.name = new string("Object Piece");
                shape.transform.position = blockArray[2].transform.position;
                block1 = null;
                block1 = Instantiate(placeholder);
                block1.name = new string("Object Piece");
                block1.transform.position = blockArray[0].transform.position;
                
                for (var i = 0; i < 4; i++)
                {
                    if (i != 1)
                    {
                        blockArray[i].transform.SetParent(shape.transform);
                        blockArray[i].transform.localScale = new Vector2(1, 1);
                        blockArray[i].GetComponent<SpriteRenderer>().color = Color.gray;
                    }
                    else 
                    {
                        blockArray[i].transform.SetParent(block1.transform);
                        blockArray[i].transform.localScale = new Vector2(1, 1); 
                        blockArray[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                    }
                }
                break;
            case Shape.Platform:
                shape = Instantiate(placeholder);
                shape.name = new string("Object Piece");
                shape.transform.position = blockArray[2].transform.position;

                var num = Random.Range(0, 2);

                if (num == 0)
                {
                    block1 = null;
                    block1 = Instantiate(placeholder);
                    block1.name = new string("Object Piece");
                    block1.transform.position = blockArray[0].transform.position;
                    block2 = null;
                    block2 = Instantiate(placeholder);
                    block2.name = new string("Object Piece");
                    block2.transform.position = blockArray[1].transform.position;  
                }
                else
                {
                    block1 = null;
                    block1 = Instantiate(placeholder);
                    block1.name = new string("Object Piece");
                    block1.transform.position = blockArray[3].transform.position;
                }
                
                for (var i = 0; i < 4; i++)
                {
                    if (num == 0)
                    {
                        if (i != 0 || i != 1)
                        {
                            blockArray[i].transform.SetParent(shape.transform);
                            blockArray[i].transform.localScale = new Vector2(1, 1);
                            blockArray[i].GetComponent<SpriteRenderer>().color = Color.gray;
                        }
                        else 
                        {
                            if (i == 0)
                            {
                                blockArray[i].transform.SetParent(block1.transform);
                            }
                            else
                            {
                                blockArray[i].transform.SetParent(block2.transform);
                            }
                            blockArray[i].transform.localScale = new Vector2(1, 1); 
                            blockArray[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                        }
                    }

                    else
                    {
                        if (i != 1 || i != 3)
                        {
                            blockArray[i].transform.SetParent(shape.transform);
                            blockArray[i].transform.localScale = new Vector2(1, 1);
                            blockArray[i].GetComponent<SpriteRenderer>().color = Color.gray;
                        }
                        else 
                        {
                            blockArray[i].transform.SetParent(block1.transform);
                            blockArray[i].transform.localScale = new Vector2(1, 1); 
                            blockArray[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                        }
                    }
                }
                break;
            
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
                while (index1 + 5 < width + width * indexV)
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
            if (length % 3 == 0)
            {
                 var index1 = 0;
                 var index2 = width;
                 var index3 = 2 * width;
                 var indexV = 0;
                 ShapeChooserThrees(index1, index2, index3);
                 while (indexV + 2 < length)
                 {
                     while (index1 + 5 < width + width * indexV)
                     {
                         index1 += 3; index2 += 3; index3 += 3;
                         ShapeChooserThrees(index1, index2, index3);
                     }
     
                     indexV += 3;
                     index1 = indexV * width - 3;
                     index2 = index1 + width;
                     index3 = index2 + width;
                 }

                 if (width % 3 == 1)
                 {
                     ShaperChooserOnes(width - 1, false);
                 }
                 else if (width % 3 == 2)
                 {
                     ShapeChooserTwos(width - 2, width - 1, false);
                 }
            }
            if (width % 3 == 0)
            {
                var index1 = 0;
                var index2 = width;
                var index3 = 2 * width;
                var indexV = 0;
                ShapeChooserThrees(index1, index2, index3);
                while (indexV + 2 < length)
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

                if (length % 3 == 1)
                {
                    ShaperChooserOnes(width * (length - 1), true);
                }
                else if (width % 3 == 2)
                {
                    ShapeChooserTwos(width * (length - 2), width * (length - 1), true);
                }
            }
        }
        else if (width > 3 & length > 3)
        {
            var index1 = 0;
            var index2 = width;
            var index3 = 2 * width;
            var indexV = 0;
            ShapeChooserThrees(index1, index2, index3);
            while (indexV + 2 < length)
            {
                while (index1 + 5 < width - 1 + width * indexV)
                {
                    index1 += 3; index2 += 3; index3 += 3;
                    ShapeChooserThrees(index1, index2, index3);
                }

                indexV += 3;
                index1 = indexV * width - 3;
                index2 = index1 + width;
                index3 = index2 + width;
            }

            int cutoff = width;
            if (width % 3 == 1)
            {
                ShaperChooserOnes(width - 1, false);
                cutoff = width - 1;
            }
            else if (width % 3 == 2)
            {
                ShapeChooserTwos(width - 2, width - 1, false);
                cutoff = width - 2;
            }
            if (length % 3 == 1)
            {
                ShaperChooserOnes(width * (length - 1), true, cutoff);
            }
            else if (length % 3 == 2)
            {
                ShapeChooserTwos(width * (length - 2), width * (length - 1), true, cutoff);
            }
        }
        gameObject.SetActive(false);
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
