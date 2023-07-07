using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

public class PlaymenuButtonController : MonoBehaviour
{
    private bool readyForLogin = false;

    [SerializeField] private GameObject loginPopup;
    [SerializeField] private GameObject OKPopup;
    [SerializeField] private TextMeshProUGUI OKAlertText;
    [SerializeField] private GameObject GuestOXPopup;
    [SerializeField] private GameObject RegisterOXPopup;
    [SerializeField] private TextMeshProUGUI RegisterOXAlertText;
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private TMP_InputField inputAuthKey;
    [SerializeField] private GameObject networkInfoPopup;

    CognitoAWSCredentials credentials;
    DynamoDBContext context;
    AmazonDynamoDBClient DBClient;

    private void Awake()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);
        credentials = new CognitoAWSCredentials("ap-northeast-2:b70964ec-0f8d-4a12-9493-04a0c2e77f18", RegionEndpoint.APNortheast2);
        DBClient = new AmazonDynamoDBClient(credentials, RegionEndpoint.APNortheast2);
        context = new DynamoDBContext(DBClient);
    }

    private void Start()
    {
        loginPopup.SetActive(false);
        OKPopup.SetActive(false);
        GuestOXPopup.SetActive(false);
        RegisterOXPopup.SetActive(false);
        networkInfoPopup.SetActive(false);

        Time.timeScale = 1;
    }

    private void Update()
    {
        //Debug.Log(Time.timeScale);
        //로그인 처리
        if (readyForLogin)
        {
            readyForLogin = false;
            loginManager();
            AWSManager.UserDataTmp = null;
        }
    }

    public void onClickSinglePlayerButton()
    {
        GameManager.IsMultiPlayer = false;
        
        loginPopup.SetActive(true);
    }

    public void onClickMultiPlayerButton()
    {
        GameManager.IsMultiPlayer = true;
        loginPopup.SetActive(true);
    }

    public void onClickBackButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void onClickLoginButton_LoginPopup()
    {
        //로그인 처리


        //입력값 유효성 검사

        if (String.IsNullOrWhiteSpace(inputName.text) || String.IsNullOrWhiteSpace(inputAuthKey.text))
        {
            OKAlertText.text = "닉네임과 비밀번호로 공백을 사용할 수 없습니다";
            OKPopup.SetActive(true);
        }
        else
        {
            if (inputName.text.Any(x => Char.IsWhiteSpace(x)) || inputAuthKey.text.Any(x => Char.IsWhiteSpace(x)))
            {
                OKAlertText.text = "닉네임과 비밀번호는 공백을 포함할 수 없습니다";
                OKPopup.SetActive(true);
            }
            else
            {
                AWSManager.UserData.DBKey = inputName.text;
                AWSManager.UserData.AuthKey = inputAuthKey.text;
                ReadDatabase(AWSManager.UserData.DBKey); //loginManager()로 넘어감
            }
        }

    }

    public void onClickGuestButton_LoginPopup()
    {
        GuestOXPopup.SetActive(true);
    }

    public void onClickXButton_LoginPopup()
    {
        loginPopup.SetActive(false);
    }

    public void onClickXButton_NetworkInfoPopup()
    {
        NetworkLobbyManager.DisconnectFromPunServerFlag = true;
        networkInfoPopup.SetActive(false);
    }

    public void onClickOKButton()
    {
        OKPopup.SetActive(false);
    }

    public void onClickConfirmButton_GuestOXPopup()
    {
        GuestOXPopup.SetActive(false);
        AWSManager.UserData.DBKey = "Guest " + UnityEngine.Random.Range(100, 999).ToString();
        AWSManager.UserData.IsGuest = true;
        if (GameManager.IsMultiPlayer)
        {
            loginToMultiplayer();
        }
        else
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    public void onClickCancelButton_GuestOXPopup()
    {
        GuestOXPopup.SetActive(false);
    }
    
    public void onClickConfirmButton_RegisterOXPopup()
    {
        RegisterOXPopup.SetActive(false);
        UpdateDatabase();
        AWSManager.UserData.IsGuest = false;
        if (GameManager.IsMultiPlayer)
        {
            loginToMultiplayer();
        }
        else
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    public void onClickCancelButton_RegisterOXPopup()
    {
        RegisterOXPopup.SetActive(false);
    }



    private void UpdateDatabase()
    {
        context.SaveAsync(AWSManager.UserData, (result) =>
        {
            if (result.Exception == null)
                Debug.Log("Data Upload Success");
            else
                Debug.Log(result.Exception);
        });
    }

    private void ReadDatabase(string id)
    {
        context.LoadAsync<AWSManager.Userdata>(id, (AmazonDynamoDBResult<AWSManager.Userdata> result) =>
        {
            Debug.Log("Load");

            if (result.Exception != null)
            {
                Debug.LogException(result.Exception);
                return;
            }
            AWSManager.UserDataTmp = result.Result;
            readyForLogin = true;
        }, null);
        return;
    }

    private void loginManager()
    {
        if (AWSManager.UserDataTmp == null)
        {
            RegisterOXAlertText.text = "닉네임: " + AWSManager.UserData.DBKey + "\n인증 키: " + AWSManager.UserData.AuthKey;
            RegisterOXPopup.SetActive(true);
        }
        else
        {
            if (AWSManager.UserData.AuthKey == AWSManager.UserDataTmp.AuthKey)
            {
                AWSManager.UserData = AWSManager.UserDataTmp;
                if (GameManager.IsMultiPlayer)
                {
                    loginToMultiplayer();
                }
                else
                {
                    SceneManager.LoadScene("GameScene");
                }
            }
            else
            {
                OKAlertText.text = "아이디와 비밀번호가 일치하지 않습니다";
                OKPopup.SetActive(true);
            }
        }
    }

    private void loginToMultiplayer()
    {
        networkInfoPopup.SetActive(true);
        NetworkLobbyManager.StartConnectToPunMasterServerFlagArg = AWSManager.UserData.DBKey;
        NetworkLobbyManager.StartConnectToPunMasterServerFlag = true;
    }
}
