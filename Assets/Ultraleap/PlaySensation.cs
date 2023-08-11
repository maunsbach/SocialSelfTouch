using System.IO;
using Ultraleap.Haptics;
using UnityEngine;

public class PlaySensation : MonoBehaviour
{
    private Library _library;
    private IDevice _device;
    private SensationEmitter _sensationEmitter;
    private SensationPackage _sensationPackage;
    private Sensation.Instance _instance;

    public bool CreateMock; // False unless set in inspector
    public string SensationPackageName = "StandardSensations.ssp";
    public string SensationName = "CircleWithFixedSpeed";

    void Awake()
    {
        _library = new Library();
        _library.Connect();
        _device = CreateMock ? _library.GetDevice("MockDevice:USX") : _library.FindDevice();
        _sensationEmitter = new SensationEmitter(_library);
        _sensationEmitter.Devices.Add(_device);
        _sensationPackage = SensationPackage.LoadFromFile(_library, Path.Combine(Application.streamingAssetsPath, SensationPackageName));
        _instance = _sensationPackage.GetSensation(SensationName).MakeInstance();
        _sensationEmitter.SetSensation(_instance);
        _sensationEmitter.Start();
    }

    private void OnDestroy()
    {
        _sensationEmitter.Stop();
        _sensationPackage.Dispose();
        _device.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
