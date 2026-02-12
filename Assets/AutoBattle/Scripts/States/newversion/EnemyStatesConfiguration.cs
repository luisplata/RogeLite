using System.Collections.Generic;

namespace Bellseboss
{
    public class EnemyStatesConfiguration
    {
        private int InitialState;

        public const int BeggingState = 0;
        public const int TurnState = 1;
        public const int CheckState = 2;
        public const int FinalState = 3;
        public const int BreakingState = 4;

        private readonly Dictionary<int, IBattleState> _states;

        public EnemyStatesConfiguration()
        {
            _states = new Dictionary<int, IBattleState>();
        }

        public void AddInitialState(int id, IBattleState state)
        {
            _states.Add(id, state);
            InitialState = id;
        }

        public void AddState(int id, IBattleState state)
        {
            _states.Add(id, state);
        }

        public IBattleState GetState(int stateId)
        {
            return _states[stateId];
        }

        public IBattleState GetInitialState()
        {
            return GetState(InitialState);
        }
    }
}