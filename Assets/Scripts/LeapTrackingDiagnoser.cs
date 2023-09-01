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

        if (_handReceiverIsActive != HandSender.activeSelf)
        {
            _handReceiverIsActive = HandSender.activeSelf;

            if (HandSender.activeSelf == false)
            {
                Disconnected();
            }
        }

    }

    private void Disconnected()
    {
        collisionDetector.DeleteAllContactPoints();
    }
}
