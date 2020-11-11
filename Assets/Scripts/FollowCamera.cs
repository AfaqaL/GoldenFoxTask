using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FollowCamera : MonoBehaviour
{
    enum CameraState
    {
        Follow, FreeLook
    };

    [SerializeField] Transform target;
    [SerializeField] Vector2 freelookLimit;
    [SerializeField] Tilemap map;

    private CameraState followState = CameraState.Follow;
    private float cameraSpeed = 15f;
    private float edgeThickness = 5f;
    
    private Vector2 center;
    private Vector2 edges;

    [SerializeField] Camera camera;
    private float zoomVal = 0.4f;
    private float minZoom = 1f, maxZoom = 11f;
    private void Start()
    {
        center = map.localBounds.center;
        edges = map.localBounds.extents;
    }
    private void SwitchFollowState()
    {
        followState = followState == CameraState.Follow
            ? CameraState.FreeLook : CameraState.Follow;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchFollowState();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(followState == CameraState.Follow)
        {
            Vector3 playerPos = target.position;
            playerPos.z = transform.position.z;
            transform.position = playerPos;
        }
        else
        {
            MouseEdgeMovement(Input.mousePosition.x, Input.mousePosition.y);
        }


        float scrollVal = Input.GetAxis("Mouse ScrollWheel");
        if(scrollVal != 0)
        {
            
            //Debug.Log("Delta: " + Time.deltaTime + " scroll: " + scrollVal);
            float currZoom = camera.orthographicSize - (scrollVal > 0 ? zoomVal : -zoomVal);
            camera.orthographicSize = Mathf.Clamp(currZoom, minZoom, maxZoom);
        }
    }

    private void MouseEdgeMovement(float mouseX, float mouseY)
    {
        Vector3 cameraPos = transform.position;
        if(mouseX < edgeThickness)
        {
            cameraPos.x -= cameraSpeed * Time.deltaTime;
        }
        else if(mouseX > Screen.width - edgeThickness)
        {
            cameraPos.x += cameraSpeed * Time.deltaTime;
        }
        if(mouseY < edgeThickness)
        {
            cameraPos.y -= cameraSpeed * Time.deltaTime;
        }
        else if(mouseY > Screen.height- edgeThickness)
        {
            cameraPos.y += cameraSpeed * Time.deltaTime;
        }

        cameraPos.x = Mathf.Clamp(cameraPos.x, -freelookLimit.x, freelookLimit.x);
        cameraPos.y = Mathf.Clamp(cameraPos.y, -freelookLimit.y, freelookLimit.y);
        transform.position = cameraPos;
    }

    
}
