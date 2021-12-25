using System;
using System.Collections;
using UnityEngine;

namespace Assets.PixelCrew.Utils.Disposables
{
    public class ActionDisposable : IDisposable
    {
        private Action _onDispose;
        public ActionDisposable(Action onDispose)
        {
            _onDispose = onDispose;
        }
        
        public void Dispose()
        {
            _onDispose?.Invoke();
            _onDispose = null;
        }
    }
}