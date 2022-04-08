using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    //  9:16 비율로 화면 고정
    void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect; // Camera의 viewport Rect 값을 가져옴
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)9 / 16); // 비율계산 (화면가로 / 화면세로) / (9 / 16 비율)
        float scalewidth = 1f / scaleheight; // 
        if (scaleheight < 1) // 9:16 비율보다 값이 작을때, 즉 화면이 날씬한 경우
            {
            rect.height = scaleheight; //레터박스를 높이를 기준으로 생성
            rect.y = (1f - scaleheight) / 2f; //중앙 정렬
        }
        else // 9:16 비율보다 값이 작을때, 즉 화면이 뚱뚱한 경우
        {
            rect.width = scalewidth; //레터박스를 폭을 기준으로 생성
            rect.x = (1f - scalewidth) / 2f;  //중앙 정렬
        }
        camera.rect = rect;
    }

}
