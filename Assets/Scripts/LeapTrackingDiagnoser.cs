using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapTrackingDiagnoser : MonoBehaviour
{
    public GameObject HandSender;
    private bool _handSenderIsActive = true;
    public GameObject HandReceiver;
    private bool _handReceiverIsActive = true;

    public CollisionDetector collisionDetector;

    void LateUpdate()
    {
        if (_handSenderIsActive != HandSender.activeSelf)
        {
            _handSenderIsActive = HandSender.activeSelf;

            if (HandSender.activeSelf == false)
            {
                Disconnected();
            }

        }

        if (_handReceiverIsActive != HandReceiver.activeSelf)
        {
            _handReceiverIsActive = HandReceiver.activeSelf;

            if (HandReceiver.activeSelf == false)
            {
                Disconnected();
            }
        }

    }

    private void Disconnected()
    {
        Debug.Log("Hand Disconnected");
        collisionDetector.DeleteAllContactPoints();
    }
}
