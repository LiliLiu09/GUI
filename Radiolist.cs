namespace GUI
{
    using Microsoft.AspNetCore.Components;

    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a configurable radio button list for selecting a single value from a predefined set.
    /// </summary>
    /// <typeparam name="T">The type of the selectable values.</typeparam>
    public class RadioList<T> : BindableComponent<T>
    {
        /// <summary>
        /// Gets or sets the parameters used to define the radio list's data, appearance, and behavior.
        /// </summary>
        internal RadioParameters<T> Parameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RadioList{T}"/> class with the provided values and configuration.
        /// </summary>
        /// <param name="values">The list of selectable values to render as radio options.</param>
        /// <param name="defaultValue">The default selected value.</param>
        /// <param name="parameters">The configuration parameters for the radio list.</param>
        /// <param name="onChangeEvent">An optional callback triggered when the selected value changes.</param>
        /// <param name="column">The optional column the radio list is bound to.</param>
        /// <param name="enabled">Indicates whether the component is enabled.</param>
        /// <param name="hidden">Indicates whether the component is hidden.</param>
        /// <param name="parent">The parent component in the GUI hierarchy.</param>
        /// <exception cref="ArgumentNullException">Thrown when the list of values is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the default value is not in the list of values.</exception>
        public RadioList(List<T> values, T defaultValue, RadioParameters<T>? parameters = null, EventCallback<T> onChangeEvent = default, IColumn? column = null, bool enabled = true, bool hidden = false, IComponent? parent = null)
            : base(defaultValue, enabled, hidden, onChangeEvent, column, parent)
        {
            Parameters = parameters ?? new RadioParameters<T>();

            Parameters.Values = values ?? throw new ArgumentNullException(nameof(values));

            Parameters.Selected = Parameters.Values.IndexOf(defaultValue);

            if (Parameters.Selected == -1)
            {
                ArgumentException exception = new ArgumentException($"Attempted to set value {defaultValue} of a radio list that does not contain this value.");

                exception.Data.Add("DefaultValue", defaultValue);
                exception.Data.Add("ValueList", values);

                throw exception;
            }
        }

        /// <summary>
        /// Gets the component type associated with this GUI class.
        /// </summary>
        protected override Type GuiComponentType
        {
            get { return typeof(Components.Radio); }
        }
        /// <summary>
        /// Renders the RadioList as a <see cref="RenderFragment"/>.
        /// </summary>
        /// <param name="focus">The component currently in focus (if any).</param>
        /// <returns>A <see cref="RenderFragment"/> that represents the rendered radio list.</returns>
        internal override RenderFragment Render(Component focus)
        {
            Dictionary<string, object> parameters = ExtractParameters(Parameters);

            return CreateRenderFragment(GuiComponentType, parameters);
        }
    }
}