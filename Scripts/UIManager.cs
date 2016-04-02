using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private Hero hero;
	private Text txt_hp;
	private PhotonView photon;

	// Use this for initialization
	void Start () {
		hero = this.GetComponent<Hero>();
		photon = this.GetComponent<PhotonView>();
		txt_hp = GameObject.Find("txt_hero_hp_value").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(photon.isMine){			
			txt_hp.text = hero.getCurrentHP().ToString();
		}
	}
}
