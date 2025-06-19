namespace GUI
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Rendering;

    using System.Reflection;

    /// <summary>
    /// Represents a dialog box component.
    /// </summary>
    public class DialogBox : CreatorComponent
    {
        /// <summary>
        /// Gets or sets the parameters used to configure the button's appearance and behavior.
        /// </summary>
        internal DialogParameters Parameters { get; set; }

        public override bool Parentable => true;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogBox"/> class with a title, optional content, parameters, and state settings.
        /// </summary>
        /// <param name="title">The title displayed in the dialog box header.</param>
        /// <param name="content">An optional list of paragraph strings to display in the dialog body.</param>
        /// <param name="parameters">Optional dialog parameters to configure appearance and behavior. If null, default parameters will be used.</param>
        /// <param name="enabled">Determines whether the dialog box is interactive (default: true).</param>
        /// <param name="hidden">Determines whether the dialog box is hidden from view (default: false).</param>
        /// <param name="parent">Optional reference to the parent component in the component hierarchy.</param>
        public DialogBox(string title, List<string>? content = null, DialogParameters? parameters = null, bool enabled = true, bool hidden = false, IComponent? parent = null) : base(enabled, hidden, parent)
        {
            Parameters = parameters ?? new DialogParameters();
            Parameters.Title = title;
            Enabled = enabled;
            Hidden = hidden;
            Parent = parent;
        }

        /// <summary>
        /// Gets the component type associated with this GUI class.
        /// </summary>
        protected override Type GuiComponentType
        {
            get { return typeof(Components.Dialog); }
        }

        /// <summary>
        /// Renders the DialogBox as a <see cref="RenderFragment"/>.
        /// </summary>
        /// <param name="focus">The currently focused component.</param>
        /// <returns>A <see cref="RenderFragment"/> representing the rendered DialogBox component.</returns>
        internal override RenderFragment Render(Component focus)
        {
            Dictionary<string, object> parameters = ExtractParameters(Parameters);

            if (ChildComponents.Count > 0)
            {
                Console.WriteLine($"Child count: {ChildComponents.Count}");

                RenderFragment childContent = CreateChildComponents(ChildComponents, focus);

                parameters["ChildContent"] = childContent;
            }

            return CreateRenderFragment(GuiComponentType, parameters);
        }
    }
}