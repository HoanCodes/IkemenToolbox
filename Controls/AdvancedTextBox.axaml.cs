using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Markup.Xaml.MarkupExtensions;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using IkemenToolbox.Extensions;
using Avalonia.Media;

namespace IkemenToolbox.Controls
{
    public partial class AdvancedTextBox : UserControl
    {
        public BindingBase TextBinding { get; set; }

        public static readonly StyledProperty<string> WatermarkProperty =
        AvaloniaProperty.Register<AdvancedTextBox, string>(nameof(Watermark));

        public static readonly StyledProperty<IBrush> BorderBackgroundProperty =
        AvaloniaProperty.Register<AdvancedTextBox, IBrush>(nameof(BorderBackground));

        public static readonly StyledProperty<IBrush> TextBoxBackgroundProperty =
        AvaloniaProperty.Register<AdvancedTextBox, IBrush>(nameof(TextBoxBackground));

        public static readonly StyledProperty<IBrush> TextBoxForegroundProperty =
        AvaloniaProperty.Register<AdvancedTextBox, IBrush>(nameof(TextBoxForeground), defaultValue: Brushes.White);
        public string Watermark
        {
            get => GetValue(WatermarkProperty);
            set => SetValue(WatermarkProperty, value);
        }
        public IBrush BorderBackground
        {
            get => GetValue(BorderBackgroundProperty);
            set => SetValue(BorderBackgroundProperty, value);
        }
        public IBrush TextBoxBackground
        {
            get => GetValue(TextBoxBackgroundProperty);
            set => SetValue(TextBoxBackgroundProperty, value);
        }
        public IBrush TextBoxForeground
        {
            get  => GetValue(TextBoxForegroundProperty);
            set => SetValue(TextBoxForegroundProperty, value);
        }

        public AdvancedTextBox()
        {
            InitializeComponent();

            // Account for user hovering over TextBlock
            textBlock.PointerEntered += (_, e) => textBox.RaiseEvent(e);

            // Account for user clicking different places to activate the TextBox
            GotFocus += (_, __) => FocusTextBox();
            textBlock.PointerPressed += (_, __) => FocusTextBox();

            // Account for value changing when initializing a definition
            textBox.TextChanged += TextBox_TextChanged;

            // Format Text when Control loses focus
            textBox.LostFocus += (_, __) => FormatText();
        }

        private void FocusTextBox()
        {
            textBox.Focus();
            textBox.SelectAll();
            UpdateVisual();
        }

        private void TextBox_TextChanged(object _, TextChangedEventArgs __)
        {
            if (!textBox.IsFocused)
            {
                FormatText();
            }
        }

        private void FormatText()
        {
            //Format text here
            UpdateVisual();
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);

            if (TextBinding != null && TextBinding is CompiledBindingExtension binding)
            {
                var path = binding.Path.ToString();
                string displayAttributeName = null;

                //Automatically set Watermark

                var property = DataContext?.GetType().GetProperty(path);
                if (property != null)
                {
                    // Automatically set Tooltip
                    var attribute = property.GetCustomAttributes(false).FirstOrDefault(x => x is DisplayAttribute);
                    if (attribute != null)
                    {
                        var display = (DisplayAttribute)attribute;
                        ToolTip.SetTip(toolTipBorder, display.Description);

                        if (!string.IsNullOrWhiteSpace(display.Name))
                        {
                            displayAttributeName = display.Name;
                        }
                    }

                    // Set up binding
                    textBox.Bind(TextBox.TextProperty, new Binding { Source = DataContext, Path = path });
                }

                Watermark ??= displayAttributeName ?? path.SplitAndGetLast('.');
            }

            UpdateVisual();
        }

        private void UpdateVisual()
        {
            textBlock.IsVisible = control.IsEnabled && !textBox.IsFocused && !string.IsNullOrWhiteSpace(textBox.Text);
        }
    }
}
