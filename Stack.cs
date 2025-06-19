namespace GUI
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Represents a GUI container component that stacks its child components vertically.
    /// </summary>
    public class Stack : CreatorComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Stack"/> class.
        /// </summary>
        /// <param name="enabled">Determines if the stack is enabled.</param>
        /// <param name="hidden">Determines if the stack is hidden.</param>
        /// <param name="parent">Optional parent component.</param>
        public Stack(bool enabled, bool hidden, IComponent? parent = null) : base(enabled, hidden, parent)
        {
        }

        public override bool Parentable => true;

        /// <summary>
        /// Gets the component type associated with this GUI class.
        /// </summary>
        protected override Type GuiComponentType
        {
            get { return typeof(Components.Stack); }
        }

        /// <summary>
        /// Renders the Stack as a <see cref="RenderFragment"/>.
        /// </summary>
        /// <param name="focus">The currently focused component.</param>
        /// <returns>A <see cref="RenderFragment"/> representing the rendered Stack component.</returns>
        internal override RenderFragment Render(Component focus)
        {
            RenderFragment childContent = null;

            if (ChildComponents.Count > 0)
            {
                childContent = CreateChildComponents(ChildComponents, focus);
            }

            Dictionary<string, object> parameters = new()
            {
                { "ChildContent", childContent }
            };

            return CreateRenderFragment(GuiComponentType, parameters);
        }
    }
}