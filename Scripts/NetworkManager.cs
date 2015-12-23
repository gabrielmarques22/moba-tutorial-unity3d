using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {

	public Text txt_status;

	// Use this for initialization
	void Start () {
		ConnectToPhoton();
	}
	
	// Update is called once per frame
	void Update () {
		// Input para criar sala
		if(Input.GetKeyDown(KeyCode.C)){
			PhotonNetwork.CreateRoom("sala_dos_trutao");
			txt_status.text = "Criou a sala";
			txt_status.color = Color.yellow;
		}

		// Input para entrar sala
		if(Input.GetKeyDown(KeyCode.E)){
			PhotonNetwork.JoinRoom("sala_dos_trutao");
		}

		// Input para instanciar os trutao
		if(Input.GetKeyDown(KeyCode.I)){
			PhotonNetwork.Instantiate("trutao", new Vector3(6, 1, 6), Quaternion.identity, 0);
			txt_status.text = "Intanciou o trutao";
			txt_status.color = Color.green;
		}

	}

	void OnJoinedRoom(){
		txt_status.text = "Entrou na sala dos trutao";
		txt_status.color = Color.magenta;
	}

	void OnJoinedLobby(){
		txt_status.text = "Tamo no lobby rapazeada";
		txt_status.color = Color.green;
	}

	void ConnectToPhoton(){
		try{
			PhotonNetwork.ConnectUsingSettings("v1.0");
			txt_status.text = "Conectou na bagaça";
			txt_status.color = Color.blue;
		} catch(UnityException error){
			Debug.Log (error);
		}
	}
}
