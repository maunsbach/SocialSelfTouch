using System.Collections;
using UnityEngine;

public class VisualizeGizmoRecording : MonoBehaviour
{
    public ColliderConnector ColliderConnector;
    public SensationCSVImport SensationCSVImport;
    public CollisionDetector CollisionDetector;
    public PointPipe PointPipe;

    private float _sampleRate;
    int _timeStamp = 0;
    int idx;

    public bool PassToHaptics;

    private DataSphereID[] iDs;

    void Awake()
    {
        _sampleRate = SamplingRate.Value;    
    }

    void Start()
    {
        iDs = SensationCSVImport.sphereIDs;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("space key was pressed");
            IEnumerator coroutine = PlayBack();
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator PlayBack()
    {
        while (_timeStamp == iDs[idx].TimeStamp)
        {

            GameObject GO = ColliderConnector.GetColliderGameObject(iDs[idx].HandLimb, iDs[idx].HandJoint, iDs[idx].Row, iDs[idx].Column);
            //CollisionDetector.AddContactPoint(GO.GetInstanceID(), GO.transform.position, GO.GetComponent<SphereID>());
            GO.GetComponent<DebugSphere>().ShowSphere(SamplingRate.Value);

            if (PassToHaptics == true)
            {
                PointPipe.AddContactPoint(GO.transform.position);
            }

            idx++;

            if (idx >= iDs.Length)
            {
                break;
            }
        }

        yield return new WaitForSeconds(_sampleRate);
        _timeStamp++;

        if (idx < iDs.Length)
        {
            yield return StartCoroutine(PlayBack());
        }
        else
        {
            idx = 0;
            _timeStamp = 0;
        }
    }
}
