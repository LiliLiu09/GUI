namespace GUI
{
    using Microsoft.AspNetCore.Components;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Compartmentalisation element for containing related elements.
    /// </summary>
    public class Tab : Component
    {
        /// <summary>
        /// Gets or sets the parameters used to define the Tab's data, appearance, and behavior.
        /// </summary>
        internal TabParameters Parameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tab"/> class.
        /// </summary>
        /// <param name="items">The tab headers and content as tuples.</param>
        /// <param name="parameters">Optional parameters for customizing the tab behavior and appearance.</param>
        /// <param name="enabled">Whether the tab component is interactive.</param>
        /// <param name="hidden">Whether the tab component is hidden.</param>
        /// <param name="parent">Optional parent component to attach this tab to.</param>
        public Tab(IEnumerable<(string Header, string Content)> items, TabParameters? parameters = null, bool enabled = true, bool hidden = false, IComponent? parent = null) : base(enabled, hidden, parent)
        {
            Parameters = parameters ?? new TabParameters();

            Parameters.TabItems = items;
        }

        /// <summary>
        /// Gets the component type associated with this GUI class.
        /// </summary>
        protected override Type GuiComponentType
        {
            get { return typeof(Components.Tabs); }
        }

        /// <summary>
        /// Renders the Tab as a <see cref="RenderFragment"/>.
        /// </summary>
        /// <param name="focus">The currently focused component.</param>
        /// <returns>A <see cref="RenderFragment"/> representing the rendered Tab component.</returns>
        internal override RenderFragment Render(Component focus)
        {
            RenderFragment tabChildContent = GetTabsList(Parameters.TabItems);

            Dictionary<string, object> parameters = ExtractParameters(Parameters);

            parameters["TabsList"] = tabChildContent;

            return CreateRenderFragment(GuiComponentType, parameters);
        }

        /// <summary>
        /// Generates a <see cref="RenderFragment"/> containing all tab items.
        /// </summary>
        /// <param name="items">A collection of tab headers and their corresponding content.</param>
        /// <returns>A <see cref="RenderFragment"/> representing the list of tabs.</returns>
        private RenderFragment GetTabsList(IEnumerable<(string Header, string Content)> items)
        {
            return tabsBuilder =>
            {
                int seq = 0;
                int tabIndex = 0;

                foreach (var (header, content) in items)
                {
                    tabsBuilder.OpenComponent(seq++, typeof(Components.TabsItem));
                    tabsBuilder.AddAttribute(seq++, "Index", tabIndex);
                    tabsBuilder.AddAttribute(seq++, "Text", header);
                    tabsBuilder.AddAttribute(seq++, "ChildContent", (RenderFragment)(childBuilder =>
                    {
                        childBuilder.AddContent(seq++, content);
                    }));
                    tabsBuilder.CloseComponent();
                    tabIndex++;
                }
            };
        }
    }
}
