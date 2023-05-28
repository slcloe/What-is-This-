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


    private float timer;
    private int waitingTime;
   private int correct_idx=0;

    // Start is called before the first frame update
    private void Start()
    {

        // Vars.word = TensorFlowLite.SsdSample.detection_text;
        Vars.word = "°ø";
        TensorFlowLite.Consonant cons = new TensorFlowLite.Consonant(Vars.word);
        Vars.AR_correct = cons.GetCorrectArray();
        Vars.TotalWords = Vars.AR_correct.Length;
        Vars.FinalWords = Vars.AR_correct.Length;
       Vars.Check_idx = 0;
        timer = 0;
        waitingTime = 1;
    }
        // Update is called once per frame
        private void Update()
    {

        if (Vars.TotalWords<=0)
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
            //  var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(Random.Range(0.35f, 0.65f), Random.Range(0.35f, 0.65f)));
            // var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f,0.5f));

            //  timer += Time.deltaTime;

            //if (timer < waitingTime)
            //{
            //   //Action
            //   return;
            //}


            if (!Utility.TryGetInputPosition(out touchPosition)) return;


            ray = arCamera.ScreenPointToRay(touchPosition);

            PlacedObject.SelectedObject = null;


            if (Utility.Raycast(touchPosition, out Pose hitPose))
            {
                if ((correct_idx + 1) % 3 == 0)
                {
                    if (Vars.AR_correct[correct_idx] == 0)
                    {
                        Vars.TotalWords--;
                        correct_idx++;
                        timer = 0;
                        return;
                    }

                    if (Vars.AR_correct[correct_idx] == 1) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH0"); 
                    if (Vars.AR_correct[correct_idx] == 2) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH1"); 
                    if (Vars.AR_correct[correct_idx] == 4) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH2"); 
                    if (Vars.AR_correct[correct_idx] == 7) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH3"); 
                    if (Vars.AR_correct[correct_idx] == 8) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH5"); 
                    if (Vars.AR_correct[correct_idx] == 16) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH6"); 
                    if (Vars.AR_correct[correct_idx] == 17) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH7"); 
                    if (Vars.AR_correct[correct_idx] == 19) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH9"); 
                    if (Vars.AR_correct[correct_idx] == 20) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH10"); 
                    if (Vars.AR_correct[correct_idx] == 21) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH11");
                    if (Vars.AR_correct[correct_idx] == 22) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH12");
                    if (Vars.AR_correct[correct_idx] == 23) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH14");
                    if (Vars.AR_correct[correct_idx] == 24) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH15");
                    if (Vars.AR_correct[correct_idx] == 25) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH16");
                    if (Vars.AR_correct[correct_idx] == 26) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH17");
                }
                else if ((correct_idx + 1) % 3 == 2)
                {
                    if (Vars.AR_correct[correct_idx] == 0) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM0");
                    if (Vars.AR_correct[correct_idx] == 1) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM1");
                    if (Vars.AR_correct[correct_idx] == 2) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM2");
                    if (Vars.AR_correct[correct_idx] == 4) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM4");
                    if (Vars.AR_correct[correct_idx] == 5) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM5");
                    if (Vars.AR_correct[correct_idx] == 6) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM6");
                    if (Vars.AR_correct[correct_idx] == 7) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM7");
                    if (Vars.AR_correct[correct_idx] == 8) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM8");
                    if (Vars.AR_correct[correct_idx] == 9) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM9");
                    if (Vars.AR_correct[correct_idx] == 12) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM12");
                    if (Vars.AR_correct[correct_idx] == 13) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM13");
                    if (Vars.AR_correct[correct_idx] == 16) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM16");
                    if (Vars.AR_correct[correct_idx] == 17) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM17");
                    if (Vars.AR_correct[correct_idx] == 18) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM18");
                    if (Vars.AR_correct[correct_idx] == 19) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM19");
                    if (Vars.AR_correct[correct_idx] == 20) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM20");
                }
                else
                {
                    if (Vars.AR_correct[correct_idx] == 0)placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH0");
                    if (Vars.AR_correct[correct_idx] == 1)placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH1");
                    if (Vars.AR_correct[correct_idx] == 2)placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH2");
                    if (Vars.AR_correct[correct_idx] == 3)placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH3");
                    if (Vars.AR_correct[correct_idx] == 4)placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH4");
                    if (Vars.AR_correct[correct_idx] == 5)placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH5");
                    if (Vars.AR_correct[correct_idx] == 6)placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH6");
                    if (Vars.AR_correct[correct_idx] == 7)placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH7");

                    if (Vars.AR_correct[correct_idx] == 8)placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH8");
                    if (Vars.AR_correct[correct_idx] == 9)placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH9");
                    if (Vars.AR_correct[correct_idx] == 10) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH10");
                    if (Vars.AR_correct[correct_idx] == 11) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH11");
                    if (Vars.AR_correct[correct_idx] == 12) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH12");
                    if (Vars.AR_correct[correct_idx] == 13) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH13");
                    if (Vars.AR_correct[correct_idx] == 14) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH14");
                    if (Vars.AR_correct[correct_idx] == 15) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH15");
                    if (Vars.AR_correct[correct_idx] == 16) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH16");
                    if (Vars.AR_correct[correct_idx] == 17)  placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH17");
                    if (Vars.AR_correct[correct_idx] == 18)  placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH18");
                }
                Debug.Log("AR1 " + Vars.TotalWords);
                if (Vars.TotalWords > 0)
                {
                    Debug.Log("AR "+ Vars.TotalWords);
                    Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                    Vars.TotalWords--;
                    correct_idx++;
                    timer = 0;
                }
            }
        }

    }
}
