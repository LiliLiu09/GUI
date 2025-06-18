namespace GUI
{

    /// <summary>
    /// A grid for displaying a set of data.
    /// </summary>
    /// <typeparam name="T">Type of data to be displayed.</typeparam>
    public class GenericGrid<T> : Grid
    {
        private readonly IEnumerable<T> gridData;

        private readonly List<Dictionary<string, object>> records;

        /// <summary>
        /// Gets the component type associated with this GUI class.
        /// </summary>
        protected override Type GuiComponentType
        {
            get { return typeof(Components.DataGrid<Dictionary<string, object>>); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericGrid{T}"/> class.
        /// </summary>
        /// <param name="data">The data to display in the grid.</param>
        /// <param name="enabled">Whether the grid is enabled.</param>
        /// <param name="hidden">Whether the grid is hidden.</param>
        /// <param name="gridColumns">Optional column definitions.</param>
        /// <param name="parent">Optional parent component.</param>
        public GenericGrid(IEnumerable<T> data, bool enabled, bool hidden, List<IGridColumn>? gridColumns = null, IComponent? parent = null) : base(gridColumns, enabled, hidden, parent)
        {
            gridData = data;

            records = gridData.Cast<Dictionary<string, object>>().ToList();
        }

        /// <summary>
        /// Renders the component into a stack layout containing a data grid.
        /// </summary>
        /// <param name="focus">The component to focus.</param>
        /// <returns>A render fragment for the component.</returns>
        internal override RenderFragment Render(Component focus) => builder =>
        {
            int seq = 0;
            builder.OpenComponent(seq++, typeof(Components.Stack));
            builder.AddAttribute(seq++, "ChildContent", BuildDataGrid());
            builder.CloseComponent();
        };

        /// <summary>
        /// Builds the render fragment for the data grid component.
        /// </summary>
        /// <returns>The render fragment containing the data grid.</returns>
        private RenderFragment BuildDataGrid() => stackBuilder =>
        {
            int seq = 0;

            stackBuilder.OpenComponent(seq++, GuiComponentType);
            stackBuilder.AddAttribute(seq++, "AllowFiltering", true);
            stackBuilder.AddAttribute(seq++, "AllowColumnPicking", true);
            stackBuilder.AddAttribute(seq++, "PageSize", 2);
            stackBuilder.AddAttribute(seq++, "AllowPaging", true);
            stackBuilder.AddAttribute(seq++, "AllowSorting", true);
            stackBuilder.AddAttribute(seq++, "FilterMode", FilterMode.SimpleWithMenu);
            stackBuilder.AddAttribute(seq++, "ColumnWidth", "300px");
            stackBuilder.AddAttribute(seq++, "PageSizeOptions", new int[] { 2, 10, 20, 30 });
            stackBuilder.AddAttribute(seq++, "Data", records);
            stackBuilder.AddAttribute(seq++, "Columns", BuildGridColumns());
            stackBuilder.CloseComponent();
        };

        /// <summary>
        /// Builds the render fragment for the grid columns based on the keys
        /// in the first row of the dataset.
        /// </summary>
        /// <returns>The render fragment containing column definitions.</returns>
        private RenderFragment BuildGridColumns() => columnsBuilder =>
        {
            int seq = 0;

            if (records.Any())
            {
                Dictionary<string, object> firstRow = records.First();

                foreach (string key in firstRow.Keys)
                {
                    columnsBuilder.OpenComponent<DataGridColumn<Dictionary<string, object>>>(seq++);
                    columnsBuilder.AddAttribute(seq++, "Title", key);
                    columnsBuilder.AddAttribute(seq++, "Width", "150px");
                    columnsBuilder.AddAttribute(seq++, "FilterValue", GetFilterValue(key));
                    columnsBuilder.AddAttribute(seq++, "Template", BuildColumnTemplate(key));
                    columnsBuilder.CloseComponent();
                }
            }
        };
    }
}
