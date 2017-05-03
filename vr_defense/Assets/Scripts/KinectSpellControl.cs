using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Windows.Kinect;

public class KinectSpellControl : MonoBehaviour {

    private KinectSensor _sensor;
    private BodyFrameReader _bodyFrameReader;
    private Body[] _bodies = null;

    public GameObject kinectAvailableText;
    public Text handXText;

    public bool IsAvailable;

    public static KinectSpellControl instance = null;


	// Use this for initialization
	void Start () {
        _sensor = KinectSensor.GetDefault();

        if(_sensor != null)
        {
            IsAvailable = _sensor.IsAvailable;

            kinectAvailableText.SetActive(IsAvailable);
            Debug.Log(IsAvailable);

            _bodyFrameReader = _sensor.BodyFrameSource.OpenReader();

            if (!_sensor.IsOpen)
            {
                _sensor.Open();
            }

            _bodies = new Body[_sensor.BodyFrameSource.BodyCount];
            Debug.Log(_sensor.BodyFrameSource.BodyCount);
        }
	}
	
	// Update is called once per frame
	void Update () {

        IsAvailable = _sensor.IsAvailable;
        
        if (_bodyFrameReader != null)
        {
            var frame = _bodyFrameReader.AcquireLatestFrame();

            Debug.Log(frame.BodyCount);

            if(frame != null)
            {
                frame.GetAndRefreshBodyData(_bodies);

                foreach(var body in _bodies.Where(b => b.IsTracked))
                {
                    IsAvailable = true;

                    Debug.Log(body.TrackingId);

                    if (body.HandRightState == HandState.Closed)
                    {
                        Debug.Log("FireBall!");
                    }
                }

                frame.Dispose();
                frame = null;
            }
        }
	}

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public Body[] GetBodies()
    {
        return _bodies;
    }

    private void OnApplicationQuit()
    {
        if(_bodyFrameReader != null)
        {
            _bodyFrameReader.IsPaused = true;
            _bodyFrameReader.Dispose();
            _bodyFrameReader = null;
        }

        if(_sensor != null)
        {
            if (_sensor.IsOpen)
            {
                _sensor.Close();
            }
            _sensor = null;
        }
    }
}
