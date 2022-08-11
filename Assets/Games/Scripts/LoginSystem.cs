using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSystem : MonoBehaviour
{
    [SerializeField] private Transform login_panel;
    [SerializeField] private TMP_InputField login_email;
    [SerializeField] private TMP_InputField login_password;

    [SerializeField] private Transform register_panel;
    [SerializeField] private TMP_InputField register_email;
    [SerializeField] private TMP_InputField register_display;
    [SerializeField] private TMP_InputField register_password;

    private void Start()
    {
        PlayFabAuthService.Instance.IsAuthenticated(result =>
        {
            OnSuccess();
        }, error =>
        {

        });
    }

    public void RegisterUser()
    {
        PlayFabAuthService.Instance.RegisterPlayer(register_email.text, register_display.text, register_password.text, result =>
        {
            PlayFabAuthService.Instance.LinkCustomID(result =>
            {
                OnSuccess();
            }, null);
        }, error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }

    public void LoginUser()
    {
        PlayFabAuthService.Instance.LoginWithEmailAndPassword(login_email.text, login_password.text, result =>
        {
            PlayFabAuthService.Instance.LinkCustomID(result =>
            {
                OnSuccess();
            }, null);
        }, error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }

    public void OnSuccess()
    {
        SceneManager.LoadScene(1);
    }
}
