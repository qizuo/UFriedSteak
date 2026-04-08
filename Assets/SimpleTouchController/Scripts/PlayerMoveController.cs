using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMoveController : MonoBehaviour {

	// PUBLIC
	public SimpleTouchController leftController;
	public JoyStickHelp mvDataJoyStk;
	public SimpleTouchController rightController;
	public Transform headTrans;
	public float speedMovements = 15f;
	public float speedContinuousLook = 100f;
	public float speedProgressiveLook = 3000f;

	// PRIVATE
	private Rigidbody _rigidbody;
	[SerializeField] bool continuousRightController = true;

	Vector3 lastPos;

	MainForm _mfObj;
	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		if(rightController!=null){
			rightController.TouchEvent += RightController_TouchEvent;
		}
		lastPos = _rigidbody.transform.position;
	}

	public bool ContinuousRightController
	{
		set{continuousRightController = value;}
	}

	void RightController_TouchEvent (Vector2 value)
	{
		if(!continuousRightController)
		{
			UpdateAim(value);
		}
	}

	public void setMnFmObj(MainForm val){
		_mfObj = val;
	}

	public Action<bool,Vector3> directAc;

	public float oriMnPosZ=0;

	public int staCt = 0;

	void Update()
	{
		// Debug.Log("playr up.."+mvDataJoyStk+" "+DragDirHelp.isPanMv);
		// move
		if(mvDataJoyStk!=null&&DragDirHelp.isPanMv){
			
			// bool fobdTn = _mfObj.getForbidTns(-1);
			// bool fobdGBk = _mfObj.getForbidGBk(-1);
			// bool isOutPl = _mfObj.checkMnIsOutPl();
			Vector2 dirVec = new Vector2(mvDataJoyStk.TouchedAxis.x,mvDataJoyStk.TouchedAxis.y);

			Vector3 verPos = -transform.forward * dirVec.x * Time.deltaTime * speedMovements;
			Vector3 horPos = transform.right * dirVec.y * Time.deltaTime * speedMovements;

			_mfObj.idleCenter.x+=verPos.x;
			_mfObj.idleCenter.y+=horPos.z;		
			// Debug.Log("play mv.."+horPos+" "+verPos);
			// bool isOutPl = _mfObj.checkMnIsOutPl(horPos.x,verPos.z);
			// bool canMv = _mfObj.getCanMvFords(-1);
			
			// Debug.Log("player mv.."+horPos+verPos);
			
			Vector3 newPos = transform.position + verPos + horPos;
			// if(newPos.x<-3.8f||newPos.x>3.8f){
			// 	isMv = false;
			// }
			// else if(newPos.z<-9.5f||newPos.z>1.5f){
			// 	isMv = false;
			// }
			// Debug.Log("ppp.."+newPos);
			_rigidbody.MovePosition(newPos);
			// Debug.Log("pmc.."+_rigidbody.transform.localPosition);


			float deX = transform.position.x - lastPos.x;
			float deY = transform.position.z - lastPos.z;
			// Debug.Log(deX+" "+deY);
			// if(mvDataJoyStk.TouchedAxis.magnitude==0){
			// 	isMv = false;
			// }

			// Vector3 dir = transform.position - lastPos;	
			// directAc?.Invoke(isMv,dir);			
			// lastPos = transform.position;
		}
		
		if(continuousRightController)
		{
			// if(rightController!=null){
			// UpdateAim(leftController.GetTouchPosition);
			// }
		}
	}

	void UpdateAim(Vector2 value)
	{
		if(headTrans != null)
		{
			Quaternion rot = Quaternion.Euler(0f,
				transform.localEulerAngles.y - value.x * Time.deltaTime * -speedProgressiveLook,
				0f);

			_rigidbody.MoveRotation(rot);

			rot = Quaternion.Euler(headTrans.localEulerAngles.x - value.y * Time.deltaTime * speedProgressiveLook,
				0f,
				0f);
			headTrans.localRotation = rot;

		}
		else
		{

			Quaternion rot = Quaternion.Euler(transform.localEulerAngles.x - value.y * Time.deltaTime * speedProgressiveLook,
				transform.localEulerAngles.y + value.x * Time.deltaTime * speedProgressiveLook,
				0f);

			_rigidbody.MoveRotation(rot);
		}
	}

	void OnDestroy()
	{
		// rightController.TouchEvent -= RightController_TouchEvent;
	}

}
