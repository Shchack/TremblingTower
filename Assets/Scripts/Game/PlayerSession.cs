namespace EG.Tower.Game
{
    public class PlayerSession
    {
        public HeroModel HeroModel { get; private set; }

        public PlayerSession()
        {
            HeroModel = new HeroModel();
        }

        public void SetHeroModel(HeroCreateModel heroCreateModel)
        {
            HeroModel = new HeroModel(heroCreateModel);
        }
    }
}