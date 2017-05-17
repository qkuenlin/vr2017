using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Windows.Kinect;

public class KinectSpellControl : MonoBehaviour
{
    public KinectHandControl RightHand;
    public KinectHandControl LeftHand;
    public Camera Head;

    public FireSpell fs;
    public ThunderSpell ts;
    public ShiedlSpell ss;

    private ThunderBall tb;

    private Vector3[] HandPreviousPos;

    private float[] HandCoolDown;

    private enum HandUse { IDLE, FIRE, LIGHTNING, SHIELD, SWORD, REST };

    private HandUse[] handUse;

    // Use this for initialization
    void Start()
    {
        HandPreviousPos = new Vector3[2];

        handUse = new HandUse[] { HandUse.IDLE, HandUse.IDLE };
        HandCoolDown = new float[] { 0, 0 };
    }

    // Update is called once per frame
    void Update()
    {

        /* RIGHT HAND MANAGER */
        HandManager(0);

        /* LEFT HAND MANAGER */
        HandManager(1);
    }

    void HandManager(int w_h)
    {
        KinectHandControl Hand;

        if (w_h == 0)
            Hand = RightHand;
        else
            Hand = LeftHand;

        Vector3 newPos = Hand.transform.position;

        Vector3 headPos = Head.transform.position;

        switch (handUse[w_h])
        {
            case HandUse.REST:
                {
                    CoolDownManager(w_h);
                    break;
                }
            case HandUse.FIRE:
                {
                    FireBallManager(Hand.state, newPos, w_h);
                    break;
                }
            case HandUse.LIGHTNING:
                {
                    LightningManager(Hand.state, newPos, w_h, headPos);
                    break;
                }
            case HandUse.SHIELD:
                {
                    ShieldManager(Hand.state, newPos, w_h, headPos);
                    break;
                }
            case HandUse.SWORD:
                {
                    break;
                }
            case HandUse.IDLE:
                {
                    if (Hand.state.Equals(HandState.Closed) && newPos.y > headPos.y && handUse[1 - w_h] != HandUse.LIGHTNING)
                    {
                        handUse[w_h] = HandUse.LIGHTNING;
                        tb = ts.newThunderBall();
                    }
                    else if (Hand.state.Equals(HandState.Open) && newPos.z + 0.4 > headPos.z && handUse[1 - w_h] != HandUse.SHIELD) // can only have one shield at a time
                    {
                        handUse[w_h] = HandUse.SHIELD;
                        ss.ActivateShield();
                    }
                    else if (Hand.state.Equals(HandState.Closed))
                    {

                        handUse[w_h] = HandUse.FIRE;
                    }

                    break;
                }
        }
        HandPreviousPos[w_h] = newPos;
    }

    void CoolDownManager(int w_h)
    {
        HandCoolDown[w_h] = Mathf.Max(HandCoolDown[w_h] - Time.deltaTime, 0f);
        if (HandCoolDown[w_h] <= 0)
        {
            handUse[w_h] = HandUse.IDLE;
        }
    }

    void LightningManager(HandState hs, Vector3 pos, int w_h, Vector3 headPos)
    {
        if (hs.Equals(HandState.Closed) && pos.y > headPos.y)
        {
            Vector3 target = new Vector3(pos.x, 0, 15f + 5 * pos.z);
            tb.Charge(target);
        }
        else
        {
            tb.Release();
            handUse[w_h] = HandUse.REST;
            HandCoolDown[w_h] = 2f;
            Destroy(tb);
        }
    }

    void FireBallManager(HandState hs, Vector3 pos, int w_h)
    {
        if (hs.Equals(HandState.Open) && pos.z > HandPreviousPos[w_h].z)
        {
            float diffX = pos.x - HandPreviousPos[w_h].x;
            float diffY = pos.y - HandPreviousPos[w_h].y;
            float diffZ = Mathf.Abs(-pos.z + HandPreviousPos[w_h].z);
            Vector3 direction = new Vector3(diffX, diffY, diffZ).normalized;
            float speed = 20 * (diffX * diffX + diffY * diffY + diffZ * diffZ);
            if (speed < 1.0f / Time.deltaTime) speed = 1.0f / Time.deltaTime;

            Debug.Log(pos + " " + HandPreviousPos[w_h]);

            fs.Throw(pos, direction, speed);
            handUse[w_h] = HandUse.REST;
            HandCoolDown[w_h] = 0.5f;
        }
    }

    void ShieldManager(HandState hs, Vector3 pos, int w_h, Vector3 spinePos)
    {
        if (!hs.Equals(HandState.Open) || pos.z + 0.3 < spinePos.z)
        {
            ss.DesactivateSheidl();
            handUse[w_h] = HandUse.REST;
            HandCoolDown[w_h] = 0.5f;
        }
    }
}
