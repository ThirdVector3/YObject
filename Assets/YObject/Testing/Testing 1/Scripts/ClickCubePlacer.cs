using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCubePlacer : MonoBehaviour
{
    [SerializeField] private GameObject blueCube;
    [SerializeField] private GameObject redCube;
    [SerializeField] private Transform parent;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            var cube = Instantiate(redCube, parent);
            BeatCube beatCube = cube.GetComponent<BeatCube>();
            beatCube.rotation = -1;
            beatCube.time = Time.time;
            cube.transform.position += Vector3.right * Random.Range(-0.5f, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            var cube = Instantiate(redCube, parent);
            BeatCube beatCube = cube.GetComponent<BeatCube>();
            beatCube.rotation = 0;
            beatCube.time = Time.time;
            cube.transform.position += Vector3.up * Random.Range(-0.5f, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            var cube = Instantiate(redCube, parent);
            BeatCube beatCube = cube.GetComponent<BeatCube>();
            beatCube.rotation = 1;
            beatCube.time = Time.time;
            cube.transform.position += Vector3.right * Random.Range(-0.5f, 0.5f);
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var cube = Instantiate(blueCube, parent);
            BeatCube beatCube = cube.GetComponent<BeatCube>();
            beatCube.rotation = -1;
            beatCube.time = Time.time;
            cube.transform.position += Vector3.right * Random.Range(-0.5f, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            var cube = Instantiate(blueCube, parent);
            BeatCube beatCube = cube.GetComponent<BeatCube>();
            beatCube.rotation = 0;
            beatCube.time = Time.time;
            cube.transform.position += Vector3.up * Random.Range(-0.5f, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            var cube = Instantiate(blueCube, parent);
            BeatCube beatCube = cube.GetComponent<BeatCube>();
            beatCube.rotation = 1;
            beatCube.time = Time.time;
            cube.transform.position += Vector3.right * Random.Range(-0.5f, 0.5f);
        }
    }
}
