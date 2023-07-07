using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class visualizeVersionData : MonoBehaviour
{
    private TextMeshProUGUI text;

    public static visualizeVersionData Instance;

    void Awake()
    {
        gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Application.productName + " v. " + Application.version + " (Unity " + Application.unityVersion + ")";

        
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
}
