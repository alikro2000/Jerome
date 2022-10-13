using UnityEngine;

/// <summary>
/// A reference for holding references for current and next level scenes.
/// </summary>
public class LevelInfo : MonoBehaviour
{
    /// <summary>
    /// The name of the scene currently being played.
    /// </summary>
    public string m_SceneName;

    /// <summary>
    /// The name of the scene of the next level.
    /// </summary>
    public string m_NextLevelName;
}
