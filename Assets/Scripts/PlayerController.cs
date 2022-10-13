using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    LEFT_PLAYER = 0,
    RIGHT_PLAYER = 1,
}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerType m_PlayerType;

    [SerializeField]
    private float m_MoveSpeed;

    [SerializeField]
    private float m_JumpForce;

    [SerializeField]
    private GroundChecker m_GroundCheck;

    private Rigidbody2D m_Rigidbody2D;

    /// <summary>
    /// In seconds
    /// </summary>
    [SerializeField]
    private float m_EnabledTimerStartingValue = 30;

    public float TimerValue { get; set; }

    public bool EnableTimer { get; set; }

    public bool ActionsEnabled { get; set; }

    public bool IsInWinArea { get; private set; }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        TimerValue = m_EnabledTimerStartingValue;
        EnableTimer = false;
        ActionsEnabled = true;
    }

    private void Update()
    {
        if (EnableTimer)
        {
            TimerValue = Mathf.Max(0f, TimerValue - Time.deltaTime);
        }

        if (TimerValue == 0f)
        {
            ActionsEnabled = false;
        }

        IsInWinArea = m_GroundCheck.IsInWinArea;
    }

    public void MoveRight()
    {
        this.transform.position += this.transform.right * Time.deltaTime * m_MoveSpeed;
    }

    public void MoveLeft()
    {
        this.transform.position -= this.transform.right * Time.deltaTime * m_MoveSpeed;
    }

    public void Jump()
    {
        if (m_GroundCheck.IsGrounded)
        {
            Debug.Log("I can jump!");
            m_Rigidbody2D.AddForce(this.transform.up * m_JumpForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log("I cannot jump!");
        }
    }

    public void ToggleActionsEnabled()
    {
        ActionsEnabled = !ActionsEnabled;
    }
}
