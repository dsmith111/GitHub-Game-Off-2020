using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevTools : MonoBehaviour
{
    [HideInInspector]
    public ShipController shipController;
    [SerializeField] float timeScale = 6f;

    // Start is called before the first frame update
    void Start()
    {
        shipController = GetComponent<ShipController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Refills Fuel
        if (Input.GetKeyDown(KeyCode.F))
        {
            shipController.remainingFuel = shipController.maxFuel;
        }

        //Restarts Scene
        if (Input.GetKeyUp(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            shipController.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = timeScale;
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            Time.timeScale = 1f;
        }
    }
}
