using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    float time;
    private int currentTime = 0;

    public static bool IsOver = false;

    public UnityEvent OnTimeOver;

    private void Start()
    {
      
        StartTimer();
    }

    private void Update()
    {
        // 타이머가 끝나지 않았다면
        if (Timer.IsOver == false && time > 0)
        {
            time -= Time.deltaTime;
            currentTime = (int)time;
            if (currentTime <= 0)
            {
                currentTime = 0;
                StopTimer();
                TimeOver();
            }
        }
    }

    // 타이머를 멈추는 메소드
    public void StopTimer()
    {
        Timer.IsOver = true;
    }

    // 타이머를 시작하는 메소드
    public void StartTimer()
    {
        Timer.IsOver = false;
    }

    public void SetTimer(int sec)
    {
        time = sec;
    }

    // 타이머가 다 됐을 때 발동하는 메소드
    private void TimeOver()
    {
        OnTimeOver.Invoke();
    }
}
