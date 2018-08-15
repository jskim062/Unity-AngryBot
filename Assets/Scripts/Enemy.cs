using UnityEngine;
using System.Collections;

public enum EnemyState{
	Idle,
	Move,
	Attack,
	Hurt,
	Die,
}

public class Enemy : MonoBehaviour {
	public EnemyState ES;

	public Animator anim;
	float Speed;

	public float MoveSpeed;
	public float AttackSpeed;

	public float FindRange = 10f;
	public float Damage = 20f;
	public Transform Player;

	public Transform FX_Point;
	public GameObject Hit_FX;
	public AudioClip Hit_Sound;
	public AudioClip Death_Sound;

	public GameObject GUI_Pivot;
	public UISlider LifeBar;
	public float Max_hp = 100;
	public float hp = 100;

	void Start ()
	{
		Player = GameObject.Find("Player").transform;
		anim= this.GetComponent<Animator>();
	}
	void DistanceCheck(){
		if(Vector3.Distance(Player.position,transform.position)>=FindRange)
		{
			//Debug.Log("idle");
			ES = EnemyState.Idle;
			anim.SetBool("Run",false);
			Speed = 0f;
		}
		else{
			//Debug.Log("move");
				ES=EnemyState.Move;
				anim.SetBool("Run",true);
				Speed = MoveSpeed;
		}
	}
	void MoveUpdate()
	{
		transform.rotation = Quaternion.LookRotation(
										new Vector3(
										Player.position.x,this.transform.position.y,Player.position.z)
										- transform.position);
		transform.Translate(Vector3.forward * Speed*Time.deltaTime);
	}
	void Update(){
		if(ES == EnemyState.Idle){
			//Debug.Log("distance check");
			DistanceCheck();
		}
		else if(ES == EnemyState.Move){
			//Debug.Log("move update");
			MoveUpdate();
			AttackRangeCheck();
		}
	}
	void AttackRangeCheck(){
		if(Vector3.Distance(Player.position,transform.position)<1.5f && ES != EnemyState.Attack)
		{
			Speed = 0;
			ES = EnemyState.Attack;
			anim.SetTrigger("Attack");
		}
	}
	public void Attack_On(){
		Debug.Log("attach-on : " + Damage);
		Player.GetComponent<Player_Ctrl>().Hurt(Damage);
	}
	public void Hurt(float damge){
		if(hp > 0){
			ES = EnemyState.Hurt;
			Speed = 0;
			anim.SetTrigger("Hurt");

			GameObject FX = Instantiate(
															Hit_FX,
															FX_Point.position,
															Quaternion.LookRotation(FX_Point.forward)
													   	  ) as GameObject; 
			hp -= damge;
			LifeBar.sliderValue = hp/Max_hp;

			audio.clip = Hit_Sound;
			audio.Play();

			if(hp<=0){
				Death();
			}
		}
	}
	public void Death(){
		Debug.Log("code in OK");
		Debug.Log("EnemyState.Die :" + EnemyState.Die);
		ES = EnemyState.Die;

		anim.SetTrigger("Die");
		Speed = 0;

		GUI_Pivot.gameObject.SetActive(false);
		audio.clip = Death_Sound;
		audio.Play();

	
		PlayManager PM = GameObject.Find ("PlayManager").GetComponent<PlayManager>();
		PM.EnemyDie();
		//PlayManager dwpm = new PlayManager(); 
		//dwpm.EnemyDie();
		Player.GetComponent<PlayManager>().EnemyDie();

	}
}


