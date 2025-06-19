namespace GUI
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using System.Collections.Generic;


    /// <summary>
    /// Handles the construction of the GUI stack and managing the GUI stack root.
    /// </summary>
    public partial class GuiBuilder : IGuiBuilder
    {
        /// <summary>
        /// Root of the GUI stack.
        /// </summary>
        public Component Component { get; internal set; } = null!;

        /// <summary>
        /// Cache for component that will acquire focus on loading.
        /// </summary>
        private Component focus = null!;

        /// <summary>
        /// Component that will acquire focus on loading.
        /// </summary>
        public Component Focus 
        {
            get
            {
                return focus;
            }

            set
            {
                focus = value;
            }
        }

        /// <summary>
        /// Holds the components that form the root of the rendering stack.
        /// </summary>
        private readonly List<Component> rootComponents = new List<Component>();

        /// <summary>
        /// Adds a component to the root of the GUI stack for rendering.
        /// </summary>
        /// <param name="component">The component to add as a root-level element.</param>
        public void AddComponent(Component component)
        {
            rootComponents.Add(component);
        }

        /// <summary>
        /// Renders the GUI Stack into a render fragment that may be inserted into a Razor Page.
        /// </summary>
        /// <returns>Render Fragment rendered from the root of the GUI stack.</returns>
        public RenderFragment Render()
        {
            return builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "style", "display: flex; flex-direction: column; gap: 8px; align-items: flex-start;");

                int sequence = 2;
                foreach (Component? component in rootComponents)
                {
                    Validate();
                    builder.AddContent(sequence++, component.Render(focus: component));
                }

                builder.CloseElement();
            };
        }

        /// <summary>
        /// Clears all components from the GUI builder's root component list.
        /// Disposes each component if it implements <see cref="IDisposable"/>.
        /// Resets the main component and focus component references.
        /// </summary>
        public void Clear()
        {
            // If the components implement IDisposable and need cleanup.
            foreach (Component component in rootComponents)
            {
                if (component is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            rootComponents.Clear();
            Component = null!;
            focus = null!;
        }

        /// <summary>
        /// Assess if the current GUI stack is valid for rendering.
        /// </summary>
        private void Validate()
        {
        }

        /// <summary>
        /// Creates a new <see cref="Stack"/> component and sets it as the root component of the stack.
        /// </summary>
        /// <param name="enabled">True if this component should be responsive, false otherwise.</param>
        /// <param name="hidden">False if the component should be rendered, true otherwise.</param>
        /// <returns>A newly created and configured <see cref="Stack"/> component.</returns>
        public Stack CreateStack(bool enabled = true, bool hidden = false)
        {
            Stack stack = new Stack(enabled, hidden, null);

            AddComponent(stack);

            Component = stack;

            return stack;
        }

        /// <summary>
        /// Creates a new <see cref="Button"/> component and sets it as the root component of the stack.
        /// </summary>
        /// <param name="onClickEvent">Event to invoke when button is clicked.</param>
        /// <param name="parameters">The configuration parameters for customizing the button appearance and behavior.</param>
        /// <param name="enabled">True if this button should be responsive, false otherwise.</param>
        /// <param name="hidden">False if the button should be rendered, true otherwise.</param>
        /// <returns>A newly created and configured <see cref="Button"/> component.</returns>
        public Button CreateButton(EventCallback onClickEvent, ButtonParameters parameters, bool enabled = true, bool hidden = false)
        {
            Button button = new Button(onClickEvent, enabled, hidden, null, parameters);

            AddComponent(button);

            Component = button;

            return button;
        }

        /// <summary>
        /// Creates a new <see cref="CheckBox"/> component and sets it as the root component of the stack.
        /// </summary>
        /// <param name="defaultValue">The initial checked state of the checkbox.</param>
        /// <param name="parameters">Optional visual and behavioral configuration for the checkbox.</param>
        /// <param name="onChangeEvent">An optional callback invoked when the checkbox value changes.</param>
        /// <param name="column">The data column to which the checkbox is bound, if any.</param>
        /// <param name="enabled">Whether the checkbox is enabled for user interaction.</param>
        /// <param name="hidden">Whether the checkbox should be hidden from the UI.</param>
        /// <returns>A newly created and configured <see cref="CheckBox"/> component.</returns>
        public CheckBox CreateCheckBox(bool defaultValue = false, InputParameters? parameters = null, EventCallback<bool> onChangeEvent = default, IColumn? column = null, bool enabled = true, bool hidden = false)
        {
            CheckBox checkBox = new CheckBox(defaultValue, parameters, onChangeEvent, column, enabled, hidden, null);

            AddComponent(checkBox);

            Component = checkBox;

            return checkBox;
        }

        /// <summary>
        /// Creates a new <see cref="DatePicker"/> component and sets it as the root component of the stack.
        /// </summary>
        /// <param name="defaultValue">Value the component should be rendered reflecting.</param>
        /// <param name="parameters">Optional input parameters to customize the date picker appearance and behavior.</param>
        /// <param name="onChangeEvent">Optional callback triggered when the date value changes.</param>
        /// <param name="column">Column this component's value should reflect.</param>
        /// <param name="enabled">True if this component should be responsive, false otherwise.</param>
        /// <param name="hidden">False if the component should be rendered, true otherwise.</param>
        /// <returns>A newly created and configured <see cref="DatePicker"/> component.</returns>
        public DatePicker CreateDatePicker(DateOnly defaultValue, InputParameters? parameters = null, EventCallback<DateOnly?> onChangeEvent = default, IColumn? column = null, bool enabled = true, bool hidden = false)
        {
            DatePicker datePicker = new DatePicker(defaultValue, parameters, onChangeEvent, column, enabled, hidden, null);

            AddComponent(datePicker);
            
            Component = datePicker;

            return datePicker;
        }

        /// <summary>
        /// Creates a new <see cref="TimePicker"/> component and sets it as the root component of the stack.
        /// </summary>
        /// <param name="defaultValue">Value the component should be rendered reflecting.</param>
        /// <param name="parameters">Optional input parameters to customize the time picker appearance and behavior.</param>
        /// <param name="onChangeEvent">Optional callback triggered when the time value changes.</param>
        /// <param name="column">Column this component's value should reflect.</param>
        /// <param name="enabled">True if this component should be responsive, false otherwise.</param>
        /// <param name="hidden">False if the component should be rendered, true otherwise.</param>
        /// <returns>A newly created and configured <see cref="TimePicker"/> component.</returns>
        public TimePicker CreateTimePicker(TimeOnly defaultValue, InputParameters? parameters = null, EventCallback<TimeOnly?> onChangeEvent = default, IColumn? column = null, bool enabled = true, bool hidden = false)
        {
            TimePicker timePicker = new TimePicker(defaultValue, parameters, onChangeEvent, column, enabled, hidden, null);

            AddComponent(timePicker);

            Component = timePicker;

            return timePicker;
        }

        /// <summary>
        /// Creates a new <see cref="DateTimePicker"/> component and sets it as the root component of the stack.
        /// </summary>
        /// <param name="defaultValue">Value the component should be rendered reflecting.</param>
        /// <param name="parameters">Optional input parameters to customize the datetime picker appearance and behavior.</param>
        /// <param name="onChangeEvent">Optional callback triggered when the datetime value changes.</param>
        /// <param name="column">Column this component's value should reflect.</param>
        /// <param name="enabled">True if this component should be responsive, false otherwise.</param>
        /// <param name="hidden">False if the component should be rendered, true otherwise.</param>
        /// <returns>A newly created and configured <see cref="DateTimePicker"/> component.</returns>
        public DateTimePicker CreateDateTimePicker(DateTime defaultValue, InputParameters? parameters = null, EventCallback<DateTime?> onChangeEvent = default, IColumn? column = null, bool enabled = true, bool hidden = false)
        {
            DateTimePicker dateTimePicker = new DateTimePicker(defaultValue, parameters, onChangeEvent, column, enabled, hidden, null);

            AddComponent(dateTimePicker);

            Component = dateTimePicker;

            return dateTimePicker;
        }

        /// <summary>
        /// Creates a new <see cref="DialogBox"/> component with a title, optional content, and optional configuration parameters, and sets it as the root component of the stack.
        /// </summary>
        /// <param name="title">The title text displayed in the dialog box header.</param>
        /// <param name="content">A collection of string values rendered as paragraphs in the dialog body.</param>
        /// <param name="parameters">Optional configuration settings for customizing the dialog box’s appearance and behavior.</param>
        /// <param name="enabled">Whether the dialog box is enabled for user interaction (default: true).</param>
        /// <param name="hidden">Whether the dialog box is hidden from the UI (default: false).</param>
        /// <returns>A newly created and configured <see cref="DialogBox"/> component.</returns>
        public DialogBox CreateDialogBox(string title, List<string>? content = null, DialogParameters? parameters = null, bool enabled = true, bool hidden = false)
        {
            DialogBox dialogBox = new DialogBox(title, content, parameters, enabled, hidden, null);

            AddComponent(dialogBox);

            Component = dialogBox;

            return dialogBox;
        }

        /// <summary>
        /// Creates a new <see cref="Label"/> component and sets it as the root component of the stack.
        /// </summary>
        /// <param name="text">The text content to be displayed in the label.</param>
        /// <param name="enabled">Whether the label is enabled for rendering (default: true).</param>
        /// <param name="hidden">Whether the label is hidden from the UI (default: false).</param>
        /// <returns>A newly created and configured <see cref="Label"/> component.</returns>
        public Label CreateLabel(string text, bool enabled = true, bool hidden = false)
        {
            Label label = new Label(text, enabled, hidden, null);

            AddComponent(label);

            Component = label;

            return label;
        }

        /// <summary>
        /// Creates a new <see cref="DropDownList{T}"/> component and sets it as the root component of the stack.
        /// </summary>
        /// <typeparam name="T">Data type of elements in the list.</typeparam>
        /// <param name="defaultValue">Initial value to be selected on render.</param>
        /// <param name="data">List of values available to be selected.</param>
        /// <param name="parameters">Optional configuration settings for customizing the dropdown’s appearance and behavior.</param>
        /// <param name="onChangeEvent">Event to invoke when a value is selected.</param>
        /// <param name="column">Column this component's value should reflect.</param>
        /// <param name="enabled">True if this component should be responsive, false otherwise.</param>
        /// <param name="hidden">False if the component should be rendered, true otherwise.</param>
        /// <returns>A newly created and configured <see cref="DropDownList{T}"/> component.</returns>
        public DropDownList<T> CreateDropDownList<T>(T defaultValue, List<T> data, DropDownParameters<T>? parameters = null, EventCallback<T> onChangeEvent = default, IColumn? column = null, bool enabled = true, bool hidden = false)
        {
            DropDownList<T> genericDropDownList = new GenericDropDownList<T>(defaultValue, data, parameters, onChangeEvent, column, enabled, hidden);

            AddComponent(genericDropDownList);

            Component = genericDropDownList;

            return genericDropDownList;
        }

        /// <summary>
        /// Creates a new <see cref="DropDownList{T}"/> of records component and sets it as the root component of the stack.
        /// </summary>
        /// <typeparam name="T">Record type of elements in the list.</typeparam>
        /// <param name="defaultValue">Initial value to be selected on render.</param>
        /// <param name="data">List of values available to be selected.</param>
        /// <param name="parameters">Optional configuration settings for customizing the dropdown’s appearance and behavior.</param>
        /// <param name="onChangeEvent">Event to invoke when a value is selected.</param>
        /// <param name="column">Column this component's value should reflect.</param>
        /// <param name="enabled">True if this component should be responsive, false otherwise.</param>
        /// <param name="hidden">False if the component should be rendered, true otherwise.</param>
        /// <returns>A newly created and configured <see cref="DropDownList{T}"/> component.</returns>
        public DropDownList<T> CreateDropDownList<T>(T defaultValue, RecordList<T> data, DropDownParameters<T>? parameters = null, EventCallback<T> onChangeEvent = default, IColumn? column = null, bool enabled = true, bool hidden = false)
            where T : Record
        {
            DropDownList<T> recordDropDownList = new RecordDropDownList<T>(defaultValue, data, parameters, onChangeEvent, column, enabled, hidden);

            AddComponent(recordDropDownList);

            Component = recordDropDownList;

            return recordDropDownList;
        }

        /// <summary>
        /// Creates a new value-based <see cref="RadioList{T}"/> component using a list of options and sets it as the root component of the stack.
        /// </summary>
        /// <typeparam name="T">The data type of the values used for the radio button options.</typeparam>
        /// <param name="values">The list of selectable values to populate the radio buttons.</param>
        /// <param name="defaultValue">The initial value to be selected when the radio list is rendered.</param>
        /// <param name="parameters">Optional configuration settings to customize the appearance and behavior of the RadioList.</param>
        /// <param name="onChangeEvent">The event callback to be triggered when the selected value changes.</param>
        /// <param name="boundColumn">Optional column binding associated with this radio list component.</param>
        /// <param name="enabled">Determines whether the radio list is interactive (default: true).</param>
        /// <param name="hidden">Determines whether the radio list is visible in the UI (default: false).</param>
        /// <returns>A newly created and configured <see cref="RadioList{T}"/> component.</returns>
        public RadioList<T> CreateRadioList<T>(List<T> values, T defaultValue, RadioParameters<T>? parameters = null, EventCallback<T> onChangeEvent = default, IColumn? boundColumn = null, bool enabled = true, bool hidden = false)
        {
            RadioList<T> radioList = new RadioList<T>(values, defaultValue, parameters, onChangeEvent, boundColumn, enabled, hidden);

            AddComponent(radioList);

            Component = radioList;

            return radioList;
        }

        /// <summary>
        /// Creates a new <see cref="Tab"/> component with multiple tab items and sets it as the root component of the stack.
        /// </summary>
        /// <param name="items">A collection of tab headers and their corresponding content.</param>
        /// <param name="parameters">Optional configuration parameters for customizing tab appearance and behavior.</param>
        /// <param name="enabled">Whether the tab component is interactive.</param>
        /// <param name="hidden">Whether the tab component is hidden from view.</param>
        /// <returns>A newly created and configured <see cref="Tab"/> component.</returns>
        public Tab CreateTab(IEnumerable<(string Header, string Content)> items, TabParameters? parameters = null, bool enabled = true, bool hidden = false)
        {
            Tab tab = new Tab(items, parameters, enabled, hidden);

            AddComponent(tab);

            Component = tab;

            return tab;
        }

        /// <summary>
        /// Creates a new single-line <see cref="TextField"/> component with optional default value and column binding, and sets it as the root component of the stack.
        /// </summary>
        /// <param name="defaultValue">The initial text value.</param>
        /// <param name="parameters">Optional parameters to customize the text field's appearance and behavior.</param>
        /// <param name="boundColumn">The optional bound column.</param>
        /// <param name="enabled">Whether the text field is enabled.</param>
        /// <param name="hidden">Whether the text field is hidden.</param>
        /// <returns>A newly created and configured <see cref="TextField"/> component.</returns>
        public TextField CreateTextField(string defaultValue, TextParameters? parameters = null, IColumn? boundColumn = null, bool enabled = true, bool hidden = false)
        {
            TextField textField = new TextField(defaultValue, parameters, boundColumn, enabled, hidden);

            AddComponent(textField);

            Component = textField;

            return textField;
        }

        /// <summary>
        /// Creates a new multi-line <see cref="TextBox"/> component with optional configuration and data binding, and sets it as the root component of the stack.
        /// </summary>
        /// <param name="text">The list of paragraph lines to populate the text box.</param>
        /// <param name="parameters">Optional parameters to customize the text box's appearance and behavior.</param>
        /// <param name="boundColumn">Optional bound data column metadata.</param>
        /// <param name="enabled">Determines whether the text box is enabled for user input.</param>
        /// <param name="hidden">Determines whether the text box is hidden in the user interface.</param>
        /// <returns>A newly created and configured <see cref="TextBox"/> component.</returns>
        public TextBox CreateTextBox(List<string> text, TextParameters? parameters = null, IColumn? boundColumn = null, bool enabled = true, bool hidden = false)
        {
            TextBox textBox = new TextBox(text, parameters, boundColumn, enabled, hidden);

            AddComponent(textBox);

            Component = textBox;

            return textBox;
        }

        /// <summary>
        /// Creates a generic grid component displaying a list of data items, and sets it as the root component of the stack.
        /// </summary>
        /// <typeparam name="T">The type of data elements to be displayed in the grid.</typeparam>
        /// <param name="data">The list of data items to display.</param>
        /// <param name="enabled">Indicates whether the grid is enabled for interaction.</param>
        /// <param name="hidden">Indicates whether the grid is hidden in the UI.</param>
        /// <returns>A newly created and configured generic <see cref="Grid"/> component.</returns>
        public Grid CreateGrid<T>(List<T> data, bool enabled = true, bool hidden = false)
        {
            Grid grid = new GenericGrid<T>(data, enabled, hidden, null);

            AddComponent(grid);

            Component = grid;

            return grid;
        }

        /// <summary>
        /// Creates a data grid component using a <see cref="RecordList{T}"/> as its data source.
        /// </summary>
        /// <typeparam name="T">The type of records in the list, which must inherit from <see cref="Record"/>.</typeparam>
        /// <param name="recordList">The data to display in the grid.</param>
        /// <param name="enabled">Specifies whether the grid is enabled (default: true).</param>
        /// <param name="hidden">Specifies whether the grid is hidden from view (default: false).</param>
        /// <returns>A newly created and configured <see cref="Grid"/> component populated with the specified record list.</returns>
        public Grid CreateGrid<T>(RecordList<T> recordList, bool enabled = true, bool hidden = false) where T : Record
        {
            Grid grid = new RecordGrid(recordList, null, enabled, hidden);

            AddComponent(grid);

            Component = grid;

            return grid;
        }

        /// <summary>
        /// Creates a record grid component displaying a collection of records with specified columns.
        /// </summary>
        /// <param name="recordList">The collection of records to display in the grid.</param>
        /// <param name="columns">The columns defining which fields to display from the records.</param>
        /// <param name="enabled">Indicates whether the grid is enabled for interaction.</param>
        /// <param name="hidden">Indicates whether the grid is hidden in the UI.</param>
        /// <returns>A newly created and configured <see cref="RecordGrid"/> component.</returns>
        public Grid CreateGrid(IEnumerable<IRecord> recordList, IEnumerable<IColumn> columns, bool enabled = true, bool hidden = false)
        {
            Grid grid = new RecordGrid(recordList, columns, enabled, hidden, null);

            Component = grid;

            return grid;
        }

        /// <summary>
        /// Creates a filtered record grid component from a record source and filter criteria.
        /// </summary>
        /// <param name="table">The record source from which to retrieve records.</param>
        /// <param name="filters">The filter criteria to apply to the record source.</param>
        /// <param name="enabled">Indicates whether the grid is enabled for interaction.</param>
        /// <param name="hidden">Indicates whether the grid is hidden in the UI.</param>
        /// <returns>A newly created and configured filtered <see cref="RecordGrid"/> component.</returns>
        public Grid CreateGrid(IRecordSource table, IFilter filters, bool enabled = true, bool hidden = false)
        {
            Grid grid = new RecordGrid(table.Where(filters), null, enabled, hidden, null);

            Component = grid;

            return grid;
        }
    }
}