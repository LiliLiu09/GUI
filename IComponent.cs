namespace GUI
{
    // TODO: Move this interface to reference library.

    /// <summary>
    /// Defines the Interface for the components.
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// Disables the component.
        /// </summary>
        public void Disable();

        /// <summary>
        /// Enables the component.
        /// </summary>
        public void Enable();

        /// <summary>
        /// Gets the list of child components.
        /// </summary>
        public List<IComponent> ChildComponents { get; }

        /// <summary>
        /// Adds a child component to the current component.
        /// </summary>
        /// <param name="component">The child component to add.</param>
        public abstract void AddChild(IComponent component);
    }
}