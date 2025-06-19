namespace GUI
{
    using Microsoft.AspNetCore.Components;


    /// <summary>
    /// Represents a dropdown list component that supports items of a record type.
    /// </summary>
    /// <typeparam name="T">The type of records contained in the dropdown list. Must inherit from <see cref="Record"/>.</typeparam>
    internal class RecordDropDownList<T> : DropDownList<T> where T : Record
    {
        /// <summary>
        /// Gets or sets the parameters used to configure the dropdown appearance, behavior, and data source.
        /// </summary>
        internal DropDownParameters<T> Parameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordDropDownList{T}"/> class.
        /// </summary>
        /// <param name="defaultValue">The default selected record in the dropdown.</param>
        /// <param name="values">The list of records to populate the dropdown options.</param>
        /// <param name="parameters">Optional dropdown configuration parameters (label, style, etc.).</param>
        /// <param name="onChangeEvent">Callback invoked when the selected record changes.</param>
        /// <param name="boundColumn">Optional column this dropdown is bound to for dynamic record mapping.</param>
        /// <param name="enabled">Indicates whether the dropdown is enabled or disabled.</param>
        /// <param name="hidden">Indicates whether the dropdown is hidden from the UI.</param>
        /// <param name="parent">The parent GUI component that contains this dropdown.</param>
        public RecordDropDownList(T defaultValue, RecordList<T> values, DropDownParameters<T>? parameters = null, EventCallback<T> onChangeEvent = default, IColumn? boundColumn = null, bool enabled = true, bool hidden = false, IComponent? parent = null) : base(defaultValue, onChangeEvent, boundColumn, enabled, hidden, parent)
        {
            Parameters = parameters ?? new DropDownParameters<T>();

            // TODO: Convert RecordList to List<T> or enhance component to directly support RecordList
            // Parameters.Items = values;
        }

        /// <summary>
        /// Renders the record-based dropdown list as a Blazor <see cref="RenderFragment"/>.
        /// Injects necessary parameters including selected value and change callback.
        /// </summary>
        /// <param name="focus">The currently focused GUI component, if any.</param>
        /// <returns>A <see cref="RenderFragment"/> that represents the rendered dropdown.</returns>
        internal override RenderFragment Render(Component focus)
        {
            Parameters.SelectedValue = DefaultValue?.ToString();
            
            Parameters.OnChange = OnChangeEvent;

            Dictionary<string, object> parameters = ExtractParameters(Parameters);

            return CreateRenderFragment(GuiComponentType, parameters);
        }
    }
}
