namespace Items
{
    [System.Serializable]
    public class GoldData
    {
        public int currentGold;

        public GoldData(int initialGold = 0)
        {
            currentGold = initialGold;
        }
    }
}
