using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dedicated script for checking whether a game object is on the ground or not.
/// Note that this is also capable of telling whether a game object is in a WinArea or not.
/// Tip: Attach <see cref="GroundChecker"/> to a new game object with a 2D Collider and make it a child of the game object you wish to have a ground check for.
/// </summary>
public class GroundChecker : MonoBehaviour
{
    /// <summary>
    /// Tells whether this game object is on the ground or not.
    /// </summary>
    public bool IsGrounded { get; private set; }

    /// <summary>
    /// Tells whether this game object is in a WinArea or not.
    /// </summary>
    public bool IsInWinArea { get; private set; }

    /// <summary>
    /// Recalculate properties when this game object enters collision with another 2D object.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
        {
            IsGrounded = true;
        }

        if (collision.tag == "WinArea")
        {
            IsInWinArea = true;
        }
    }

    /// <summary>
    /// Recalculate properties when this game object exits collision with another 2D object.
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
        {
            IsGrounded = false;
        }

        if (collision.tag == "WinArea")
        {
            IsInWinArea = false;
        }
    }
}
