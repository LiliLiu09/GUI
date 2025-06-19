namespace GUI
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// GUI wrapper for a text input field, providing bindable string value support and customizable parameters.
    /// </summary>
    public class TextField : BindableComponent<string>
    {
        /// <summary>
        /// Gets or sets the parameters used to define the TextField's data, appearance, and behavior.
        /// </summary>
        internal TextParameters Parameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextField"/> class.
        /// </summary>
        /// <param name="defaultValue">The initial string value of the text field.</param>
        /// <param name="enabled">Whether the text field is enabled for user input.</param>
        /// <param name="hidden">Whether the text field is hidden in the UI.</param>
        /// <param name="parameters">Optional parameters to configure appearance and behavior.</param>
        /// <param name="boundColumn">Optional bound data column metadata.</param>
        /// <param name="parent">Optional parent GUI component.</param>
        public TextField(string defaultValue, TextParameters? parameters = null, IColumn? boundColumn = null, bool enabled = true, bool hidden = false, IComponent? parent = null) : base(defaultValue, enabled, hidden, default, boundColumn, parent)
        {
            Parameters = parameters ?? new TextParameters();
        }

        /// <summary>
        /// Gets the component type associated with this GUI class.
        /// </summary>
        protected override Type GuiComponentType
        {
            get { return typeof(Components.TextField); }
        }

        /// <summary>
        /// Renders the TextField as a <see cref="RenderFragment"/>.
        /// </summary>
        /// <param name="focus">The currently focused component.</param>
        /// <returns>A <see cref="RenderFragment"/> representing the rendered TextField component.</returns>
        internal override RenderFragment Render(Component focus)
        {
            Dictionary<string, object> parameters = ExtractParameters(Parameters);

            return CreateRenderFragment(GuiComponentType, parameters);
        }
    }
}
