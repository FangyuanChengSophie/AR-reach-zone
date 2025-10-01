using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;


public class FollowGrabAnchor : MonoBehaviour
{
    public Transform grabAnchor;
    public HandGrabInteractable interactable;

    private Vector3 lastPosition;
    private Quaternion lastRotation;

    void Start()
    {
        if (grabAnchor != null)
        {
            lastPosition = grabAnchor.position;
            lastRotation = grabAnchor.rotation;
        }
    }

    void Update()
    {
        if (grabAnchor == null || interactable == null) return;


        if (interactable.SelectingInteractors.Count > 0)
        {
 
            if (grabAnchor.position != lastPosition || grabAnchor.rotation != lastRotation)
            {
                Debug.Log("[GrabAnchor] moved!");
                transform.position = grabAnchor.position;
                Vector3 euler = grabAnchor.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(0, euler.y, 0);

                lastPosition = grabAnchor.position;
                lastRotation = grabAnchor.rotation;
            }
        }
    }
}