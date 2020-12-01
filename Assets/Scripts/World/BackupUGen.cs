using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupUGen : MonoBehaviour
{
    [Header("Quadrant Spacing")]
    [SerializeField]
    int sectorDistance = 5000;
   
    [SerializeField]
    int worldSize = 10;

    [Header("Quadrant Spacing Multiplier")]
    [SerializeField]
    [Range(1, 5)]
    int quadrantSpacing = 1;

    [Header("Starting Gap")]
    [SerializeField]
    [Range(1, 5)]
    int startSpace = 1;

    [SerializeField]
    [Range(0f, 0.9f)]
    float spaceDensity = 0.5f;

    private ValidObjects valid_objects;
    private int rand;
    [HideInInspector]
    public int furthestGen = 0;
    [HideInInspector]
    public bool generated = false;

    // Start is called before the first frame update
    void Start()
    {
        valid_objects = GameObject.FindGameObjectWithTag("valid_generation_objects").GetComponent<ValidObjects>();
        GenerateSystem(worldSize);
    }

    // Binary Matrix Generator
    //int[,] BMatrix(int size)
    //{
    //    int[,] matrix = new int[size,size];
    //    for(int i = 0; i < size; i++)
    //    {
    //        for(int j = 0; j < size; j++)
    //        {
    //            int newVal;
    //            if (spaceDensity < Random.value) {
    //                newVal = 0;
    //            }
    //            else {
    //                newVal = 1;
    //                if(Mathf.Max(i, j) + (size / 2) > furthestGen)
    //                {
    //                    Debug.Log(furthestGen);
    //                    furthestGen = Mathf.Max(i, j) + (size/2);
    //                }
    //            }
    //            matrix[i, j] = newVal;
                
    //        }
    //    }
    //    return matrix;

    //}

    // Clean Up Matrix
    //int [,] CleanMatrix(int[,] bmatrix, int size)
    //{
    //    for (int i = 0; i < size; i++)
    //    {
    //        for (int j = 0; j < size; j++)
    //        {
    //            if (bmatrix[i,j] == 1)
    //            {
    //                // Check neighbors
    //                bmatrix[i,j] = CheckNeighbors(bmatrix, i, j, size);
    //            }
    //        }
    //    }
    //    return bmatrix;
    //}

    // Check Neighbors
    int CheckNeighbors(int[,] bmatrix, int i, int j, int size)
    {

       
        int newVal = 1;
        for(int x = -1; x < 2; x++)
        {
            for(int y = -1; y < 1; y++)
            {
                if(i+x<0 || i + x >= size || j + y < 0 || j + y >= size)
                {
                    continue;
                }
                else if((x == 0 && y == 0))
                {
                    continue;
                }
                else if((Mathf.Abs(x + i - Mathf.RoundToInt(worldSize/2)) < startSpace && Mathf.Abs(y + j - Mathf.RoundToInt(worldSize / 2)) < startSpace))
                {
                    newVal = 0;
                    break;
                }
                if (bmatrix[i+x,j+y] == 1)
                {
                    newVal = 0;
                    break;
                }
            }
        }
        return newVal;
    }

    // Generate System
    void GenerateSystem(int size)
    {
        //----------------------------------------- Imported Functions
        int[,] bmatrix = new int[size,size];
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                if ((i == Mathf.RoundToInt(worldSize / 2)) && (j == Mathf.RoundToInt(worldSize / 2)))
                {
                    continue;
                }
                int newVal;
                if (spaceDensity < Random.value)
                {
                    newVal = 0;
                }
                else
                {
                    newVal = 1;
                    if (Mathf.Max(i, j)> furthestGen)
                    {
                        Debug.Log(furthestGen);
                        Debug.Log("i:" + i + "  j:" + j);
                        furthestGen = Mathf.Max(i, j);
                    }
                }
                bmatrix[i, j] = newVal;
            }
        }

        // for (int i = 0; i < size; i++)
        //{
        //    for (int j = 0; j < size; j++)
        //    {
        //        if (bmatrix[i, j] == 1)
        //        {
        //            // Check neighbors
        //            bmatrix[i, j] = (CheckNeighbors(bmatrix, i, j, size));
        //        }
        //    }
        //}

        //----------------------------------------------------End of Imported Functions
        // int[,] cleanmat = CleanMatrix(bmatrix, worldSize);
        int x;
        int y;

        Vector2 pos = new Vector2(0, 0);

        for (int i = 0; i < size; i++)
        {
            x = i - (size / 2);
            for (int j = 0; j < size; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }
                y = j - (size / 2);
                if (bmatrix[i, j] == 1)
                {
                    pos.Set(transform.position.x + (x * sectorDistance * quadrantSpacing), transform.position.y + (y * sectorDistance * quadrantSpacing));
                    rand = Random.Range(0, valid_objects.Objects.Length);
                    Instantiate(valid_objects.Objects[rand], pos, transform.rotation);
                }
            }
        }
        generated = true;
    }
}
