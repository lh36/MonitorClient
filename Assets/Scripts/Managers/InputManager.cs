using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : SingletonUnity<InputManager>
{
	private float m_fFrontAndBack = 0;          //前后
	private float m_fLeftAndRight = 0;          //左右
	private float m_fScroll = 0;                //滚轮
	private int m_iLeftMouseClick = 0;          //鼠标左键点击
	private int m_iRightMouseClick = 0;         //鼠标右键点击
	private bool m_bIsLeftMouseStay = false;      //鼠标左键按住
	private bool m_bIsRightMouseStay = false;     //鼠标右键按住
	private Vector2 m_v2MouseXYDelta = new Vector2 ();

	private Vector2 m_OldPosition1 = new Vector2();
	private Vector2 m_OldPosition2 = new Vector2();

	private ControlMode m_ControlMode = ControlMode.OpenControl;
	private List<Vector2> m_ClickPointList = new List<Vector2> ();

	public Transform Trans_RightView;
	private float m_RightViewPosX;

	void Start()
	{
		Input.multiTouchEnabled = true;
		this.m_RightViewPosX = Trans_RightView.position.x - Trans_RightView.gameObject.GetComponent<RectTransform> ().sizeDelta.x / 2;
	}

	void Update () 
	{
		ControlInput ();

		if(GlobalManager.Instance.IsGameRunning)
		{
			if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
			{
				PCInput ();
			}
			else if(Application.platform == RuntimePlatform.Android)
			{
				AndroidInput ();
			}
		}
		else
		{
			m_fFrontAndBack = 0;
			m_fLeftAndRight = 0;
			m_fScroll = 0;
			m_iLeftMouseClick = 0;
			m_iRightMouseClick = 0;
			m_bIsLeftMouseStay = false;
			m_bIsRightMouseStay = false;
			m_v2MouseXYDelta = new Vector2 ();
		}

	}

	private void PCInput()
	{
		m_fFrontAndBack = (int) Input.GetAxis("Vertical");
		if(m_fFrontAndBack != 0)
		{
			m_fFrontAndBack = (m_fFrontAndBack > 0) ? 1 : -1;
		}

		m_fLeftAndRight = (int) Input.GetAxis("Horizontal");
		if(m_fLeftAndRight != 0)
		{
			m_fLeftAndRight = (m_fLeftAndRight > 0) ? 1 : -1;
		}

		m_fScroll = Input.GetAxis("Mouse ScrollWheel") * 2;
		m_iLeftMouseClick = Input.GetMouseButtonUp(0) ? 1:0;
		m_iRightMouseClick = Input.GetMouseButtonUp(1) ? 1:0;
		m_bIsLeftMouseStay = Input.GetMouseButton(0);
		m_bIsRightMouseStay = Input.GetMouseButton(1);
		m_v2MouseXYDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
	}

	private void AndroidInput()
	{
        if(Input.touchCount == 1)
        {
			if(Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				m_bIsRightMouseStay = true;
				m_v2MouseXYDelta = Input.GetTouch(0).deltaPosition;
			}
        }
		else if(Input.touchCount > 1)
		{
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                //计算出当前两点触摸点的位置
                var tempPosition1 = Input.GetTouch(0).position;
                var tempPosition2 = Input.GetTouch(1).position;
                //函数返回真为放大，返回假为缩小
                if(isEnlarge(m_OldPosition1,m_OldPosition2,tempPosition1,tempPosition2))
                {
                    m_fScroll = -0.03f;
                }else
                {
                    m_fScroll = 0.03f;
                }
            //备份上一次触摸点的位置，用于对比
            m_OldPosition1 = tempPosition1;
            m_OldPosition2 = tempPosition2;
            }
		}
        else
        {
            m_bIsRightMouseStay = false;
            m_v2MouseXYDelta = new Vector2();
            m_fScroll = 0;
        }

	}

    //函数返回真为放大，返回假为缩小
    private bool isEnlarge(Vector2 oP1,  Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        //函数传入上一次触摸两点的位置与本次触摸两点的位置计算出用户的手势
        var leng1 =Mathf.Sqrt((oP1.x-oP2.x)*(oP1.x-oP2.x)+(oP1.y-oP2.y)*(oP1.y-oP2.y));
        var leng2 =Mathf.Sqrt((nP1.x-nP2.x)*(nP1.x-nP2.x)+(nP1.y-nP2.y)*(nP1.y-nP2.y));
        if(leng1<leng2)
        {
            //放大手势
            return true;
        }
        else
        {
            //缩小手势
            return false;
        }
    }

	public float GetFrontAndBack()
	{
		return m_fFrontAndBack;
	}

	public float GetLeftAndRight()
	{
		return m_fLeftAndRight;
	}

	public float GetScroll()
	{
		return m_fScroll;
	}

	public int GetLeftMouseClick()
	{
		return m_iLeftMouseClick;
	}

	public int GetRightMouseClick()
	{
		return m_iRightMouseClick;
	}

	public bool IsLeftMouseStay()
	{
		return m_bIsLeftMouseStay;
	}

	public bool IsRightMouseStay()
	{
		return m_bIsRightMouseStay;
	}

	public Vector2 GetMouseXYDelta()
	{
		return m_v2MouseXYDelta;
	}

	public void SetControlMode(ControlMode controlMode)
	{
		this.m_ClickPointList.Clear ();
		this.m_ControlMode = controlMode;
	}

	private void ControlInput()
	{
		if(Input.GetMouseButtonUp(0))
		{
			if(Input.mousePosition.x > this.m_RightViewPosX)
			{
				return;
			}

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo, 2000, LayerMask.GetMask("Water") ))
			{
				GameObject gameObj = hitInfo.collider.gameObject;
				Vector3 hitPoint = hitInfo.point;

				switch(this.m_ControlMode)
				{
				case ControlMode.OpenControl:
					break;
				case ControlMode.CircleControl:
					this.m_ClickPointList.Add (new Vector2 (hitPoint.x, hitPoint.z));
					break;
				default:
					if ((hitPoint.x > 2 && hitPoint.x < 46) && (hitPoint.z > 1 && hitPoint.z < 14)) {
						this.m_ClickPointList.Add (new Vector2 (hitPoint.x, hitPoint.z));
					}
					break;
				}
			}
		}

		switch(this.m_ControlMode)
		{
		case ControlMode.OpenControl:
			break;
		case ControlMode.PointControl:
			if(this.m_ClickPointList.Count == 1)
			{
				SignalManager.Instance.DispatchSignal (SignalID.ControlClick, null, this.m_ClickPointList [0]);
			}
			break;
		case ControlMode.SpecialLineControl:
			if(this.m_ClickPointList.Count == 1)
			{
				SignalManager.Instance.DispatchSignal (SignalID.ControlClick, null, this.m_ClickPointList [0].y);
			}
			break;
		case ControlMode.GenLineControl:
			if(this.m_ClickPointList.Count == 1)
			{
				DrawManager.Instance.DrawClickPoint (this.m_ClickPointList);
			}
			if(this.m_ClickPointList.Count == 2)
			{
				SignalManager.Instance.DispatchSignal (SignalID.ControlClick, null, this.m_ClickPointList);
				DrawManager.Instance.DrawClickPoint (null);
			}
			break;
		case ControlMode.MulLineControl:
			DrawManager.Instance.DrawClickPoint (this.m_ClickPointList);
			break;
		case ControlMode.CircleControl:
			if(this.m_ClickPointList.Count == 1)
			{
				float fRadius = Mathf.Sqrt (Mathf.Pow (this.m_ClickPointList [0].x - 24, 2) +
					                		Mathf.Pow (this.m_ClickPointList [0].y - 8, 2));
				if(fRadius >4 && fRadius < 6)
				{
					SignalManager.Instance.DispatchSignal (SignalID.ControlClick, null, fRadius);
				}
				else
				{
					this.m_ClickPointList.Clear ();
				}
			}
			break;
		case ControlMode.FormationControl:
			break;
		default:
			break;
		}
	}

	public void ControlOK()
	{
		if(this.m_ControlMode == ControlMode.MulLineControl || this.m_ControlMode == ControlMode.FormationControl)
		{
			SignalManager.Instance.DispatchSignal (SignalID.ControlClick, null, this.m_ClickPointList);
			DrawManager.Instance.DrawClickPoint (null);
		}
	}

	public void ControlCancle()
	{
		this.m_ClickPointList.Clear ();
		DrawManager.Instance.DrawClickPoint (null);
	}
}


public enum ControlMode
{
	OpenControl,
	PointControl,
	SpecialLineControl,
	GenLineControl,
	MulLineControl,
	CircleControl,
	FormationControl,
}