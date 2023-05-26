using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float minX, maxX;

    private Transform playerTarget;
    private Vector3 tempPos;

    void Start()
    {
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
    }
    void LateUpdate()
    {

        if (!playerTarget)
            return;

        tempPos = transform.position;
        tempPos.x = playerTarget.position.x;

        if (tempPos.x < minX)
            tempPos.x = minX;

        if (tempPos.x > maxX)
            tempPos.x = maxX;

        transform.position = tempPos;

    }
}
