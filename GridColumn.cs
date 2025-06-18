namespace GUI
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Represents a column in a data grid, capable of binding to a specific data type and supporting interaction.
    /// </summary>
    /// <typeparam name="T">The data type of the values within this column.</typeparam>
    public class GridColumn<T> : BindableComponent<T>, IGridColumn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridColumn{T}"/> class with a bound data column and default value.
        /// </summary>
        /// <param name="boundColumn">The data column that this grid column binds to.</param>
        /// <param name="defaultValue">The default value of the column.</param>
        /// <param name="enabled">Indicates whether the column is enabled for editing.</param>
        /// <param name="hidden">Indicates whether the column is hidden in the UI.</param>
        /// <param name="parent">The parent <see cref="Grid"/> component that contains this column.</param>
        /// <param name="onChangeEvent">An optional callback invoked when the column value changes.</param>
        /// <exception cref="InvalidOperationException">Thrown if the bound column's type does not match <typeparamref name="T"/>.</exception>
        public GridColumn(IColumn boundColumn, T defaultValue, bool enabled, bool hidden, Grid parent, EventCallback<T> onChangeEvent = default)
            : base((T)boundColumn.GetValue(), enabled, hidden, onChangeEvent, boundColumn, parent)
        {
            if (boundColumn.GetValueType() is not T)
            {
                InvalidOperationException exception = new InvalidOperationException("Attempted to set the default value of a grid column bound to a column of an incompatible type");

                exception.Data.Add("Column", boundColumn.Sql);
                exception.Data.Add("DataType", typeof(T));

                throw exception;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GridColumn{T}"/> class using an unbound value and custom title.
        /// </summary>
        /// <param name="value">The initial value of the column.</param>
        /// <param name="title">The title displayed in the column header.</param>
        /// <param name="enabled">Indicates whether the column is enabled for interaction.</param>
        /// <param name="hidden">Indicates whether the column is hidden from view.</param>
        /// <param name="parent">The parent component containing this column.</param>
        /// <param name="onChangeEvent">An optional callback invoked when the column value changes.</param>
        public GridColumn(T value, string title, bool enabled, bool hidden, IComponent parent, EventCallback<T> onChangeEvent = default)
            : base(value, enabled, hidden, onChangeEvent, null, parent)
        { }

        /// <summary>
        /// Gets the component type associated with this GUI class.
        /// </summary>
        protected override Type GuiComponentType
        {
            get { return typeof(Components.DataGridColumn<T>); }
        }

        /// <summary>
        /// Renders the grid column as a <see cref="RenderFragment"/>.
        /// </summary>
        /// <param name="focus">The currently focused component, if any.</param>
        /// <returns>A <see cref="RenderFragment"/> representing the rendered column.</returns>
        internal override RenderFragment Render(Component focus)
        {
            throw new NotImplementedException();
        }
    }
}