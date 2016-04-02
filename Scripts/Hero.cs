using UnityEngine;
using System.Collections;

public class Hero : CharacterBase {
		

	// Use this for initialization
	void Start () {
		this.setCurrentHP(this.getHpMax());
		this.setCurrentMana(this.getManaMax());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// Aqui eh seu cliente mandando informaçoes
			stream.SendNext(this.getCurrentHP());
		}
		else {
			// Aqui sao os outros clientes recebendo sua informaçao
			this.setCurrentHP((int)stream.ReceiveNext());
		}
	}
		
}
