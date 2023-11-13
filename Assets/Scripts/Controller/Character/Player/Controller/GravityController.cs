using System.Collections;
using System.Collections.Generic;
using Architecture;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    private Rigidbody mRb;

    private void Awake ()
    {
        mRb = GetComponent<Rigidbody>();
        mRb.useGravity = false;
    }

    private void FixedUpdate ()
    {
        var gravity = MemoirOfWarAsset.Gravity * MemoirOfWarAsset.GravityScale * Vector3.up;
        mRb.AddForce(gravity, ForceMode.Acceleration);
    }
}
