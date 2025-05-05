using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{

    private void Start()
    {
        Cursor.visible = true;
    }


    private void Update()
    {
        // 마우스 위치를 지역 변수에 할당
        Vector3 inputPos = Input.mousePosition;
        // 스크린 좌표를 월드 좌표로 변환
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(inputPos);
        // 2D니까 Z좌표는 무시하거나 0으로 설정
        mousePos.z = 0f;
        // 마우스 따라가기
        transform.position = mousePos;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Clicked on : "+hit.collider.name);
            }
            else
            {
                Debug.Log("Nothing was clicked");
            }
        }
    }
}
