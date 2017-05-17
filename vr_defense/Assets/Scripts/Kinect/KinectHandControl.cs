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
            Windows.Kinect.Joint wrist;

            if (rightHand)
            {
                hand = data[bodyNbr].Joints[JointType.HandRight];
                wrist = data[bodyNbr].Joints[JointType.HandRight-1];
            }
            else
            {
                hand = data[bodyNbr].Joints[JointType.HandLeft];
                wrist = data[bodyNbr].Joints[JointType.HandLeft-1];
            }

            CameraSpacePoint pos = hand.Position;
            Vector3 newPos = new Vector3(pos.X, pos.Y, pos.Z);
            if (mirrorX)
                newPos.x = -newPos.x;
            if (mirrorZ)
                newPos.z = -newPos.z;

            newPos += offset;

            Vector3 target = GetVector3FromJoint(hand) - GetVector3FromJoint(wrist);

            target = transform.TransformDirection(target);

            Vector3 dir = new Vector3(0f, 0f, 0f);
            Quaternion quat = Quaternion.FromToRotation(dir, target);

            if (hand.TrackingState == TrackingState.Tracked)
            {
                newPos = Vector3.Lerp(transformToSet.localPosition, newPos, expSmoothWeight * Time.deltaTime * 60);
                quat = Quaternion.Lerp(transformToSet.localRotation, quat, expSmoothWeight * 60 * Time.deltaTime);
            }
            else
            {
                newPos = Vector3.Lerp(transformToSet.localPosition, newPos, (expSmoothWeight * Time.deltaTime * 60 * 0.2f));
                quat = Quaternion.Lerp(transformToSet.localRotation, quat, (expSmoothWeight * 0.2f * 60 * Time.deltaTime));
            }

            transformToSet.localPosition = newPos;
            transformToSet.localRotation = quat; 

        }
    }


    private static Vector3 GetVector3FromJoint(Windows.Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Z * 10, joint.Position.Y * 10);
    }


}
