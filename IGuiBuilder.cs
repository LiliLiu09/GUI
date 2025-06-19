namespace GUI
{
    using Microsoft.AspNetCore.Components;

    // TODO: Move this interface to reference library.

    /// <summary>
    /// Interface for GuiBuilder service.
    /// </summary>
    public interface IGuiBuilder : IComponentCreator
    {
        /// <summary>
        /// Root of the GUI stack.
        /// </summary>
        public Component Component
        {
            get;
        }

        /// <summary>
        /// Renders the GUI Stack into a render fragment that may be inserted into a Razor Page.
        /// </summary>
        /// <returns>Render Fragment rendered from the root of the GUI stack.</returns>
        public RenderFragment? Render();
    }
}
