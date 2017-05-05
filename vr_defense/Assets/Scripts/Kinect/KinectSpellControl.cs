using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Windows.Kinect;

public class KinectSpellControl : MonoBehaviour
{

    public BodySourceManager bsm;
    public FireSpell fs;

    private bool rightHandClosed;
    private CameraSpacePoint rightHandPreviousPos;

    private bool leftHandClosed;
    private CameraSpacePoint leftHandPreviousPos;

    // Use this for initialization
    void Start()
    {
        rightHandClosed = false;
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
        
        if(data[bodyNbr].HandRightState.Equals(HandState.Closed))
        {
            rightHandClosed = true;
        }
        if (rightHandClosed && data[bodyNbr].HandRightState.Equals(HandState.Open))
        {
            CameraSpacePoint currentPosition = data[bodyNbr].Joints[JointType.HandRight].Position;
            
            Debug.Log("Throw Fireball!!!!");
            Vector3 position = new Vector3(currentPosition.X, currentPosition.Y+2, 1.0f);
            float diffX = currentPosition.X - rightHandPreviousPos.X;
            float diffY = currentPosition.Y - rightHandPreviousPos.Y;
            float diffZ = -currentPosition.Z + rightHandPreviousPos.Z;
            Vector3 direction = new Vector3(diffX, diffY, diffZ);
            float speed = 15 * (diffX * diffX + diffY * diffY + 2 * diffZ * diffZ);
            if (speed < 1 / Time.deltaTime) speed = 1 / Time.deltaTime;

            Debug.Log(position);
            Debug.Log(speed);

            fs.Throw(position, direction.normalized, speed);
            rightHandClosed = false;
        }

        if (data[bodyNbr].HandLeftState.Equals(HandState.Closed))
        {
            leftHandClosed = true;
        }
        if (leftHandClosed && data[bodyNbr].HandLeftState.Equals(HandState.Open))
        {
            CameraSpacePoint currentPosition = data[bodyNbr].Joints[JointType.HandLeft].Position;

            Debug.Log("Throw Fireball!!!!");
            Vector3 position = new Vector3(currentPosition.X, currentPosition.Y + 2, 1.0f);
            float diffX = currentPosition.X - leftHandPreviousPos.X;
            float diffY = currentPosition.Y - leftHandPreviousPos.Y;
            float diffZ = -currentPosition.Z + leftHandPreviousPos.Z;
            Vector3 direction = new Vector3(diffX, diffY, diffZ);
            float speed = 15 * (diffX * diffX + diffY * diffY + 2 * diffZ * diffZ);
            if (speed < 1 / Time.deltaTime) speed = 1 / Time.deltaTime;

            Debug.Log(position);
            Debug.Log(speed);

            fs.Throw(position, direction.normalized, speed);
            leftHandClosed = false;
        }

        leftHandPreviousPos = data[bodyNbr].Joints[JointType.HandLeft].Position;
        rightHandPreviousPos = data[bodyNbr].Joints[JointType.HandRight].Position;


    }
}
