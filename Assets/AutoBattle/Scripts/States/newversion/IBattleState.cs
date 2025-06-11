using System.Collections;

namespace Bellseboss
{
    public interface IBattleState
    {
        IEnumerator DoEnter();
        IEnumerator DoAction();
        IEnumerator DoExit();
        int NextStateId { get; }
    }
}