using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dev note: I thought there might be a way to have multiple players where each of them had different controls,
/// hence why such an enum exists in the first place.
/// </summary>
public enum PlayerType
{
    LEFT_PLAYER = 0,
    RIGHT_PLAYER = 1,
}

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Set whether this player is the one on the right or left of the screen.
    /// </summary>
    [SerializeField]
    private PlayerType m_PlayerType;

    /// <summary>
    /// Controls the left/right movement speed of this player.
    /// </summary>
    [SerializeField]
    private float m_MoveSpeed;

    /// <summary>
    /// Controls the jump force of this player.
    /// </summary>
    [SerializeField]
    private float m_JumpForce;

    /// <summary>
    /// Corresponding <see cref="GroundChecker"/> for this player.
    /// Must assign through inspector.
    /// </summary>
    [SerializeField]
    private GroundChecker m_GroundCheck;

    /// <summary>
    /// This player's <see cref="Rigidbody2D"/>. Is automatically set in <see cref="PlayerController.Awake"/>.
    /// </summary>
    private Rigidbody2D m_Rigidbody2D;

    /// <summary>
    /// Controls how long, in seconds, this player is allowed to move by its own at the beginning of a level.
    /// </summary>
    [SerializeField]
    private float m_EnabledTimerStartingValue = 30;

    /// <summary>
    /// The amount of time this player is allowed to move on its own.
    /// </summary>
    public float TimerValue { get; set; }

    /// <summary>
    /// Controls whether this player's <see cref="TimerValue"/> should start or not.
    /// </summary>
    public bool EnableTimer { get; set; }

    /// <summary>
    /// Player actions are moving (<see cref="PlayerController.MoveLeft"/> and <see cref="PlayerController.MoveRight"/>) and jumping (<see cref="PlayerController.Jump"/>).
    /// </summary>
    public bool ActionsEnabled { get; set; }

    /// <summary>
    /// Use this to see if this player is in a WinArea or not.
    /// </summary>
    public bool IsInWinArea { get; private set; }

    /// <summary>
    /// Handle initialization
    /// </summary>
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        TimerValue = m_EnabledTimerStartingValue;
        EnableTimer = false;
        ActionsEnabled = true;
    }

    /// <summary>
    /// Handles this player's logic (including timer calculations).
    /// </summary>
    private void Update()
    {
        // Handle timer logic
        if (EnableTimer)
        {
            TimerValue = Mathf.Max(0f, TimerValue - Time.deltaTime);
        }

        // If the player has ran out of time for moving, it shouldn't be able to make moves.
        if (TimerValue == 0f)
        {
            ActionsEnabled = false;
        }

        // Check being in win area through the corresponding GroundChecker attached to this player.
        IsInWinArea = m_GroundCheck.IsInWinArea;
    }

    /// <summary>
    /// Move this player <see cref="PlayerController"> to right.
    /// </summary>
    public void MoveRight()
    {
        this.transform.position += this.transform.right * Time.deltaTime * m_MoveSpeed;
    }

    /// <summary>
    /// Move this player <see cref="PlayerController"> to left.
    /// </summary>
    public void MoveLeft()
    {
        this.transform.position -= this.transform.right * Time.deltaTime * m_MoveSpeed;
    }

    /// <summary>
    /// Make this player <see cref="PlayerController"> jump only once (double jump, or more, is not allowed).
    /// </summary>
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

    /// <summary>
    /// Toggle actions of this player <see cref="PlayerController"/> enabled or disabled.
    /// Player actions are moving (<see cref="PlayerController.MoveLeft"/> and <see cref="PlayerController.MoveRight"/>) and jumping (<see cref="PlayerController.Jump"/>).
    /// </summary>
    public void ToggleActionsEnabled()
    {
        ActionsEnabled = !ActionsEnabled;
    }
}
