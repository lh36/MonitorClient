using UnityEngine;
using System.Collections;

/*
 摄像机镜头远近匹配函数：y = -1.61 - 0.28 * z;
*/

public class CameraController : MonoBehaviour {

	public GameObject CameraCenter;         //摄像机中心
	public GameObject LookAt;               //观察的对象

	public float LerpingSpeed = 8f;         //插值速度
	public float MinFieldDistance = 4f;     //最小缩放距离
	public float MaxFieldDistance = 20f;    //最大缩放距离
	public float FieldScrollSpeed = 1f;     //缩放速度
	public float XRotationSpeed = 1f;       //X旋转速度
	public float YRotationSpeed = 1f;       //Y旋转速度
	public float MinYRotation = -15f;       //Y最小角度
	public float MaxYRotation = 50f;        //Y最大角度

	private bool m_bIsScrollUpdating = false;   //是否正在缩放插值
	private float m_fScroll;                    //缩放值
	private Vector3 m_v3DistField;              //目标缩放，用于线性插值
	private bool m_bIsRotationUpdating = false; //是否正在旋转插值
	private float m_fXRotation;                 //X方向旋转值
	private float m_fYRotation;                 //Y方向旋转值
	private Quaternion m_qRotation;             //四元数角度值


	void Start()
	{
		m_v3DistField = gameObject.transform.localPosition;
		m_fXRotation = CameraCenter.transform.localEulerAngles.y;
		m_fYRotation = CameraCenter.transform.localEulerAngles.x;
	}

	void LateUpdate()
	{
		CameraCenter.transform.position = LookAt.transform.position;
		scrollUpdate();
		rotationUpdate();
	}

	/// <summary>
	/// 缩放控制
	/// </summary>
	private void scrollUpdate()
	{
		m_fScroll = InputManager.Instance.GetScroll();
		if(m_fScroll != 0)
		{
			Debug.Log (m_fScroll);
			float z = - Mathf.Clamp(-gameObject.transform.localPosition.z + Time.deltaTime * FieldScrollSpeed * m_fScroll, 
				MinFieldDistance, MaxFieldDistance);
			float y = -1.61f - 0.28f * z;
			m_v3DistField = new Vector3(0, y, z);
			m_bIsScrollUpdating = true;
		}
		if(m_bIsScrollUpdating)
		{
			gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, m_v3DistField, LerpingSpeed * Time.deltaTime);
			if(gameObject.transform.localPosition == m_v3DistField)
			{
				m_bIsScrollUpdating = false;
			}
		}
	}

	/// <summary>
	/// 旋转控制
	/// </summary>
	private void rotationUpdate()
	{
		if(InputManager.Instance.IsRightMouseStay())
		{
			Vector2 mouseXYDelta = InputManager.Instance.GetMouseXYDelta();
			m_fXRotation += mouseXYDelta.x * XRotationSpeed;
			m_fYRotation -= mouseXYDelta.y * YRotationSpeed;
			m_fYRotation = MathUtil.ClampAngle(m_fYRotation, MinYRotation, MaxYRotation);
			m_qRotation = Quaternion.Euler(m_fYRotation, m_fXRotation, 0);

			m_bIsRotationUpdating = true;
		}
		if(m_bIsRotationUpdating)
		{
			CameraCenter.transform.localRotation = Quaternion.Lerp(CameraCenter.transform.localRotation, m_qRotation, LerpingSpeed * Time.deltaTime);
			if(CameraCenter.transform.localRotation == m_qRotation)
			{
				m_bIsRotationUpdating = false;
			}
		}
	}


}
