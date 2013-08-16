namespace DevDefined.Common.Observable
{
  /*public class EventObservable<TTarget,TEventArgs> : IObservable<TEventArgs>
  {
    readonly TTarget _target;
    readonly Expression<Func<TTarget, Delegate>> _accessor;

    public EventObservable(TTarget target, Expression<Func<TTarget, Delegate>> accessor)
    {
      _target = target;
      _accessor = accessor;
    }

    public IDisposable Subscribe(IObserver<TEventArgs> observer)
    {
      throw new NotImplementedException();
    }
  }


  public sealed class EventWatcher : IDisposable
  {
    private readonly object target_;
    private readonly string eventName_;
    private readonly FieldInfo eventField_;
    private readonly Delegate listener_;
    private bool eventWasRaised_;

    private EventWatcher(object target, LambdaExpression accessor)
    {
      this.target_ = target;

      // Retrieve event definition from expression.
      var eventAccessor = accessor.Body as MemberExpression;
      this.eventField_ = eventAccessor.Member as FieldInfo;
      this.eventName_ = this.eventField_.Name;

      // Create our event listener and add it to the declaring object's event field.
      this.listener_ = CreateEventListenerDelegate(this.eventField_.FieldType);
      var currentEventList = this.eventField_.GetValue(this.target_) as Delegate;
      var newEventList = Delegate.Combine(currentEventList, this.listener_);
      this.eventField_.SetValue(this.target_, newEventList);
    }

    public void SetEventWasRaised()
    {
      this.eventWasRaised_ = true;
    }

    private Delegate CreateEventListenerDelegate(Type eventType)
    {
      // Create the event listener's body, setting the 'eventWasRaised_' field.
      var setMethod = typeof(EventWatcher).GetMethod("SetEventWasRaised");
      var body = Expression.Call(Expression.Constant(this), setMethod, Expression.NewArrayInit(typeof(object)))

      // Get the event delegate's parameters from its 'Invoke' method.
      var invokeMethod = eventType.GetMethod("Invoke");
      var parameters = invokeMethod.GetParameters()
          .Select((p) => Expression.Parameter(p.ParameterType, p.Name));

      // Create the listener.
      var listener = Expression.Lambda(eventType, body, parameters);
      return listener.Compile();
    }

    void IDisposable.Dispose()
    {
      // Remove the event listener.
      var currentEventList = this.eventField_.GetValue(this.target_) as Delegate;
      var newEventList = Delegate.Remove(currentEventList, this.listener_);
      this.eventField_.SetValue(this.target_, newEventList);

      // Ensure event was raised.
      if (!this.eventWasRaised_)
        throw new InvalidOperationException("Event was not raised: " + this.eventName_);
    }
  }*/
}