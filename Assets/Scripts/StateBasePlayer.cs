public abstract class StateBasePlayer : StateBase
{
    protected IPlayerMediator _mediator;
    protected bool whileTrue = true;

    protected StateBasePlayer(StateOfGame next, IPlayerMediator mediator) : base(next)
    {
        _mediator = mediator;
    }
}