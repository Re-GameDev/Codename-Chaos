using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

/// <summary> Obstacle for an endless runner </summary>
public class Runner2dObstacle : MonoBehaviour
{
    /// <summary> How fast all obstacles move </summary>
    private const float speed = 2;

    [SerializeField]
    [Tooltip("Obstacle respawns after it moves past this collider and respawns before this collider.")]
    private Collider2D ground = null;

    [Tooltip("After every respawn, obstacle moves closer. Respawning more often. Time is in minutes.")]
    [SerializeField]
    private AnimationCurve respawnDistance = new AnimationCurve(new Keyframe(0, 4, 0, 3), new Keyframe(1, 0));

    [Tooltip("When this chaotic otter touches this obstacle, it's game over")]
    [SerializeField]
    private Runner2dOtter otter = null;

    /// <summary> If this respawn of the obstacle has been added to the players score </summary>
    private bool isScored = false;

    /// <summary> In the Unity Editor, try to auto wire up the component if it is in an invalid state </summary>
    void OnValidate()
    {
        if (ground == null)
        {
            ground = FindObjectsOfType<Collider2D>().FirstOrDefault(c => c.name == "Ground");
        }
        if (otter == null)
        {
            otter = FindObjectsOfType<Runner2dOtter>().FirstOrDefault(c => c.name == "SpaceOtter2d");
        }
    }

    // void Start() // show that Start isn't yet being used

    void Update()
    {
        deltaXPosition(-1 * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == otter.gameObject) // player died, restart
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void deltaXPosition(float deltaX)
    {
        Vector3 currentPosition = this.transform.localPosition;
        currentPosition.x += deltaX;

        if (currentPosition.x < 0 && false == isScored)
        {
            otter.IncrementScore();
            isScored = true;
        }
        else if (currentPosition.x < ground.bounds.min.x) // obstacle has run its course, respawn it
        {
            respawn(ref currentPosition);
        }

        this.transform.localPosition = currentPosition;
    }

    private void respawn(ref Vector3 currentPosition)
    {
        isScored = false;
        currentPosition.x = ground.bounds.max.x
            + respawnDistance.Evaluate(Time.timeSinceLevelLoad / 60); // convert seconds to minutes
    }
}
