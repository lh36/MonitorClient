using UnityEngine;
using System.Collections;

public class InputManager : SingletonUnity<InputManager>
{
	private float m_fFrontAndBack = 0;          //前后
	private float m_fLeftAndRight = 0;          //左右
	private float m_fScroll = 0;                //滚轮
	private int m_iLeftMouseClick = 0;          //鼠标左键点击
	private int m_iRightMouseClick = 0;         //鼠标右键点击
	private bool m_bIsLeftMouseStay = false;      //鼠标左键按住
	private bool m_bIsRightMouseStay = false;     //鼠标右键按住



	void Update () 
	{
		if(GlobalManager.Instance.IsGameRunning)
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
		return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
	}

    void FixedUpdate()
    {
        if(GlobalManager.Instance.IsGameRunning)
        {
            //input something
        }


    }

}

