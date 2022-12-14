using Code.GUI.MVC.View;
using Code.GUI.MVC.Model;

namespace Code.GUI.MVC.Controller
{
    /// <summary>
    /// Основной интерфейс по работе с коммуникаций объектов View и Model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="M"></typeparam>
    public interface IGUIController<T, M> : IController
        where T : class, IView
        where M : class, IModel, new()
    {
        T LinkedView { get; }
        M Model { get; }
        void UpdateView();
    }
}
