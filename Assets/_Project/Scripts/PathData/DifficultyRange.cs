namespace PolSl.UrbanHealthPath.PathData
{
    /// <summary>
    /// Struct that holds information about range of exercise difficulties.
    /// </summary>
    public readonly struct DifficultyRange
    {
        public int MinDifficulty { get; }
        public int MaxDifficulty { get; }

        public DifficultyRange(int minDifficulty, int maxDifficulty)
        {
            MinDifficulty = minDifficulty;
            MaxDifficulty = maxDifficulty;
        }
    }
}