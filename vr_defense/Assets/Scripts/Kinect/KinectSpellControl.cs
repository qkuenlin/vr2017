using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Windows.Kinect;

public class KinectSpellControl : MonoBehaviour
{

    public BodySourceManager bsm;

    [Range(0.00001f, 1)]
    public float expSmoothWeight = 0.1f;

    public Vector3 offset = Vector3.zero;

    public FireSpell fs;
    public ThunderSpell ts;
    
    private Vector3[] HandPreviousPos;

    private bool[] HandFireBall;
    private bool[] HandLightning;
    private bool[] HandShield;

    // Use this for initialization
    void Start()
    {
        HandFireBall = new bool[]{false, false};
        HandLightning = new bool[] { false, false };
        HandShield = new bool[] { false, false };
        HandPreviousPos = new Vector3[2];
    }

    // Update is called once per frame
    void Update()
    {
        Body[] data = bsm.GetData();

        if (data == null)
        {
            Debug.Log("KinectModelControllerV2.cs: no capture data");
            return;
        }

        int bodyNbr = 0;
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i].IsTracked)
            {
                bodyNbr = i;
                break;
            }
        }

        /* RIGHT HAND MANAGER */
        HandManager(data[bodyNbr].HandRightState, data[bodyNbr].Joints[JointType.HandRight], 0, data[bodyNbr]);

        /* LEFT HAND MANAGER */
        HandManager(data[bodyNbr].HandLeftState, data[bodyNbr].Joints[JointType.HandLeft], 1, data[bodyNbr]);
    }

    void HandManager(HandState hs, Windows.Kinect.Joint hand, int w_h, Body data)
    {
        CameraSpacePoint pos = hand.Position;
        Vector3 newPos = new Vector3(pos.X, pos.Y, pos.Z);
        newPos += offset;
        if (hand.TrackingState == TrackingState.Tracked)
            newPos = Vector3.Lerp(HandPreviousPos[w_h], newPos, expSmoothWeight * Time.deltaTime * 60);
        else
            newPos = Vector3.Lerp(HandPreviousPos[w_h], newPos, (expSmoothWeight * Time.deltaTime * 60 * 0.2f));


        if (HandLightning[w_h]) LightningManager(hs, newPos, w_h);
        else if (HandFireBall[w_h]) FireBallManager(hs, newPos, w_h);
        else if (HandShield[w_h]) ShieldManager(hs, newPos, w_h);



        if(hs.Equals(HandState.Closed) && hand.Position.Z > data.Joints[JointType.Head].Position.Z)
        {
            HandLightning[w_h] = true;
        }
        else if (hs.Equals(HandState.Closed))
        {
            HandFireBall[w_h] = true;
        }

        HandPreviousPos[w_h] = newPos;
    }

    void LightningManager(HandState hs, Vector3 pos, int w_h)
    {
        if (hs.Equals(HandState.Closed))
        {
            ts.thunderBall.Charge(pos);
        }
        else
        {
            ts.thunderBall.Release();
        }
    }

    void FireBallManager(HandState hs, Vector3 pos, int w_h)
    {
        if (hs.Equals(HandState.Open))
        {
            float diffX = pos.x - HandPreviousPos[w_h].x;
            float diffY = pos.y - HandPreviousPos[w_h].y;
            float diffZ = -pos.z + HandPreviousPos[w_h].z;
            Vector3 direction = new Vector3(diffX, diffY, diffZ).normalized;
            float speed = 20 * (diffX * diffX + diffY * diffY + diffZ * diffZ);
            if (speed < 1.0f / Time.deltaTime) speed = 1.0f / Time.deltaTime;

            fs.Throw(pos, direction, speed);
            HandFireBall[w_h] = false;
        }
    }

    void ShieldManager(HandState hs, Vector3 pos, int w_h)
    {

    }
}
