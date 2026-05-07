using Helteix.Tools.Phases.Listeners;
using TMPro;
using UnityEngine;

public class TimerUI : MonoPhaseListener<ManagementPhase>
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private int timerDuration;
    [SerializeField] private bool isTimerActive;
        
    private ManagementPhase currentPhase;
        
    protected override void OnPhaseBegin(ManagementPhase phase)
    {
        currentPhase = phase;
        if (isTimerActive)
            StartCountdown();
            
            
        base.OnPhaseBegin(phase);
    }

    protected override void OnPhaseEnd(ManagementPhase phase)
    {
        currentPhase = null;
        timerText.text = string.Empty;
        timerDuration = 0;
            
        base.OnPhaseEnd(phase);
    }

    private async void StartCountdown()
    {
        int remaining = timerDuration;
        for (int i = 0; i < timerDuration; i++)
        {
            remaining--;
            var minutes = remaining / 60;
            var seconds = remaining % 60;
            timerText.text = $"{minutes:00} : {seconds:00}";
            await Awaitable.WaitForSecondsAsync(1);
        }
            
        if (remaining <= 0)
            currentPhase.SetResult(true);
    }
}