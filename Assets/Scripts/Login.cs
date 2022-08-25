using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;

[System.Serializable]
public class Login : MonoBehaviour
{
    public GameObject loginCanvas;
    public GameObject SignupCanvas;
    public GameObject MainMenuCanvas;
    [SerializeField] private string loginEndpoint = "https://mapty3d.herokuapp.com/api/users/login";
    [SerializeField] private string signupEndpoint = "https://mapty3d.herokuapp.com/api/users/signup";

    [SerializeField] private Button loginButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button createButton;
    [SerializeField] private Button loginButtonNext;
    [SerializeField] private Button signupButtonNext;

    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_InputField signupEmailInputField;
    [SerializeField] private TMP_InputField signupPasswordInputField;
    [SerializeField] private TMP_InputField confirmPasswordInputField;

    public static void Hide(GameObject obj)
    {
        obj.SetActive(false);
    }

    public static void Show(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void Start()
    {
        Hide(loginCanvas);
        Hide(SignupCanvas);
        Show(MainMenuCanvas);
    }

    public void OnMenuLoginClick()
    {
        Hide(SignupCanvas);
        Show(loginCanvas);
    }

    public void OnLoginClick()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;
        if (email.Length > 0 && password.Length > 0)
        {
            TryLogin(email, password);
        }
    }

    public void OnMenuCreateClick()
    {
        Hide(loginCanvas);
        Hide(MainMenuCanvas);
        Show(SignupCanvas);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void BackButtonClick()
    {
        Hide(loginCanvas);
        Hide(SignupCanvas);
        Show(MainMenuCanvas);
    }

    public void onCreateClick()
    {
        string email = signupEmailInputField.text;
        string password = signupPasswordInputField.text;
        string confirmPassword = confirmPasswordInputField.text;
        if (email.Length > 0 && password.Length > 0 && confirmPassword.Length > 0)
        {
            TryCreate(email, password, confirmPassword);
        }
    }

    private async void TryLogin(string email, string password)
    {
        try
        {
            WWWForm form = new WWWForm();
            form.AddField("email", email);
            form.AddField("password", password);
            if (email.Length <= 0 && password.Length <= 0)
            {
                return;
            }
            UnityWebRequest request = UnityWebRequest.Post(loginEndpoint, form);
            var handler = request.SendWebRequest();

            while (!handler.isDone)
            {
                await Task.Yield();
            }
            if (request.result == UnityWebRequest.Result.Success)
            {
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);
                Debug.Log(response.token);
                PlayerPrefs.SetString("token", response.token);
                PlayerPrefs.SetString("userId", response.user._id);
                SceneManager.LoadScene(1);
            }
            else
            {
                PlayerPrefs.DeleteKey("token");
                PlayerPrefs.DeleteKey("userId");
                ActivateButtons(true);
            }
            request.Dispose();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            PlayerPrefs.DeleteKey("token");
            PlayerPrefs.DeleteKey("userId");
            ActivateButtons(true);
        }
    }

    private async void TryCreate(string email, string password, string confirmPassword)
    {
        try
        {
            WWWForm form = new WWWForm();
            form.AddField("email", email);
            form.AddField("password", password);
            form.AddField("confirmPassword", confirmPassword);

            UnityWebRequest request = UnityWebRequest.Post(signupEndpoint, form);
            var handler = request.SendWebRequest();
            while (!handler.isDone)
            {
                await Task.Yield();
            }
            Debug.Log(request.result);
            if (request.result == UnityWebRequest.Result.Success)
            {
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);
                Debug.Log(response.token);
                PlayerPrefs.SetString("token", response.token);
                PlayerPrefs.SetString("userId", response.user._id);
                SceneManager.LoadScene(1);
            }
            else
            {
                PlayerPrefs.DeleteKey("token");
                PlayerPrefs.DeleteKey("userId");
            }
            request.Dispose();
            ActivateButtons(true);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            PlayerPrefs.DeleteKey("token");
            PlayerPrefs.DeleteKey("userId");
            ActivateButtons(true);
        }
    }

    private void ActivateButtons(bool toggle)
    {
        loginButton.interactable = toggle;
        createButton.interactable = toggle;
    }
}