using UnityEngine;
using System.Collections;

public class HeroMovement : MonoBehaviour {

	private NavMeshAgent agent;
	private Vector3 targetDestination;
	private PhotonView photon;
	private Animator animator;
    private AttackManager attackManager;


	// Use this for initialization
	void Start () {
		agent = this.GetComponent<NavMeshAgent>();
		photon = this.GetComponent<PhotonView>();
		animator = this.GetComponent<Animator>();
		targetDestination = transform.position;
        attackManager = this.GetComponent<AttackManager>();
	}

    // Update is called once per frame
    void Update()
    {
        if (photon.isMine)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    agent.Resume();
                    targetDestination = hit.point;
                    agent.SetDestination(targetDestination);

                }
            }
            if (Vector3.Distance(transform.position, targetDestination) > 0.5f)
            {
                animator.SetBool("isMoving", true);
            }
            else {
                animator.SetBool("isMoving", false);
            }
            if (attackManager.isAttacking) {
                if(Vector3.Distance(transform.position, attackManager.selected.transform.position) > 1)
                {
                    attackManager.canPerformAttack = false;
                    animator.SetBool("isAttacking", false);
                    Follow(attackManager.selected);
                } else if (Vector3.Distance(transform.position, attackManager.selected.transform.position) <= 1){
                    Stop();
                    attackManager.canPerformAttack = true;
                }
            }

        }
    }

    public void Follow(GameObject target) {
        agent.Resume();
        agent.SetDestination(target.transform.position);
        animator.SetBool("isMoving", true);               
    }
    public void Stop() {
        agent.Stop();
        animator.SetBool("isMoving", false);

    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if(stream.isWriting){
			// Aqui eh seu cliente mandando informaçoes
			stream.SendNext(animator.GetBool("isMoving"));
		} else {
			// Aqui sao os outros clientes recebendo sua informaçao
			animator.SetBool("isMoving", (bool)stream.ReceiveNext());
		}
	}
}
