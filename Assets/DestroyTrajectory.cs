using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrajectory : MonoBehaviour
{
    [SerializeField]
    public float stopSpeed = 12;

    SpeedTrackerUI speedTrack;
    TrajectoryLine trajectory;
    // Start is called before the first frame update
    void Start()
    {
        speedTrack = FindObjectOfType<SpeedTrackerUI>();
        trajectory = FindObjectOfType<TrajectoryLine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (speedTrack.speed <= stopSpeed)
        {
            GameObject[] trajectoryPoinits = GameObject.FindGameObjectsWithTag("trajectory");
            trajectory.enabled = false;
            if (trajectoryPoinits.Length != 0)
            {
                for (int i = 0; i < trajectoryPoinits.Length; i++)
                {
                    Destroy(trajectoryPoinits[i]);
                }
            }
        }
        else if(!trajectory.enabled)
        {
            trajectory.enabled = true;
        }
    }
}
