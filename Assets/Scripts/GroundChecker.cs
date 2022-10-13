using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    public bool IsInWinArea { get; private set; }

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
