using Code.Runtime.UI.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Windows.Api
{
  public abstract class BaseWindow : MonoBehaviour
  {
    private readonly UniTaskCompletionSource _showTaskCompletionSource = new();
    private readonly UniTaskCompletionSource _hideTaskCompletionSource = new();

    private bool _subscribedNavigation;
    
    public Fader Fader;
    
    public WindowTypeId Id { get; protected set; }

    protected bool HasFader => Fader != null;
    public UniTask ShowTask => _showTaskCompletionSource.Task;
    public UniTask HideTask => _hideTaskCompletionSource.Task;

    private void Awake()
    {
      OnAwake();
      
      if(HasFader)
        Fader.BecameVisible += OnVisibleInternal;
    }

    private void Start()
    {
      Initialize();
      SubscribeUpdates();
      if(!HasFader)
        OnVisibleInternal();
    }

    private void Update() =>
      OnUpdate();

    private void OnDestroy()
    {
      if(!HasFader || _subscribedNavigation)
        OnHideInternal();
      
      UnsubscribeUpdates();
      Cleanup();
      
      if(HasFader)
      {
        Fader.BecameVisible -= OnVisibleInternal;
        Fader.HideTriggered -= OnHideInternal;
      }
    }

    public async UniTask Show()
    {
      if(Fader == null)
        return;
      
      Fader.HideImmediately();
      await Fader.ShowAsync();
      _showTaskCompletionSource.TrySetResult();
    }

    public async UniTask Hide()
    {
      if (Fader == null) 
        return;
      
      await Fader.HideAsync();
      _hideTaskCompletionSource.TrySetResult();
    }

    protected virtual void OnAwake() { }
    protected virtual void Initialize() { }
    protected virtual void OnVisible() { }
    protected virtual void OnUpdate() { }
    protected virtual void SubscribeUpdates() { }
    protected virtual void UnsubscribeUpdates() { }
    protected virtual void SubscribeNavigation() { }
    protected virtual void UnsubscribeNavigation() { }
    protected virtual void Cleanup() { }

    private void OnVisibleInternal()
    {
      if(HasFader)
        Fader.HideTriggered += OnHideInternal;
      SubscribeNavigationInternal();
      OnVisible();
    }

    private void OnHideInternal() =>
      UnsubscribeNavigationInternal();

    private void SubscribeNavigationInternal()
    {
      _subscribedNavigation = true;
      SubscribeNavigation();
    }

    private void UnsubscribeNavigationInternal()
    {
      _subscribedNavigation = false;
      UnsubscribeNavigation();
    }
  }
}