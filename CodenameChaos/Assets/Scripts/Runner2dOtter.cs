using System.Collections;
using UnityEngine;

/// <summary> Player controller for a 2D endless runner </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Runner2dOtter : MonoBehaviour
{
    [Tooltip("Tally how many obstacles have been cleared")]
    [SerializeField]
    private UnityEngine.UI.Text scoreBoard = null;

    [SerializeField]
    private Animator visual;

    [Range(1, 400)]
    public float JumpPower = 200;

    /// <summary> cache first Rigidbody2D found on this gameobject </summary>
    private Rigidbody2D rigid2d
    {
        get
        {
            if (_rigid2d == null)
            {
                _rigid2d = this.GetComponent<Rigidbody2D>();
            }
            return _rigid2d;
        }
    }
    private Rigidbody2D _rigid2d;

    /// <summary> How much the otters visual feet sink into the ground </summary>
    private float groundY = 0.14f;

    /// <summary> Tally how many obstacles have been cleared </summary>
    private int score = 0;

    void Start()
    {
        StartCoroutine(updateSometimes());
    }

    // void Update() { } // show that Update isn't yet being used

    /// <summary> Only update when not in the middle of a jumpd </summary>
    private IEnumerator updateSometimes()
    {
        while (true)
        {
            if (Input.anyKeyDown)
            {
                // jump
                visual.SetBool("IsJumping", true);
                rigid2d.AddForce(JumpPower * Vector2.up);
                yield return new WaitForSeconds(0.1f); // a few frames for y to get above ground

                // land
                yield return new WaitUntil(() => transform.position.y <= groundY);
                visual.SetBool("IsJumping", false);
            }

            yield return null;
        }
    }

    /// <summary> Add 1 to the current score </summary>
    public void IncrementScore()
    {
        scoreBoard.text = (++score).ToString();
    }
}
