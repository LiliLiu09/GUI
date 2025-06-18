using Microsoft.AspNetCore.Components;

namespace GUI
{
    /// <summary>
    /// Grid that displays data.
    /// </summary>
    public abstract class Grid : Component
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="columns">Data grid columns that will be used to display data.</param>
        /// <param name="enabled">True if this grid's interactivity should be responsive, false otherwise.</param>
        /// <param name="hidden">True if this grid should not be rendered, false otherwise.</param>
        /// <param name="parent">GUI Component this grid should be rendered inside.</param>
        internal Grid(IEnumerable<IGridColumn> columns, bool enabled = true, bool hidden = false, IComponent? parent = null) : base(enabled, hidden, parent)
        {
            Columns = columns?.ToList() ?? new List<IGridColumn>();
        }

        protected readonly Dictionary<string, object> FilterValues = new();

        /// <summary>
        /// Constructor dynamically determines columns.
        /// </summary>
        /// <param name="table">Table to use to generate columns on the grid from.</param>
        /// <param name="enabled">True if this grid's interactivity should be responsive, false otherwise.</param>
        /// <param name="hidden">True if this grid should not be rendered, false otherwise.</param>
        /// <param name="parent">GUI Component this grid should be rendered inside.</param>
        internal Grid(IEnumerable<IColumn> columns, bool enabled = true, bool hidden = false, IComponent? parent = null) : base(enabled, hidden, parent)
        {
            Columns = new List<IGridColumn>();

            foreach(IColumn column in columns)
            {
                AddGridColumn(column.Title, column.GetValueType());
            }
        }

        /// <summary>
        /// Grid columns to control and display data in the grid.
        /// </summary>
        protected List<IGridColumn> Columns { get; }

        /// <summary>
        /// Add a grid column to the data grid.
        /// </summary>
        /// <typeparam name="T">Data type this column will store and display.</typeparam>
        /// <param name="title">Display title of the column.</param>
        /// <param name="value">Value this column will display.</param>
        public void AddGridColumn<T>(string title, T value)
        {
            Columns.Add(new GridColumn<T>(value, title, Enabled, Hidden, this));
        }

        /// <summary>
        /// Add a grid column to the data grid.
        /// </summary>
        /// <typeparam name="T">Data type this column will store and display.</typeparam>
        /// <param name="column">SQL column the new grid column represents.</param>
        /// <param name="defaultValue">Value to use when there is no value in the column.</param>
        public void AddGridColumn<T>(IColumn column, T defaultValue)
        {
            Columns.Add(new GridColumn<T>(column, defaultValue, Enabled, Hidden, this));
        }

        /// <summary>
        /// Gets the current filter value for the specified key.
        /// </summary>
        /// <param name="key">The key to retrieve the filter value for.</param>
        /// <returns>The filter value, if any.</returns>
        protected object? GetFilterValue(string key)
        {
            return FilterValues.TryGetValue(key, out var value) ? value : null;
        }

        /// <summary>
        /// Builds a column template to render the value for a specific key.
        /// </summary>
        /// <param name="key">The dictionary key representing the column name.</param>
        /// <returns>A render fragment for the column cell.</returns>
        protected static RenderFragment<Dictionary<string, object>> BuildColumnTemplate(string key)
        {
            return row => builder =>
            {
                builder.AddContent(0, row.ContainsKey(key) ? row[key] : null);
            };
        }
    }
}
