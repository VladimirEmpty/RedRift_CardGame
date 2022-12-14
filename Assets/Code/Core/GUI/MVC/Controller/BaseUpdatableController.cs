using Code.GUI.MVC.View;
using Code.GUI.MVC.Model;

namespace Code.GUI.MVC.Controller
{
    public abstract class BaseUpdatableController<T, M> : BaseController<T, M>, IUpdatableController
        where T : class, IView
        where M : class, IModel, new()
    {
        public abstract string Tag { get; }

        public void UpdateController(string updateTag)
        {
            if (!string.IsNullOrEmpty(updateTag) & !Tag.Equals(updateTag))
                return;

            UpdateView();
        }
    }
}
