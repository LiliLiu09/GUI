namespace GUI
{
    using Microsoft.AspNetCore.Components;


    /// <summary>
    /// Represents a base class for dropdown list components.
    /// </summary>
    /// <typeparam name="T">The type of value used in the dropdown list.</typeparam>
    public abstract class DropDownList<T> : BindableComponent<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownList{T}"/> class.
        /// </summary>
        /// <param name="defaultValue">The default selected value.</param>
        /// <param name="enabled">True if the component should be enabled.</param>
        /// <param name="hidden">True if the component should be hidden.</param>
        /// <param name="onChangeEvent">The event triggered when the value changes.</param>
        /// <param name="boundColumn">Optional column to bind the value to.</param>
        /// <param name="parent">Parent component if nested.</param>
        protected DropDownList(T defaultValue, EventCallback<T> onChangeEvent = default, IColumn? boundColumn = null, bool enabled = true, bool hidden = false, IComponent? parent = null) : base(defaultValue, enabled, hidden, onChangeEvent, boundColumn, parent)
        {
        }

        /// <summary>
        /// Gets the component type associated with this GUI class.
        /// </summary>
        protected override Type GuiComponentType
        {
            get { return typeof(Components.DropDownList<T>); }
        }
    }
}