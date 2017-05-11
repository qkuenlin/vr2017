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
    public ShiedlSpell ss;

    private ThunderBall tb;
    
    private Vector3[] HandPreviousPos;

    private bool[] HandFireBall;
    private bool[] HandLightning;
    private bool[] HandShield;
    private float[] HandCoolDown;

    // Use this for initialization
    void Start()
    {
        HandFireBall = new bool[]{false, false};
        HandLightning = new bool[] { false, false };
        HandShield = new bool[] { false, false };
        HandCoolDown = new float[] { 0f, 0f };
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

        Vector3 headPos = new Vector3(data.Joints[JointType.Head].Position.X, data.Joints[JointType.Head].Position.Y, data.Joints[JointType.Head].Position.Z);
        headPos += offset;

        Vector3 spinePos = new Vector3(data.Joints[JointType.SpineMid].Position.X, data.Joints[JointType.SpineMid].Position.Y, data.Joints[JointType.SpineMid].Position.Z);
        spinePos += offset;
      //  Debug.Log("Hand pos: " + newPos + "    head pos " + headPos );

        if(HandCoolDown[w_h] > 0)
        {
            CoolDownManager(w_h);
        }
        else if (HandLightning[w_h])
        {
            LightningManager(hs, newPos, w_h, headPos);
        }
        else if (HandFireBall[w_h])
        {
            FireBallManager(hs, newPos, w_h);
        }
        else if (HandShield[w_h])
        {
            ShieldManager(hs, newPos, w_h, spinePos);
        }

        /* If there is no spell activated by this hand, look if one should be activated */

        else if(hs.Equals(HandState.Closed) && newPos.y > headPos.y && !HandLightning[1 - w_h])
        {
            HandLightning[w_h] = true;
            tb = ts.newThunderBall();
        }
        else if(hs.Equals(HandState.Open) && newPos.z+0.5 <spinePos.z && !HandShield[1-w_h]) // can only have one shield at a time
        {
            HandShield[w_h] = true;
            ss.ActivateShield();
        }
        else if (hs.Equals(HandState.Closed))
        {
            HandFireBall[w_h] = true;
        }

        HandPreviousPos[w_h] = newPos;
    }

    void CoolDownManager(int w_h)
    {
        HandCoolDown[w_h] = Mathf.Max(HandCoolDown[w_h] - Time.deltaTime, 0f);
    }

    void LightningManager(HandState hs, Vector3 pos, int w_h, Vector3 headPos)
    {
        if (hs.Equals(HandState.Closed) && pos.y > headPos.y)
        {
            Vector3 target = new Vector3(pos.x, 0, 20f - 10*pos.z);
            tb.Charge(target);
        }
        else
        {
            tb.Release();
            HandLightning[w_h] = false;
            HandCoolDown[w_h] = 2f;
            Destroy(tb);
        }
    }

    void FireBallManager(HandState hs, Vector3 pos, int w_h)
    {
        if (hs.Equals(HandState.Open) && pos.z < HandPreviousPos[w_h].z)
        {
            float diffX = pos.x - HandPreviousPos[w_h].x;
            float diffY = pos.y - HandPreviousPos[w_h].y;
            float diffZ = Mathf.Abs(-pos.z + HandPreviousPos[w_h].z);
            Vector3 direction = new Vector3(diffX, diffY, diffZ).normalized;
            float speed = 20 * (diffX * diffX + diffY * diffY + diffZ * diffZ);
            if (speed < 1.0f / Time.deltaTime) speed = 1.0f / Time.deltaTime;

            fs.Throw(pos, direction, speed);
            HandFireBall[w_h] = false;
            HandCoolDown[w_h] = 0.5f;
        }
    }

    void ShieldManager(HandState hs, Vector3 pos, int w_h, Vector3 spinePos)
    {
        if (!hs.Equals(HandState.Open) || pos.z + 0.4 > spinePos.z)
        {
            ss.DesactivateSheidl();
            HandShield[w_h] = false;
            HandCoolDown[w_h] = 0.5f;
        }
    }
}
