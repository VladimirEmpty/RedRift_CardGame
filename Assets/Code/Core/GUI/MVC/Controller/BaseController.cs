using Code.GUI.MVC.View;
using Code.GUI.MVC.Model;
using Code.Factory;

namespace Code.GUI.MVC.Controller
{
    public abstract class BaseController<T, M> : IGUIController<T, M>
        where T : class, IView
        where M : class, IModel, new()
    {
        public BaseController()
        {
            using(var factory = new NativeObjectFactory<M>())
            {
                Model = factory.Create();
            }
        }

        public T LinkedView { get; private set; }
        public M Model { get; }

        void IController.AddView<V>(V view)
        {
            if (LinkedView != null) return;
            LinkedView = view as T;

            OnShow();
        }

        public abstract void UpdateView();

        protected abstract void OnShow();
    }
}
