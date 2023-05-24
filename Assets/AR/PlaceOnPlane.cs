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
    private int[] correct;
    private int correct_idx=0;
    // Start is called before the first frame update
    private void Start()
    {

        TensorFlowLite.Consonant cons = new TensorFlowLite.Consonant(Vars.word);
        correct = cons.GetCorrectArray();

        foreach (int c in correct)
        {
            Vars.TotalWords=correct.Length;
            Debug.Log(Vars.TotalWords);
            Debug.Log(Vars.TotalWords + "test:" + c);
        }

        timer = 0;
        waitingTime = 1;

    }
        // Update is called once per frame
        private void Update()
    {

        if (correct_idx > correct.Length)
        {

            Debug.Log(touchPosition);
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
            //var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(Random.Range(0.35f, 0.65f), Random.Range(0.35f, 0.65f)));
            var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f,0.5f));


            timer += Time.deltaTime;

            if (timer < waitingTime)
            {
                //Action
                return;
            }


            ray = arCamera.ScreenPointToRay(screenCenter);

            PlacedObject.SelectedObject = null;

            if (Utility.Raycast(screenCenter, out Pose hitPose))
            {
                if (Vars.SelectedLetter == 0) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH0");
                if (Vars.SelectedLetter == 1) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH1");
                if (Vars.SelectedLetter == 2) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH2");
                if (Vars.SelectedLetter == 3) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH3");
                if (Vars.SelectedLetter == 4) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH4");
                if (Vars.SelectedLetter == 5) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH5");
                if (Vars.SelectedLetter == 6) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH6");
                if (Vars.SelectedLetter == 7) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH7");
                if (Vars.SelectedLetter == 8) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH8");
                if (Vars.SelectedLetter == 9) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH9");
                if (Vars.SelectedLetter == 10) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH10");
                if (Vars.SelectedLetter == 11) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH11");
                if (Vars.SelectedLetter == 12) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH12");
                if (Vars.SelectedLetter == 13) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH13");
                if (Vars.SelectedLetter == 14) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH14");
                if (Vars.SelectedLetter == 15) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH15");
                if (Vars.SelectedLetter == 16) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH16");
                if (Vars.SelectedLetter == 17) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedH17");
                if (Vars.SelectedLetter == 18) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM0");
                if (Vars.SelectedLetter == 19) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM1");
                if (Vars.SelectedLetter == 20) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM2");
                if (Vars.SelectedLetter == 21) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM4");
                if (Vars.SelectedLetter == 22) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM5");
                if (Vars.SelectedLetter == 23) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM6");
                if (Vars.SelectedLetter == 24) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM7");
                if (Vars.SelectedLetter == 25) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM8");
                if (Vars.SelectedLetter == 26) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM9");
                if (Vars.SelectedLetter == 27) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM12");
                if (Vars.SelectedLetter == 28) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM13");
                if (Vars.SelectedLetter == 29) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM16");
                if (Vars.SelectedLetter == 30) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM17");
                if (Vars.SelectedLetter == 31) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM18");
                if (Vars.SelectedLetter == 32) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM19");
                if (Vars.SelectedLetter == 33) placedPrefab = Resources.Load<GameObject>("Hanguel/PlacedM20");




                    Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                    //Vars.SelectedLetter++;
                    Vars.TotalWords--;

                    timer = 0;
            }
        }

    }
}
