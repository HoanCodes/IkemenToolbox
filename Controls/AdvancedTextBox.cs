using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml.MarkupExtensions;
using IkemenToolbox.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IkemenToolbox.Controls
{
    public class AdvancedTextBox : TextBox
    {
        public BindingBase TextBinding { get; set; }
        public string BindingPath { get; set; }
        protected override Type StyleKeyOverride => typeof(TextBox);
        protected override void OnSizeChanged(SizeChangedEventArgs e)
        {
            base.OnSizeChanged(e);
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);

            if (TextBinding != null && TextBinding is CompiledBindingExtension binding)
            {
                var path = binding.Path.ToString();

                // Automatically set Watermark
                if (string.IsNullOrWhiteSpace(Watermark))
                {
                    Watermark = path.SplitAndGetLast('.');
                }

                var property = DataContext?.GetType().GetProperty(path);
                if (property != null)
                {
                    // Automatically set Tooltip
                    var attribute = property.GetCustomAttributes(false).FirstOrDefault(x => x is DisplayAttribute);
                    if (attribute != null)
                    {
                        var display = (DisplayAttribute)attribute;
                        ToolTip.SetTip(this, display.Description);

                        if (!string.IsNullOrWhiteSpace(display.Name))
                        {
                            Watermark = display.Name;
                        }
                    }

                    // Set up binding
                    this.Bind(TextProperty, new Binding { Source = DataContext, Path = path });
                }
            }
        }
    }
}
