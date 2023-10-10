using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbEnableSelector : MonoBehaviour
{
    public bool All;
    private bool _all;

    [Space(10)]

    public GameObject[] Index = new GameObject[4];
    public bool IndexMeta = true;
    public bool IndexA = true;
    public bool IndexB = true;
    public bool IndexC = true;
    private bool[] IndexBools;

    [Space(10)]
    public GameObject[] Middle = new GameObject[4];
    public bool MiddleMeta;
    public bool MiddleA;
    public bool MiddleB;
    public bool MiddleC;
    private bool[] MiddleBools;

    [Space(10)]
    public GameObject[] Ring = new GameObject[4];
    public bool RingMeta;
    public bool RingA;
    public bool RingB;
    public bool RingC;
    private bool[] RingBools;

    [Space(10)]
    public GameObject[] Pinky = new GameObject[4];
    public bool PinkyMeta;
    public bool PinkyA;
    public bool PinkyB;
    public bool PinkyC;
    private bool[] PinkyBools;

    [Space(10)]
    public GameObject[] Thumb = new GameObject[3];
    public bool ThumbMeta;
    public bool ThumbA;
    public bool ThumbB;
    private bool[] ThumbBools;

    void OnValidate()
    {

        CheckUpdateAll();
        UpdateBoolArrays();
        UpdateCollisionDetectors();


    }

    private void CheckUpdateAll()
    {
        if (_all != All)
        {
            _all = All;
            ToggleAll(All);
        }
    }

    private void UpdateCollisionDetectors()
    {
        UpdateFinger(Index, IndexBools);
        UpdateFinger(Middle, MiddleBools);
        UpdateFinger(Ring, RingBools);
        UpdateFinger(Pinky, PinkyBools);
        UpdateFinger(Thumb, ThumbBools);
    }

    private void UpdateFinger(GameObject[] limbs, bool[] limbToggle)
    {
        for (int i = 0; i < limbs.Length; i++)
        {
            limbs[i].GetComponent<CollisionDetectorChild>().isEnabled = limbToggle[i];
        }
    }

    private void ToggleAll(bool isEnable)
    {
        ToggleFinger(isEnable, ref IndexMeta, ref IndexA, ref IndexB, ref IndexC);
        ToggleFinger(isEnable, ref MiddleMeta, ref MiddleA, ref MiddleB, ref MiddleC);
        ToggleFinger(isEnable, ref RingMeta, ref RingA, ref RingB, ref RingC);
        ToggleFinger(isEnable, ref PinkyMeta, ref PinkyA, ref PinkyB, ref PinkyC);
        ToggleFinger(isEnable, ref ThumbMeta, ref ThumbA, ref ThumbB);
    }

    private void ToggleFinger(bool isEnable, ref bool meta, ref bool a, ref bool b, ref bool c)
    {
        meta = isEnable;
        a = isEnable;
        b = isEnable;
        c = isEnable;
    }

    private void ToggleFinger(bool isEnable, ref bool meta, ref bool a, ref bool b)
    {
        meta = isEnable;
        a = isEnable;
        b = isEnable;
    }

    void UpdateBoolArrays()
    {
        IndexBools = new bool[] { IndexMeta, IndexA, IndexB, IndexC };
        MiddleBools = new bool[] { MiddleMeta, MiddleA, MiddleB, MiddleC };
        RingBools = new bool[] { RingMeta, RingA, RingB, RingC };
        PinkyBools = new bool[] { PinkyMeta, PinkyA, PinkyB, PinkyC };
        ThumbBools = new bool[] { ThumbMeta, ThumbA, ThumbB };
    }
}
