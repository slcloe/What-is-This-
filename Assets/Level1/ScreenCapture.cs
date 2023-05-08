using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace TensorFlowLite
{
    [RequireComponent(typeof(Button))]
    public class ScreenCapture : MonoBehaviour
    {

        [System.Serializable]
        public class DisplayNameEvent : UnityEvent<string> { }

        [ScenePath]
        public string sceneName = string.Empty;

        [SerializeField]
        private string displayName = string.Empty;

        public LoadSceneMode mode = LoadSceneMode.Single;

        public DisplayNameEvent onDisplayNameChanged = new DisplayNameEvent();

        Image image;
        public static Sprite detection_image;

        public string DisplayName
        {
            get => displayName;
            set
            {
                displayName = value;
                if (!string.IsNullOrEmpty(value))
                { onDisplayNameChanged.Invoke(displayName); }
            }
        }

        private void Start()
        {
            image = GetComponent<Image>();
        }
        //private void Update()
        //{
        //    bool check = TensorFlowLite.SsdSample.is_set_frame;

        //    if (check) { OnEnable(); }
        //    else { OnDisable(); return; }
        //}

        void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(OnButtonClick);
            DisplayName = string.IsNullOrWhiteSpace(displayName) ? sceneName : displayName;
        }

        void OnDisable()
        {
            GetComponent<Button>().onClick.RemoveListener(OnButtonClick);
        }
        void OnButtonClick()
        {
            Texture2D screenTex = new Texture2D(2400, 1080, TextureFormat.ARGB32, false);

            Rect area = new Rect(0f, 0f, 2400, 1080);
            screenTex.ReadPixels(area, 0, 0);
            //Debug.Log("take a photo");

            Texture2D imageTexture = new Texture2D(1, 1, TextureFormat.RGB24, false);
            byte[] texBuffer = screenTex.EncodeToPNG();
            imageTexture.LoadImage(texBuffer);

            Rect rect = new Rect(0, 0, imageTexture.width, imageTexture.height);
            detection_image = Sprite.Create(imageTexture, rect, Vector2.one * 0.5f);
			//image.sprite = detection_image;

			string word = TensorFlowLite.SsdSample.detection_text;
            if (word.Equals(""))
                return;
            SceneManager.LoadScene(sceneName, mode);
            //DontDestroyOnLoad(detection_image);
        }
    }
}
