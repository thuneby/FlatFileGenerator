namespace FlatFileGenerator.DataGenerator.Business
{
    public static class AmountGenerator
    {
        private static readonly Random Random = new();

        public static int GetAmount(int start = 125, int end = 15000)
        {
            return Random.Next(start, end);
        }
    }
}
