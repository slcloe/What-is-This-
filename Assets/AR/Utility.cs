using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Utility
{
    private static ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    static Utility()
    {
        raycastManager = GameObject.FindObjectOfType<ARRaycastManager>();
    }

    //screenPosition 위치로 광선 발사
    // pose 정보를 hits에 저장
    //충돌가능 물체 == TrackableType.All
    public static bool Raycast(Vector2 screenPosition, out Pose pose)
    {
        //raycastmanager로 광선 발사 및 충돌감지
        if (raycastManager.Raycast(screenPosition, hits, TrackableType.All))
        {
            //첫번째 hits의 위치 회전정보를 위부로 내보냄!!
            pose = hits[0].pose;
            return true;
        }
        else
        {
            pose = Pose.identity;
            return false;
        }
    }

    public static bool TryGetInputPosition(out Vector2 position)
    {
        position = Vector2.zero;
        //터치안하면
        if (Input.touchCount == 0)
        {
            return false;
        }

        //터치하면
        position = Input.GetTouch(0).position;

        if (Input.GetTouch(0).phase != TouchPhase.Began)
        {
            return false;
        }

        return true;
    }
}
