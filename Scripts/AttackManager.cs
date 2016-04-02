using UnityEngine;
using System.Collections;

public class AttackManager : MonoBehaviour {

    public GameObject selected;
    public bool isAttacking, canPerformAttack;
    private PhotonView photon;
    private Animator animator;
    private HeroMovement heroMovement;
    private Ray ray;
    private RaycastHit hit;
	private Hero hero;
	public int idAttacked;

	// Use this for initialization
	void Start () {
        photon = this.GetComponent<PhotonView>();
        animator = this.GetComponent<Animator>();
        isAttacking = false;
        canPerformAttack = false;
        heroMovement = this.GetComponent<HeroMovement>();
		hero = this.GetComponent<Hero>();

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1)) {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit)) {
                if(hit.collider.CompareTag("Hero") && !hit.collider.GetComponent<PhotonView>().isMine) {
                    selected = hit.collider.gameObject;
                    selected.GetComponent<Hero>().SelectCharacter();
					idAttacked = hit.collider.GetComponent<PhotonView>().ownerId;
                    isAttacking = true;
                } else {
                    if (selected)
                    {
                        selected.GetComponent<Hero>().UnselectCharacter();
                    }
                    isAttacking = false;
                    canPerformAttack = false;
                    animator.SetBool("isAttacking", false);
                }
            } 
        }

        if(isAttacking && canPerformAttack && photon.isMine)
        {
            animator.SetBool("isAttacking", true);
        }
	}

	public void Attack(){
		int attackValue = hero.getAttack();
		PhotonPlayer playerAttacked = PhotonPlayer.Find(this.idAttacked);
//		Debug.Log(PhotonPlayer.Find(this.idAttacked));
		photon.RPC("ApplyDamage", playerAttacked, attackValue);
	}
}
