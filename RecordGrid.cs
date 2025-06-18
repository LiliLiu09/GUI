namespace GUI
{


    /// <summary>
    /// Represents a data grid for displaying values from a collection of records in a structured format.
    /// </summary>
    public class RecordGrid : Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordGrid"/> class using the specified data and column definitions.
        /// </summary>
        /// <param name="data">The collection of records to display in the grid.</param>
        /// <param name="columns">The collection of columns defining the structure of the grid.</param>
        /// <param name="enabled">Specifies whether the grid is enabled (default: true).</param>
        /// <param name="hidden">Specifies whether the grid is hidden from view (default: false).</param>
        /// <param name="parent">The parent GUI component in which this grid is rendered (optional).</param>
        public RecordGrid(IEnumerable<IRecord> data, IEnumerable<IColumn> columns, bool enabled, bool hidden, IComponent? parent = null) : base(columns, enabled, hidden, parent)
        {
            Data = data;
        }

        /// <summary>
        /// Gets or sets the collection of records that this grid displays.
        /// </summary>
        protected IEnumerable<IRecord> Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override Type GuiComponentType
        {
            get { return typeof(Components.DataGrid<IRecord>); }
        }

        /// <summary>
        /// Renders the <see cref="RecordGrid"/> into a <see cref="RenderFragment"/> to be displayed in a Razor component.
        /// </summary>
        /// <param name="focus">The currently focused component, if any.</param>
        /// <returns>A <see cref="RenderFragment"/> representing the rendered grid.</returns>
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
            stackBuilder.AddAttribute(seq++, "Data", Data);
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

            if (Data.Any())
            {
                IRecord firstRow = Data.First();

                foreach (IColumn column in firstRow.Columns.Values)
                {
                    columnsBuilder.OpenComponent<DataGridColumn<Dictionary<string, object>>>(seq++);
                    columnsBuilder.AddAttribute(seq++, "Title", column.Title);
                    columnsBuilder.AddAttribute(seq++, "Width", "150px");
                    columnsBuilder.AddAttribute(seq++, "FilterValue", GetFilterValue(column.Title));
                    columnsBuilder.AddAttribute(seq++, "Template", BuildColumnTemplate(column.Title));
                    columnsBuilder.CloseComponent();
                }
            }
        };
    }
}
