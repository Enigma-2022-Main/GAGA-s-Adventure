using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

public class AWSManager : MonoBehaviour
{
    public static Userdata UserData = new Userdata {
        DBKey = "null",
        AuthKey = "nullAuth",
        HighestScore = 0,
        CustomizationData = -1,
        IsGuest = true
    };
    public static Userdata UserDataTmp = null;
    public static List<Userdata> OtherUsersData;

    [DynamoDBTable("GAGA_Database")]
    public class Userdata
    {
        [DynamoDBHashKey] // Hash Key
        public string DBKey { get; set; } //Nickname
        [DynamoDBProperty]
        public string AuthKey { get; set; }
        [DynamoDBGlobalSecondaryIndexHashKey]
        public int Type { get; set; }
        [DynamoDBGlobalSecondaryIndexRangeKey]
        public float HighestScore { get; set; }
        [DynamoDBIgnore]
        public int CustomizationData { get; set; }
        [DynamoDBIgnore]
        public bool IsGuest { get; set; }
        [DynamoDBIgnore]
        public float LatestScore { get; set; }
    }

    /*
    public void UpdateDatabase(Userdata userdata)
    {
        context.SaveAsync(userdata, (result) =>
        {
            if (result.Exception == null)
                Debug.Log("Data Upload Success");
            else
                Debug.Log(result.Exception);
        });
    }

    public Userdata ReadDatabase(string id)
    {
        Userdata _userdata = new Userdata();
        context.LoadAsync<Userdata>(id, (AmazonDynamoDBResult<Userdata> result) =>
        {
            if (result.Exception != null)
            {
                Debug.LogException(result.Exception);
                return;
            }
            _userdata = result.Result;
        }, null);
        return _userdata;
    }*/
}
