using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupFPGen : MonoBehaviour
{
    // Object we are treating as the center of the universe

    [Header("Quadrant Spacing")]
    [SerializeField]
    int sectorDistance = 5000;

    [Header("Quadrant Spacing Multiplier")]
    [SerializeField]
    [Range(1, 5)]
    int quadrantSpacing = 1;

    private ValidObjects valid_objects;
    private int rand;
    private int furthestGen = 0;

    // Start is called before the first frame update

    void Update()
    {
        if (GetComponent<universe_generator>().generated) {
            valid_objects = GameObject.FindGameObjectWithTag("valid_final_objects").GetComponent<ValidObjects>();
            furthestGen = GetComponent<universe_generator>().furthestGen;
            GenerateSystem();
        }
    }



    // Generate System
    void GenerateSystem()
    {
        Vector2 pos = new Vector2(0, 0);
        int x = Random.Range(-1, 2);
        x *= (furthestGen+1)/2;

        int y = Random.Range(-1, 2);
        y *= (furthestGen + 1) /2;

        if(x==0 && y == 0)
        {
            x = (furthestGen + 1) /2;
        }

        pos.Set(transform.position.x + (x * sectorDistance * quadrantSpacing), transform.position.y + (y * sectorDistance * quadrantSpacing));
        rand = Random.Range(0, valid_objects.Objects.Length);
        GameObject Final = Instantiate(valid_objects.Objects[rand], pos, transform.rotation);
        Final.tag = "final_object";
        Final.name = "Final Object";
        Final.layer = 9;
        GetComponent<FinalPlanetGenerator>().enabled = false;
    }

}
