public interface ILevelPlayer
{
    int Level { get; }
}
public interface ILevelEnemy
{
    void SetLevel(int level);
}
