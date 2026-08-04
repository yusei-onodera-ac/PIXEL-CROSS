using System;
using PixelCross.Data;

namespace PixelCross.Tutorial
{
    public class TutorialManager
    {
        public const int CompletionRewardGachaTickets = 1;
        public const int CompletionRewardCurrency = 500;

        public TutorialStep CurrentStep { get; private set; } = TutorialStep.NotStarted;

        public event Action<TutorialStep> OnStepChanged;
        public event Action OnTutorialCompleted;

        // Restores step without firing events, for use when reconstructing
        // state from a save file.
        public void LoadState(TutorialStep step)
        {
            CurrentStep = step;
        }

        public void AdvanceStep()
        {
            if (CurrentStep == TutorialStep.Completed) return;

            CurrentStep = CurrentStep switch
            {
                TutorialStep.NotStarted => TutorialStep.ManagerAndTeamSetup,
                TutorialStep.ManagerAndTeamSetup => TutorialStep.PracticeAndStaminaIntro,
                TutorialStep.PracticeAndStaminaIntro => TutorialStep.TacticsAndFreshmanCup,
                TutorialStep.TacticsAndFreshmanCup => TutorialStep.SummerCampScoutIntro,
                TutorialStep.SummerCampScoutIntro => TutorialStep.GachaAndItemIntro,
                TutorialStep.GachaAndItemIntro => TutorialStep.Completed,
                _ => TutorialStep.Completed
            };

            OnStepChanged?.Invoke(CurrentStep);

            if (CurrentStep == TutorialStep.Completed)
            {
                OnTutorialCompleted?.Invoke();
            }
        }

        public void GrantCompletionReward(TeamData team)
        {
            team.GachaTickets += CompletionRewardGachaTickets;
            team.BasicCurrency += CompletionRewardCurrency;
        }
    }
}
