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
        if (!Utility.TryGetInputPosition(out touchPosition)) return;


        ray = arCamera.ScreenPointToRay(touchPosition);
        if(Physics.Raycast(ray,out hit, Mathf.Infinity, placedObjectLayerMask))
        {
            if (hit.transform.GetComponentInChildren<PlacedObject>() != null)
            {

                PlacedObject.SelectedObject = hit.transform.GetComponentInChildren<PlacedObject>();
                Destroy(hit.transform.GetComponentInChildren<PlacedObject>());
                return;
            }
        }

        PlacedObject.SelectedObject = null;


        if (Utility.Raycast(touchPosition, out Pose hitPose))
        {
            if(Vars.test1==0) placedPrefab = Resources.Load<GameObject>("PlacedObject");
            if(Vars.test1==1) placedPrefab = Resources.Load<GameObject>("PlacedSphere");


            if (Vars.test1 == 0)
            {
                Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                Vars.test1 = 1;
            }
            else if(Vars.test1 == 1) 
            { 
                Instantiate(placedPrefab, hitPose.position, hitPose.rotation); 
                Vars.test1 = 0;
            }
        }
    }
}
