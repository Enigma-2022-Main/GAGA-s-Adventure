using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;
using UnityEngine.SceneManagement;
public class ScoreboardManager : MonoBehaviour
{
    CognitoAWSCredentials credentials;
    AmazonDynamoDBClient DBClient;
    DynamoDBContext context;

    [SerializeField]
    GameObject panel;

    GameObject panelTmp;

    private bool readyForInstantiatePanels = false;
    [SerializeField]
    private int panelNum = 20;

    private void Awake()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);
        credentials = new CognitoAWSCredentials("ap-northeast-2:b70964ec-0f8d-4a12-9493-04a0c2e77f18", RegionEndpoint.APNortheast2);
        DBClient = new AmazonDynamoDBClient(credentials, RegionEndpoint.APNortheast2);
        context = new DynamoDBContext(DBClient);
    }

    private void Start()
    {
        LoadOtherUsersData(panelNum);
    }

    private void Update()
    {
        if (readyForInstantiatePanels)
        {
            Debug.Log("Update");
            readyForInstantiatePanels = false;
            createListItem();
        }
    }

    private void LoadOtherUsersData(int num)
    {
        Debug.Log("LoadOtherUsersData");
        QueryFilter filter = new QueryFilter();
        filter.AddCondition("Type", QueryOperator.Equal, 0);
        filter.AddCondition("HighestScore", QueryOperator.GreaterThan, 0);

        QueryOperationConfig queryOperationConfig = new QueryOperationConfig
        {
            IndexName = "ScoreboardData",
            Limit = num,
            Filter = filter
        };

        context.FromQueryAsync<AWSManager.Userdata>(queryOperationConfig, (result) =>
        {
            if (result.Exception != null)
            {
                Debug.LogException(result.Exception);
                return;
            }
            result.Result.GetNextSetAsync((_result) =>
            {
                if (_result.Exception != null)
                {
                    Debug.LogException(_result.Exception);
                    return;
                }
                AWSManager.OtherUsersData = _result.Result;
                readyForInstantiatePanels = true;
            }, null);
        }, null);
        return;
    }


    private void createListItem()
    {
        int count = 1;
        foreach (AWSManager.Userdata userdata in AWSManager.OtherUsersData)
        {
            panelTmp = Instantiate(panel);
            panelTmp.transform.SetParent(GameObject.Find("Content").transform);
            panelTmp.transform.position = new Vector3(panelTmp.transform.position.x, panelTmp.transform.position.y, 0);
            panelTmp.transform.localScale = new Vector3(1, 1, 1);
            panelTmp.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = count.ToString();
            panelTmp.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = userdata.DBKey;
            panelTmp.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = timeFloatToVisualizableString(userdata.HighestScore);
            count++;
        }
    }

    private string timeFloatToVisualizableString(float time)
    {
        string[] textTemp = new string[3] { "00", "00", "00" };

        int hour = (int)time / 3600;
        int min = (int)time / 60 % 60;
        int sec = (int)time % 60;

        if (hour == 0)
        {
            textTemp[0] = "00";
        }
        else if (hour < 10)
        {
            textTemp[0] = "0" + hour.ToString();
        }
        else textTemp[0] = hour.ToString();

        if (min == 0)
        {
            textTemp[1] = "00";
        }
        else if (min < 10)
        {
            textTemp[1] = "0" + min.ToString();
        }
        else textTemp[1] = min.ToString();

        if (sec == 0)
        {
            textTemp[2] = "00";
        }
        else if (sec < 10)
        {
            textTemp[2] = "0" + sec.ToString();
        }
        else textTemp[2] = sec.ToString();

        return (textTemp[0].ToString() + ":" + textTemp[1].ToString() + ":" + textTemp[2].ToString());
    }

    public void onClickBackButton()
    {
        SceneManager.LoadScene("TitleScene");
    }


}
