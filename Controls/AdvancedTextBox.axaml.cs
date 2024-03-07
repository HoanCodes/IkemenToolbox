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
        private IDisposable _binding;
        public BindingBase TextBinding { get; set; }
        public bool HasToolTip { get; set; } = true;
        public bool IsToolTipVisible
        {
            get => GetValue(IsToolTipVisibleProperty);
            set => SetValue(IsToolTipVisibleProperty, value);
        }
        public bool HasInlineToolTip
        {
            get => GetValue(HasInlineToolTipProperty);
            set => SetValue(HasInlineToolTipProperty, value);
        }
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
            get => GetValue(TextBoxForegroundProperty);
            set => SetValue(TextBoxForegroundProperty, value);
        }

        public static readonly StyledProperty<bool> IsToolTipVisibleProperty =
        AvaloniaProperty.Register<AdvancedTextBox, bool>(nameof(IsToolTipVisible));

        public static readonly StyledProperty<bool> HasInlineToolTipProperty =
        AvaloniaProperty.Register<AdvancedTextBox, bool>(nameof(HasInlineToolTip));

        public static readonly StyledProperty<string> WatermarkProperty =
        AvaloniaProperty.Register<AdvancedTextBox, string>(nameof(Watermark));

        public static readonly StyledProperty<IBrush> BorderBackgroundProperty =
        AvaloniaProperty.Register<AdvancedTextBox, IBrush>(nameof(BorderBackground));

        public static readonly StyledProperty<IBrush> TextBoxBackgroundProperty =
        AvaloniaProperty.Register<AdvancedTextBox, IBrush>(nameof(TextBoxBackground));

        public static readonly StyledProperty<IBrush> TextBoxForegroundProperty =
        AvaloniaProperty.Register<AdvancedTextBox, IBrush>(nameof(TextBoxForeground), defaultValue: Brushes.White);

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
            textBox.TextChanged += (_, __) => FormatText();

            // Format Text when Control loses focus
            textBox.LostFocus += (_, __) => FormatText();

        }

        private void FocusTextBox()
        {
            textBox.Focus();
            textBox.CaretIndex = textBox.Text != null ? textBox.Text.Length : 0;
            UpdateVisual();
        }

        //Format text here
        private void FormatText()
        {
            if (textBox.IsFocused)
            {
                return;
            }

            var stackPanelChildren = textStackPanel.Children;
            stackPanelChildren.Clear();

            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (!HasInlineToolTip)
                {
                    stackPanelChildren.Add(new TextBlock { Text = textBox.Text });
                }
                else
                {
                    var textBlock = new TextBlock();

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
                }
            }

            UpdateVisual();
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);

            if (TextBinding is CompiledBindingExtension binding)
            {
                var path = binding.Path.ToString();

                // Set up binding (Dispose previous binding first, else a null value will overwrite the existing values)
                _binding?.Dispose();
                _binding = textBox.Bind(TextBox.TextProperty, new Binding { Source = DataContext, Path = path });

                var property = DataContext?.GetType().GetProperty(path);
                var display = (DisplayAttribute)(property?.GetCustomAttributes(false).FirstOrDefault(x => x is DisplayAttribute));

                // Generate Watermark
                Watermark ??= display?.Name ?? path.SplitAndGetLast('.');

                // Generate ToolTip
                if (HasToolTip)
                {
                    if (!string.IsNullOrWhiteSpace(display?.Description))
                    {
                        IsToolTipVisible = true;
                        ToolTip.SetTip(toolTipBorder, display.Description);
                    }
                    else
                    {
                        IsToolTipVisible = false;
                    }
                }
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
