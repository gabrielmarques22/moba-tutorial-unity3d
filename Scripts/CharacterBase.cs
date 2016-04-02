using UnityEngine;
using System.Collections;

public class CharacterBase : MonoBehaviour {


	public string nome;
	public int id, strenght, defense, inteligence, movementSpeed, lvl;
	private int attack, magicalAttack, hpMax, manaMax, currentHP, currentMANA;
    public GameObject selected;
	private PhotonView photon;


	// Use this for initialization
	void Start () {
		photon = this.GetComponent<PhotonView>();
	}

    public void SelectCharacter()
    {
        selected.SetActive(true);
    }

    public void UnselectCharacter()
    {
        selected.SetActive(false);
    }

    int CalculateAttack(int strenght){
		return Random.Range(strenght - 2, strenght + 2);
	}

	public int getAttack(){
		return CalculateAttack(this.strenght);
	}

	int CalculateMagicAttack(int inteligence){
		return Random.Range(inteligence - 2, inteligence + 2);
	}
	
	public int getMagicAttack(){
		return CalculateMagicAttack(this.inteligence);
	}

	int CalculateHpMax(int strenght, int lvl){
		return Mathf.RoundToInt((strenght + 5 * lvl) * 100);
	}
	
	public int getHpMax(){
		return CalculateHpMax(this.strenght, this.lvl);
	}

	int CalculateMana(int inteligence){
		return Mathf.RoundToInt(inteligence * 100);
		 
	}

	public int  getManaMax(){
		return CalculateMana(this.inteligence);
	}

	public void setCurrentHP(int hp){
		currentHP = hp;
	}

	public void setCurrentMana(int mana){
		currentMANA = mana;
	}

	public int getCurrentHP(){
		return currentHP;
	}

	public int getCurrentMana(){
		return currentMANA;
	}

	[PunRPC]
	public void ApplyDamage(int attackValue){
		if(photon.isMine){
			int hp = getCurrentHP();
			hp -= attackValue; 
			//		Debug.Log("bateu " + attackValue);
			this.setCurrentHP(hp);
		}
	}
}
