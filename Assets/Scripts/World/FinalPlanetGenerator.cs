using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPlanetGenerator : MonoBehaviour
{
    // Object we are treating as the center of the universe

    [Header("Quadrant Spacing")]
    [SerializeField]
    int sectorDistance = 5000;

    [SerializeField]
    int worldSize = 10;

    [Header("Quadrant Spacing Multiplier")]
    [SerializeField]
    [Range(1, 5)]
    int quadrantSpacing = 1;

    private ValidObjects valid_objects;
    private int rand;

    // Start is called before the first frame update
    void Start()
    {
        valid_objects = GameObject.FindGameObjectWithTag("valid_final_objects").GetComponent<ValidObjects>();
        GenerateSystem(worldSize);
    }



    // Generate System
    void GenerateSystem(int size)
    {
        Vector2 pos = new Vector2(0, 0);
        int x = Random.Range(-1, 2);
        x *= (size+1);

        int y = Random.Range(-1, 2);
        y *= (size+1);

        if(x==0 && y == 0)
        {
            x = (size + 1);
        }

        pos.Set(transform.position.x + (x * sectorDistance * quadrantSpacing), transform.position.y + (y * sectorDistance * quadrantSpacing));
        rand = Random.Range(0, valid_objects.Objects.Length);
        GameObject Final = Instantiate(valid_objects.Objects[rand], pos, transform.rotation);
        Final.tag = "final_object";
        Final.name = "Final Object";
        Final.layer = 9;
    }

}
