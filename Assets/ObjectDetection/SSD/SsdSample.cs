using TensorFlowLite;
using UnityEngine;
using UnityEngine.UI;

namespace TensorFlowLite
{
    [RequireComponent(typeof(WebCamInput))]
    public class SsdSample : MonoBehaviour
    {
        [SerializeField]
        private SSD.Options options = default;

        [SerializeField]
        private AspectRatioFitter frameContainer = null;

        [SerializeField]
        private Text framePrefab = null;

        [SerializeField, Range(0f, 1f)]
        // 정확도 설정
        private float scoreThreshold = 0.5f;

        [SerializeField]
        private TextAsset labelMap = null;

        private SSD ssd;
        private Text[] frames;
        private string[] labels;
        public static bool is_set_frame = false;
        public static string detection_text;
        private Text text;

        private void Start()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        // This is an example usage of the NNAPI delegate.
        if (options.accelerator == SSD.Accelerator.NNAPI && !Application.isEditor)
        {
            string cacheDir = Application.persistentDataPath;
            string modelToken = "ssd-token";
            var interpreterOptions = new InterpreterOptions();
            var nnapiOptions = NNAPIDelegate.DefaultOptions;
            nnapiOptions.AllowFp16 = true;
            nnapiOptions.CacheDir = cacheDir;
            nnapiOptions.ModelToken = modelToken;
            interpreterOptions.AddDelegate(new NNAPIDelegate(nnapiOptions));
            ssd = new SSD(options, interpreterOptions);
        }
        else
#endif // UNITY_ANDROID && !UNITY_EDITOR
            {
                ssd = new SSD(options);
            }

			//Screen.orientation = ScreenOrientation.LandscapeLeft;

			// Init frames
			frames = new Text[10];
            Transform parent = frameContainer.transform;
            for (int i = 0; i < frames.Length; i++)
            {
                frames[i] = Instantiate(framePrefab, Vector3.zero, Quaternion.identity, parent);
                frames[i].transform.localPosition = Vector3.zero;
            }

            // Labels
            labels = labelMap.text.Split('\n');

            GetComponent<WebCamInput>().OnTextureUpdate.AddListener(Invoke);
            text = GameObject.Find("ssdText").GetComponent<Text>();
			if (LoginDirector.language == 0)
				text.text = "사물을 찾아보세요";
			else
				text.text = "Search Object.";
		}

        private void OnDestroy()
        {
            GetComponent<WebCamInput>().OnTextureUpdate.RemoveListener(Invoke);
            ssd?.Dispose();
        }

        private bool check_condition(Text frame, SSD.Result result, Vector2 size)
        {
            if (result.score >= scoreThreshold)
                frame.gameObject.SetActive(true);
            else
            {
                frame.gameObject.SetActive(false);
                return false;
            }
            //check frame position
            return true;
        }

        private void Invoke(Texture texture)
        {
            ssd.Invoke(texture);

            SSD.Result[] results = ssd.GetResults();
            Vector2 size = (frameContainer.transform as RectTransform).rect.size;
            //int number = 0;
            is_set_frame = false;
            detection_text = "";
            for (int i = 0; i < 10; i++)
            {
                SetFrame(frames[i], results[i], size);

                //if (results[i].score > scoreThreshold) 
                //{ 
                //    Debug.Log("i :  " + (i + 0) + "  name : " + GetLabelName(results[i].classID) + "\n");
                //}
            }
        }

        private void SetFrame(Text frame, SSD.Result result, Vector2 size)
        {
            if (result.score >= scoreThreshold && !is_set_frame)
            {
                frame.gameObject.SetActive(true);
                is_set_frame = true;
            }
            else
            {
                frame.gameObject.SetActive(false);
                return;
            }
            frame.text = $"{GetLabelName(result.classID)}";
            detection_text = frame.text;
            var rt = frame.transform as RectTransform;
            rt.anchoredPosition = result.rect.position * size - size * 0.5f;
            rt.sizeDelta = result.rect.size * size;
            //Debug.Log("cloe class id :  " + frame.text);
            //Debug.Log("cloe size x : y " + rt.sizeDelta.x + " "+ rt.sizeDelta.y);
            //Debug.Log("cloe anchoredPosition x : y" + rt.anchoredPosition.x + " " + rt.anchoredPosition.y);
   //         float minX = rt.anchoredPosition.x;
   //         float maxX = rt.anchoredPosition.x + rt.sizeDelta.x;
   //         float minY = rt.anchoredPosition.y;
   //         float maxY = rt.anchoredPosition.y - rt.sizeDelta.y;
   //         if (minX < -689 || maxX > 68)
   //         {
			//	frame.gameObject.SetActive(false);
			//	is_set_frame = false;
			//	return;
			//}
   //         if (minY > 214 || maxY < -216)
   //         {
			//	frame.gameObject.SetActive(false);
			//	is_set_frame = false;
			//	return;
			//}
            if (rt.sizeDelta.x > 756 || rt.sizeDelta.y > 433)
            {
				frame.gameObject.SetActive(false);
				is_set_frame = false;
				return;
			}
        }

        private string GetLabelName(int id)
        {
            if (id < 0 || id >= labels.Length - 1)
            {
                return "?";
            }
            return labels[id + 1];
        }

    }
}
