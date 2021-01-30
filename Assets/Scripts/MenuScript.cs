using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Transports.UNET;
using TMPro;
using System;

public class MenuScript : MonoBehaviour
{

    public GameObject menuPanel;
    public TextMeshProUGUI inputField;
    public TextMeshProUGUI inputClientPassword;
    public TextMeshProUGUI inputHostPassword;

    private string hostPassword;

    private void Start() {
        NetworkingManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientID, NetworkingManager.ConnectionApprovedDelegate callback)
    {
        // approval logic
        bool approve = false;

        // verify the connection data contains the password
        string roomPassword = System.Text.Encoding.UTF8.GetString(connectionData);

        if (roomPassword == hostPassword){
            approve = true;
        }

        Debug.Log($"Approval: {approve}, Room Password: {hostPassword}, Given Password: {roomPassword}, clientID: {clientID}");
        // todo: will need to update if not using prefab default hash
        callback(true, null, approve, new Vector3(0, 10, 0), Quaternion.Euler(0, -90, 0));

    }
    public void Host()
    {
        hostPassword = inputHostPassword.text;
        NetworkingManager.Singleton.StartHost();
        menuPanel.SetActive(false);
    }

    public void Join()
    {

        // validate
        if (inputField.text.Length <= 6)
        {
            NetworkingManager.Singleton.GetComponent<UnetTransport>().ConnectAddress = "127.0.0.1";
        }
        else
        {
            NetworkingManager.Singleton.GetComponent<UnetTransport>().ConnectAddress = inputField.text;
        }

        // Pass in the Client password
        string clientPassword = inputClientPassword.text;
        Debug.Log($"Connect Address: {NetworkingManager.Singleton.GetComponent<UnetTransport>().ConnectAddress}, Room Password: {clientPassword}");
        NetworkingManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.UTF8.GetBytes(inputClientPassword.text);
        
        NetworkingManager.Singleton.StartClient();
        menuPanel.SetActive(false);
    }
}
