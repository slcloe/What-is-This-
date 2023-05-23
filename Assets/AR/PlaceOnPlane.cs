using UnityEngine;

public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefab;
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private LayerMask placedObjectLayerMask;
    private Vector2 touchPosition;
    private Ray ray;
    private RaycastHit hit;


    // Start is called before the first frame update
    private void Start()
    {
        placedPrefab = Resources.Load<GameObject>("PlacedObject");
        }
        // Update is called once per frame
        private void Update()
    {
        if (Vars.TotalWords == 0)
        {

            if (!Utility.TryGetInputPosition(out touchPosition)) return;


            ray = arCamera.ScreenPointToRay(touchPosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, placedObjectLayerMask))
            {
                if (hit.transform.GetComponentInChildren<PlacedObject>() != null)
                {
                    PlacedObject.SelectedObject = hit.transform.GetComponentInChildren<PlacedObject>();
                    return;
                }
            }
        }
        else
        {

          var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(Random.Range(0.35f, 0.65f), Random.Range(0.35f, 0.65f)));

            ray = arCamera.ScreenPointToRay(screenCenter);

            PlacedObject.SelectedObject = null;


            if (Utility.Raycast(screenCenter, out Pose hitPose))
            {

                if (Vars.test1 == 0) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH0");
                if (Vars.test1 == 1) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH1");
                if (Vars.test1 == 2) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH2");
                if (Vars.test1 == 3) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH3");
                if (Vars.test1 == 4) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH4");
                if (Vars.test1 == 5) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH5");
                if (Vars.test1 == 6) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH6");
                if (Vars.test1 == 7) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH7");
                if (Vars.test1 == 8) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH8");
                if (Vars.test1 == 9) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH9");
                if (Vars.test1 == 10) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH10");
                if (Vars.test1 == 11) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH11");
                if (Vars.test1 == 12) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH12");
                if (Vars.test1 == 13) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH13");
                if (Vars.test1 == 14) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH14");
                if (Vars.test1 == 15) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH15");
                if (Vars.test1 == 16) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH16");
                if (Vars.test1 == 17) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH17");


                if ( Vars.TotalWords > 0)
                {
                    Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                    Vars.test1 ++;
                    Vars.TotalWords--;
                }


            }
        }

    }
}
