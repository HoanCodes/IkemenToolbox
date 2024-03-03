using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
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

            // Account for user hovering over textStackPanel
            textStackPanel.PointerEntered += (_, e) => textBox.RaiseEvent(e);
            textStackPanel.PointerExited += (_, e) => textBox.RaiseEvent(e);

            // Account for user clicking different places to activate the TextBox
            GotFocus += (_, __) => FocusTextBox();
            textStackPanel.PointerPressed += (_, __) => FocusTextBox();

            // Account for value changing when initializing a definition
            textBox.TextChanged += TextBox_TextChanged;

            // Format Text when Control loses focus
            textBox.LostFocus += (_, __) => FormatText();

        }

        private void FocusTextBox()
        {
            textBox.Focus();
            textBox.CaretIndex = textBox.Text != null ? textBox.Text.Length : 0;
            UpdateVisual();
        }

        private void TextBox_TextChanged(object _, TextChangedEventArgs __)
        {
            if (!textBox.IsFocused)
            {
                FormatText();
            }
        }

        //Format text here
        private void FormatText()
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                return;
            }

            var textBlock = new TextBlock();
            var stackPanelChildren = textStackPanel.Children;
            stackPanelChildren.Clear();

            foreach (var containingText in textBox.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                var text = containingText;

                if (text.TryGetTip(out var tip))
                {
                    if (!string.IsNullOrWhiteSpace(textBlock.Text))
                    {
                        stackPanelChildren.Add(textBlock);
                        textBlock = new();
                    }

                    var tippedTextBlock = new TextBlock
                    {
                        Text = text,
                        Foreground = Brushes.Yellow,
                    };
                    ToolTip.SetTip(tippedTextBlock, tip);
                    stackPanelChildren.Add(tippedTextBlock);

                    continue;
                }

                textBlock.Text += (string.IsNullOrWhiteSpace(textBlock.Text) ? "" : " ") + text;
            }

            if (!string.IsNullOrWhiteSpace(textBlock.Text))
            {
                stackPanelChildren.Add(textBlock);
            }

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
                }

                // Set up binding
                textBox.Bind(TextBox.TextProperty, new Binding { Source = DataContext, Path = path });

                Watermark ??= displayAttributeName ?? path.SplitAndGetLast('.');
            }

            UpdateVisual();
        }

        private void UpdateVisual()
        {
            textStackPanel.IsVisible = control.IsEnabled && !textBox.IsFocused && !string.IsNullOrWhiteSpace(textBox.Text);
            textStackPanel.VerticalAlignment = !string.IsNullOrWhiteSpace(textBox.Watermark) ? Avalonia.Layout.VerticalAlignment.Bottom : Avalonia.Layout.VerticalAlignment.Center;
        }
    }
}
