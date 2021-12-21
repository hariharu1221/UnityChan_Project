using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Vector3 offSetPosition;
    public Vector3 offSetRotation;
    public float speed = 1;

    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject player;
    private Vector3 cameraPosition;

    private void Awake()
    {
        mainCamera = GameObject.Find("Main_Camera");
        player = GameObject.Find("Player");
    }

    private void LateUpdate()
    {
        cameraPosition.x = offSetPosition.x + player.transform.position.x;
        cameraPosition.y = offSetPosition.y + player.transform.position.y;
        cameraPosition.z = offSetPosition.z + player.transform.position.z;
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraPosition, Time.deltaTime * speed);

        Quaternion SetRotation = Quaternion.Euler(offSetRotation);
        mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, SetRotation, Time.deltaTime * speed);
    }
}
