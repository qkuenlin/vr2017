using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class KinectHeadTracking : MonoBehaviour
{

    public int player;
    [Range(0.00001f, 1)]
    public float expSmoothWeight = 0.1f;

    public BodySourceManager bsm;

    public bool updatePosition = true;
    public bool mirrorX = false;
    public bool mirrorZ = false;

    public Vector3 offset = Vector3.zero;

    public Transform transformToSet;


    // Update is called once per frame
    void Update()
    {
        Body[] data = bsm.GetData();
        if (data == null)
        {
            Debug.Log("KinectModelControllerV2.cs: no capture data");
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
            CameraSpacePoint pos = data[bodyNbr].Joints[JointType.Head].Position;
            Vector3 newPos = new Vector3(pos.X, pos.Y, pos.Z);
            if (mirrorX)
                newPos.x = -newPos.x;
            if (mirrorZ)
                newPos.z = -newPos.z;

            newPos += offset;

            if (data[bodyNbr].Joints[JointType.Head].TrackingState == TrackingState.Tracked)
                newPos = Vector3.Lerp(transformToSet.localPosition, newPos, expSmoothWeight * Time.deltaTime * 60);
            else
                newPos = Vector3.Lerp(transformToSet.localPosition, newPos, (expSmoothWeight * Time.deltaTime * 60 * 0.2f));

            transformToSet.localPosition = newPos;

        }

    }
}
