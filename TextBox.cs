namespace GUI
{
    using Microsoft.AspNetCore.Components;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// GUI wrapper for a multi-line text box, providing paragraph content support and customizable parameters.
    /// </summary>
    public class TextBox : BindableComponent<string>
    {
        /// <summary>
        /// Gets or sets the list of paragraphs displayed in the text box.
        /// </summary>
        public List<string> Paragraphs { get; set; }

        /// <summary>
        /// Gets or sets the configuration parameters for the text box appearance and behavior.
        /// </summary>
        internal TextParameters Parameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBox"/> class.
        /// </summary>
        /// <param name="paragraphs">The initial paragraph content.</param>
        /// <param name="parameters">Optional parameters to configure appearance and behavior.</param>
        /// <param name="enabled">Whether the text box is enabled for editing.</param>
        /// <param name="hidden">Whether the text box is hidden in the UI.</param>
        /// <param name="boundColumn">Optional bound data column.</param>
        /// <param name="parent">Optional parent component.</param>
        public TextBox(List<string> paragraphs, TextParameters? parameters = null, IColumn? boundColumn = null, bool enabled = true, bool hidden = false, IComponent? parent = null) : base(string.Join(Environment.NewLine, paragraphs), enabled, hidden, default, boundColumn, parent)
        {
            Paragraphs = paragraphs;
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
        /// Renders the TextBox as a <see cref="RenderFragment"/>.
        /// </summary>
        /// <param name="focus">The currently focused component.</param>
        /// <returns>A <see cref="RenderFragment"/> representing the rendered TextBox component.</returns>
        internal override RenderFragment Render(Component focus)
        {
            throw new NotImplementedException();

            Dictionary<string, object> parameters = ExtractParameters(Parameters);

            return CreateRenderFragment(GuiComponentType, parameters);
        }
    }
}