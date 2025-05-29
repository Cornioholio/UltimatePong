using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNetwork : MonoBehaviour
{
    public void StartServer() 
    {
        
        NetworkManager.Singleton.StartServer();
    }
    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }
}
