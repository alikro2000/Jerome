using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// A reference to the script containing level info (of current & next level scenes).
    /// </summary>
    [SerializeField]
    private LevelInfo m_LevelInfo;

    /// <summary>
    /// The reference to the player in the left section of the game.
    /// </summary>
    [SerializeField]
    private PlayerController m_LeftPlayer;

    /// <summary>
    /// The reference to the timer text for the left player.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI m_LeftPlayerTimerText;

    /// <summary>
    /// The reference to the selection status indicator image for the left player.
    /// </summary>
    [SerializeField]
    private Image m_LeftPlayerSelectionImage;

    /// <summary>
    /// The reference to the win status indicator image for the left player.
    /// </summary>
    [SerializeField]
    private Image m_LeftPlayerWinningImage;

    /// <summary>
    /// The reference to the player in the right section of the game.
    /// </summary>
    [SerializeField]
    private PlayerController m_RightPlayer;

    /// <summary>
    /// The reference to the timer text for the right player.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI m_RightPlayerTimerText;

    /// <summary>
    /// The reference to the selection status indicator image for the right player.
    /// </summary>
    [SerializeField]
    private Image m_RightPlayerSelectionImage;

    /// <summary>
    /// The reference to the win status indicator image for the right player.
    /// </summary>
    [SerializeField]
    private Image m_RightPlayerWinningImage;

    /// <summary>
    /// The reference to the winning panel that is displayed when the level is won.
    /// </summary>
    [SerializeField]
    private RectTransform m_WinPanel;

    /// <summary>
    /// The reference to the losing panel that is displayed when the level is lost.
    /// </summary>
    [SerializeField]
    private RectTransform m_LosePanel;

    private void Start()
    {
        m_RightPlayer.ActionsEnabled = true;
        m_LeftPlayer.ActionsEnabled = true;
    }

    private void Update()
    {
        HandlePlayerSelection();

        HandlePlayerMovement();

        HandlePlayerJump();

        HandlePlayersTimers();

        HandleWinningAndLosing();

        HandleUI();
    }

    private void HandleWinningAndLosing()
    {
        if (m_LeftPlayer.IsInWinArea && m_RightPlayer.IsInWinArea)
        {
            m_WinPanel.gameObject.SetActive(true);
            StartCoroutine("LoadNextLevel");
        }
        else if (
            (m_LeftPlayer.TimerValue == 0 && m_RightPlayer.TimerValue == 0) ||
            (m_LeftPlayer.IsInWinArea && m_RightPlayer.TimerValue == 0) ||
            (m_RightPlayer.IsInWinArea && m_LeftPlayer.TimerValue == 0)
            )
        {
            m_LosePanel.gameObject.SetActive(true);
            StartCoroutine("ReloadCurrentLevel");
        }
    }

    private IEnumerator ReloadCurrentLevel()
    {
        Debug.Log("Reloading current level");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(m_LevelInfo.m_SceneName);
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(m_LevelInfo.m_NextLevelName);
    }

    private void HandleUI()
    {
        //Handle players' timers UI
        m_LeftPlayerTimerText.text = ((int)m_LeftPlayer.TimerValue).ToString() + "s";
        m_RightPlayerTimerText.text = ((int)m_RightPlayer.TimerValue).ToString() + "s";

        //Handle players' selection status on UI
        m_LeftPlayerSelectionImage.color = new Color(207 / 255f, 70 / 255f, 63 / 255f, m_LeftPlayer.ActionsEnabled ? 1f : 0.5f);
        m_RightPlayerSelectionImage.color = new Color(51 / 255f, 160 / 255f, 212 / 255f, m_RightPlayer.ActionsEnabled ? 1f : 0.5f);

        //Handle players' winning status on UI
        m_LeftPlayerWinningImage.color = new Color(63 / 255f, 207 / 255f, 63 / 255f, m_LeftPlayer.IsInWinArea ? 1f : 0f);
        m_RightPlayerWinningImage.color = new Color(63 / 255f, 207 / 255f, 63 / 255f, m_RightPlayer.IsInWinArea ? 1f : 0f);
    }

    private void HandlePlayersTimers()
    {
        //Left player enabaled, right player disabled
        if (m_LeftPlayer.ActionsEnabled && !m_RightPlayer.ActionsEnabled)
        {
            m_LeftPlayer.EnableTimer = true;
            m_RightPlayer.EnableTimer = false;
        }
        //Right player enabaled, left player disabled
        else if (!m_LeftPlayer.ActionsEnabled && m_RightPlayer.ActionsEnabled)
        {
            m_LeftPlayer.EnableTimer = false;
            m_RightPlayer.EnableTimer = true;
        }
        //Both players enabled or disabled
        else
        {
            m_LeftPlayer.EnableTimer = false;
            m_RightPlayer.EnableTimer = false;
        }
    }

    private void HandlePlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (m_LeftPlayer.ActionsEnabled) { m_LeftPlayer.Jump(); }
            if (m_RightPlayer.ActionsEnabled) { m_RightPlayer.Jump(); }
        }
    }

    private void HandlePlayerMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (m_LeftPlayer.ActionsEnabled) { m_LeftPlayer.MoveLeft(); }
            if (m_RightPlayer.ActionsEnabled) { m_RightPlayer.MoveLeft(); }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (m_LeftPlayer.ActionsEnabled) { m_LeftPlayer.MoveRight(); }
            if (m_RightPlayer.ActionsEnabled) { m_RightPlayer.MoveRight(); }
        }
    }

    private void HandlePlayerSelection()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !m_LeftPlayer.IsInWinArea)
        {
            m_LeftPlayer.ToggleActionsEnabled();
            Debug.Log($"Left player isenabled:{m_LeftPlayer.ActionsEnabled}!");
        }
        if (Input.GetKeyDown(KeyCode.E) && !m_RightPlayer.IsInWinArea)
        {
            m_RightPlayer.ToggleActionsEnabled();
            Debug.Log($"Right player isenabled:{m_LeftPlayer.ActionsEnabled}!");
        }

        m_LeftPlayer.ActionsEnabled = m_LeftPlayer.IsInWinArea ? false : m_LeftPlayer.ActionsEnabled;
        m_RightPlayer.ActionsEnabled = m_RightPlayer.IsInWinArea ? false : m_RightPlayer.ActionsEnabled;
    }
}
