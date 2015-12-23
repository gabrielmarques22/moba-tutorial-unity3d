using UnityEngine;
using System.Collections;

public class HeroMovementSandBox : MonoBehaviour {

	private NavMeshAgent agent;
	private Vector3 targetDestination;
	private PhotonView photon;
	private Animator animator;
	private PhotonAnimatorView photonAnimator;
	private bool isMoving = false;

	// Use this for initialization
	void Start () {
		agent = this.GetComponent<NavMeshAgent>();
		photon = this.GetComponent<PhotonView>();
		animator = this.GetComponent<Animator>();
		photonAnimator = this.GetComponent<PhotonAnimatorView>();
		targetDestination = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1) && photon.isMine){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)){
				targetDestination = hit.point;
				agent.SetDestination(targetDestination);

			}
		}
		if(photon.isMine){
			if(Vector3.Distance(transform.position, targetDestination) > 0.5f){
				isMoving = true;
				animator.SetBool("moving", isMoving);
			} else {
				isMoving = false;
				animator.SetBool("moving", isMoving);
			}
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(animator.GetBool("moving"));
		}
		else
		{
			animator.SetBool("moving", (bool)stream.ReceiveNext());
		}
	}
}
