using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class KinectHandControl : MonoBehaviour
{

    public int player;
    [Range(0.00001f, 1)]
    public float expSmoothWeight = 0.1f;

    public BodySourceManager bsm;

    public bool updatePosition = true;
    public bool mirrorX = false;
    public bool mirrorZ = false;

    public Vector3 offset = Vector3.zero;

    public bool rightHand = true;

    public Transform transformToSet;

    public HandState state;

    public Vector3 HipPosition; 

    // Update is called once per frame
    void Update()
    {
        Body[] data = bsm.GetData();
        if (data == null)
        {
            //Debug.Log("KinectModelControllerV2.cs: no capture data");
            return;
        }
        // select a valid kinect body (for some reason the body ID seems to be randomly attributed by kinect)
        // player = 0 selects the first valid body
        // player = 1 selects the second valid body (and so on)
        int bodyNbr = 0;
        int count = 0;
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i].IsTracked)
            {
                if (count == player)
                {
                    bodyNbr = i;
                    break;
                }
                else
                    count++;
            }
        }

        // set model position
        if (updatePosition)
        {
            Windows.Kinect.Joint hand;
            Windows.Kinect.Joint hip;

            if (rightHand)
            {
                hand = data[bodyNbr].Joints[JointType.HandRight];
                hip = data[bodyNbr].Joints[JointType.HipLeft];
            }
            else
            {
                hand = data[bodyNbr].Joints[JointType.HandLeft];
                hip = data[bodyNbr].Joints[JointType.HipRight];
            }

            CameraSpacePoint pos = hand.Position;
            Vector3 newPos = new Vector3(pos.X, pos.Y, pos.Z);


            CameraSpacePoint hippos = hand.Position;
            Vector3 hipnewPos = new Vector3(hippos.X, hippos.Y, hippos.Z);


            if (mirrorX)
            {
                newPos.x = -newPos.x;
                hipnewPos.x = -hipnewPos.x;
            }
            if (mirrorZ)
            {
                newPos.z = -newPos.z;
                hipnewPos.z = -hipnewPos.z;
            }

            newPos += offset;

            if (hand.TrackingState == TrackingState.Tracked)
            {
                newPos = Vector3.Lerp(transformToSet.localPosition, newPos, expSmoothWeight * Time.deltaTime * 60);
                hipnewPos = Vector3.Lerp(HipPosition, hipnewPos, expSmoothWeight * Time.deltaTime * 60);

            }
            else
            {
                newPos = Vector3.Lerp(transformToSet.localPosition, newPos, (expSmoothWeight * Time.deltaTime * 60 * 0.2f));
                hipnewPos = Vector3.Lerp(HipPosition, hipnewPos, expSmoothWeight * Time.deltaTime * 60);

            }

            transformToSet.localPosition = newPos;

            HipPosition = hipnewPos;


            if (rightHand)
                state = data[bodyNbr].HandRightState;
            else
                state = data[bodyNbr].HandLeftState;
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        Minion minion = (Minion)col.GetComponent("Minion");
        if (minion != null)
        {
            minion.Hit(1f, "fist");
        }
    }
}
