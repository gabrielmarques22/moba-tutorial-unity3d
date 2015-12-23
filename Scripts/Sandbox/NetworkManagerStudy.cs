using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters;
public class NetworkManagerStudy : Photon.MonoBehaviour {
	 
	public Text txt_status;
	public GameObject player;
	// Use this for initialization
	void Start () {
		StartLobbyConnection();
	}


	public void StartLobbyConnection()
	{
		if (!PhotonNetwork.connected) 
		{
			txt_status.text = "Disconnected";
			txt_status.color = Color.red;
			try{
				PhotonNetwork.ConnectUsingSettings("1.0");

			} catch (UnityException e){
				Debug.Log("Não foi possível conectar!");
			}
		} else {
			txt_status.text = "Connected";
			txt_status.color = Color.black;
			
		}
	}
	void OnJoinedLobby(){
		txt_status.text = "Entrou nessa porra";
		txt_status.color = Color.green;
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Can't join random room!");
		PhotonNetwork.CreateRoom("teste");
	}

	void OnJoinedRoom(){
		txt_status.text = "Entrou nessa Sala";
		txt_status.color = Color.blue;
	}
		
	void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		txt_status.text = "Player instanciado";
		txt_status.color = Color.green;
//		if(player.GetComponent<PhotonView>().isMine){
//			player.GetComponent<ThirdPersonController>().enabled = true;
//		}
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.I)){
			int cont = 1;
			player = PhotonNetwork.Instantiate("player_teste", new Vector3(3,3,4), Quaternion.identity, 0);
			cont++;
		}
		if(Input.GetKeyDown(KeyCode.C)){
			try{
				PhotonNetwork.CreateRoom("teste");
				Debug.Log("criou a sala teste");
			} catch (UnityException e){
				Debug.Log("Não foi possível conectar no lobby!");
			}
		}
	}
}
