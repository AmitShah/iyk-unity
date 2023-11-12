using UnityEngine;
using System.Collections;

public class MessageHandler : MonoBehaviour
{
    public TMPro.TextMeshPro userNameTMP;
    public TMPro.TextMeshPro bossHealthTMP;
    public TMPro.TextMeshPro attackPowerTMP;
    // Use this for initialization
    void Start()
    {
        userNameTMP.text = "--.--";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateUserName(string name)
    {
        userNameTMP.text = name;  
    }

    public void UpdateState(string jsonData)
    {
        var state = JsonUtility.FromJson<GameState>(jsonData);
        userNameTMP.text = state.userName;

    }
}
