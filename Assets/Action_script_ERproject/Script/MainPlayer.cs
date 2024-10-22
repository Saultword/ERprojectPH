using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class MainPlayer : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Vector2 moveAction;
    [SerializeField] private float speed = 1;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float minHeight = 0;
    [SerializeField] private float maxHeight = float.PositiveInfinity;
    [SerializeField] private CharacterController characterController;
    void Start()
    {
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
    }

    void Update()
    {
        HandleHeight();
        Move();
    }
    private void Move()
    {
        if (moveAction.axis.magnitude > 0.1f)
        {
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(moveAction.axis.x, 0, moveAction.axis.y));
            characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, gravity, 0) * Time.deltaTime);
        }

    }
    private void HandleHeight()
    {
        float headHeight = Mathf.Clamp(Player.instance.hmdTransform.position.y, minHeight, maxHeight);
        characterController.height = headHeight;

        Vector3 newCenter = Player.instance.transform.InverseTransformPoint(Player.instance.hmdTransform.position);
        newCenter.y = characterController.height / 2 + characterController.skinWidth;
        characterController.center = newCenter;
    }
}
