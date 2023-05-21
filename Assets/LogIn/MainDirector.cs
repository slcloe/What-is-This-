using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainDirector : MonoBehaviour
{
    Button btLogin;
    Button btSignUp;
    GameObject star;
    void Start()
    {
        btLogin = GameObject.Find("ButtonLogin").GetComponent<Button>();
        btSignUp = GameObject.Find("ButtonSignUp").GetComponent<Button>();
        star = GameObject.Find("Star");

        btLogin.onClick.AddListener(GotoLogin);
        btSignUp.onClick.AddListener(GotoSignUp);
    }

    void Update()
    {
        star.transform.Rotate(0, 0, 30 * Time.deltaTime);
    }

    void GotoLogin()
    {
        SceneManager.LoadScene("LoginScene");
    }

    void GotoSignUp()
    {
        SceneManager.LoadScene("SignUpScene");
    }
}
