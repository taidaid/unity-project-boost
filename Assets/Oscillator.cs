using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 obstacleMovementVector;

    // todo remove from inspector later
    [Range(0,1)] [SerializeField] float movementFactor; // 0 for not moved and 1 for fully moved

    private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = obstacleMovementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
