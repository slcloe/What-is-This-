using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainDirector : MonoBehaviour
{
    Button btLogin;
    Button btSignUp;
    void Start()
    {
        btLogin = GameObject.Find("ButtonLogin").GetComponent<Button>();
        btSignUp = GameObject.Find("ButtonSignUp").GetComponent<Button>();

        btLogin.onClick.AddListener(GotoLogin);
        btSignUp.onClick.AddListener(GotoSignUp);
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
