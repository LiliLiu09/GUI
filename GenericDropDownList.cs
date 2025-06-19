namespace GUI
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Represents a generic dropdown list component that supports plain data types (non-record types).
    /// </summary>
    /// <typeparam name="T">The type of items contained in the dropdown list.</typeparam>
    internal class GenericDropDownList<T> : DropDownList<T>
    {
        /// <summary>
        /// Gets or sets the parameters used to configure the dropdown appearance, behavior, and data source.
        /// </summary>
        internal DropDownParameters<T> Parameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericDropDownList{T}"/> class.
        /// </summary>
        /// <param name="defaultValue">The default value that will be selected in the dropdown.</param>
        /// <param name="values">The list of selectable items in the dropdown.</param>
        /// <param name="parameters">Optional dropdown configuration parameters (label, style, etc.).</param>
        /// <param name="onChangeEvent">The callback to invoke when the selected item changes.</param>
        /// <param name="boundColumn">An optional column that this dropdown may be bound to (for dynamic data scenarios).</param>
        /// <param name="enabled">Indicates whether the dropdown is enabled or disabled.</param>
        /// <param name="hidden">Indicates whether the dropdown is hidden from the UI.</param>
        /// <param name="parent">The parent component (if any) that owns this dropdown.</param>
        public GenericDropDownList(T defaultValue, List<T> values, DropDownParameters<T>? parameters = null, EventCallback<T> onChangeEvent = default, IColumn? boundColumn = null, bool enabled = true, bool hidden = false, IComponent? parent = null) : base(defaultValue, onChangeEvent, boundColumn, enabled, hidden, parent)
        {
            Parameters = parameters ?? new DropDownParameters<T>();
            
            Parameters.Items = values;
        }

        /// <summary>
        /// Renders the dropdown list as a Blazor <see cref="RenderFragment"/>.
        /// This method injects all necessary parameters (selected value, change event, enabled state) for correct rendering and interaction.
        /// </summary>
        /// <param name="focus">The currently focused component, if any (used for keyboard navigation or focus control).</param>
        /// <returns>A <see cref="RenderFragment"/> representing the rendered dropdown component.</returns>
        internal override RenderFragment Render(Component focus)
        {
            Parameters.SelectedValue = DefaultValue?.ToString();
            
            Parameters.OnChange = OnChangeEvent;

            Dictionary<string, object> parameters = ExtractParameters(Parameters);

            return CreateRenderFragment(GuiComponentType, parameters);
        }
    }
}
