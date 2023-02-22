using EG.Tower.Game;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{
    public class SequencerCommandShowImage : SequencerCommand
    {
        private void Awake()
        {
            var imageName = GetParameter(0);
            GameHub.One.DialogueSystem.ShowImage(imageName);
            Stop();
        }
    }
}
