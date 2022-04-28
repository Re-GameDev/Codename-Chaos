using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LaptopCameraHandler : MonoBehaviour
{
    public bool CameraIsOn = false;
    public Camera Camera;
    public MeshRenderer ScreenMesh;
    public RenderTexture OriginalRenderTexture;
    public Transform ScreenTransform;
    public Transform LidTransform;
    public Transform LidCollisionTransform;

    private RenderTexture cameraTexture;
    private Quaternion originalLidRotation;
    private Transform lidColRelativeTransform;
    private BasicPlayer player;
    private Transform playerTransform;
    private float openProgress = 0.0f;
    
    void Start()
    {
        Assert.IsNotNull(Camera);
        Assert.IsNotNull(ScreenMesh);
        Assert.IsNotNull(OriginalRenderTexture);
        Assert.IsNotNull(ScreenTransform);
        Assert.IsNotNull(LidTransform);
        Assert.IsNotNull(LidCollisionTransform);
        if (CameraIsOn)
        {
            this.cameraTexture = new RenderTexture(OriginalRenderTexture);
            Camera.targetTexture = this.cameraTexture;
            ScreenMesh.material.mainTexture = this.cameraTexture;
        }
        else
        {
            Camera.enabled = false;
        }
        originalLidRotation = ScreenTransform.localRotation;
        //lidColRelativeTransform = new Transform();
        //lidColRelativeTransform.position = LidCollisionTransform.position - LidTransform.position;
        //lidColRelativeTransform.rotation = Quaternion. LidCollisionTransform.rotation. - LidTransform.rotation;
    }

    private void Awake()
    {
        player = Object.FindObjectOfType<BasicPlayer>();
        if (player != null)
        {
            playerTransform = player.gameObject.GetComponent<Transform>();
            Assert.IsNotNull(playerTransform);
        }
        else
        {
            Debug.LogError("LaptopCameraHandler couldn't find player instance!");
        }
    }

    private void FixedUpdate()
    {
        bool playerIsClose = false;
        if (playerTransform != null)
        {
            if (Vector3.Distance(playerTransform.position, transform.position) < 4)
            {
                playerIsClose = true;
            }
        }
        if (Input.GetKey(KeyCode.Return))
        {
            playerIsClose = true;
        }

        if (playerIsClose)
        {
            if (openProgress < 1.0f)
            {
                openProgress += 0.06f;
                if (openProgress >= 1.0f) { openProgress = 1.0f; }
            }
        }
        else
        {
            if (openProgress > 0.0f)
            {
                openProgress -= 0.06f;
                if (openProgress <= 0.0f) { openProgress = 0.0f; }
            }
        }

        // Time.unscaledTime
        Vector3 screenRotation = new Vector3(0, 270 + (openProgress * 90), 0);
        ScreenTransform.localRotation = originalLidRotation * Quaternion.Euler(screenRotation);
        LidTransform.localRotation = originalLidRotation * Quaternion.Euler(screenRotation);
        //LidCollisionTransform.rotation = LidTransform.rotation;
        //LidCollisionTransform.position = LidTransform.position + lidColRelativeTransform.position;
        LidCollisionTransform.position = LidTransform.position;
        LidCollisionTransform.rotation = LidTransform.rotation;
    }
}
