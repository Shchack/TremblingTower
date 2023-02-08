namespace EG.Tower.Game
{
    public class PlayerSession
    {
        public HeroModel HeroModel { get; private set; }

        public void SetHeroModel(HeroModel heroModel)
        {
            HeroModel = heroModel;
        }
    }
}