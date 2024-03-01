using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;
using System;
namespace IkemenToolbox.Controls
{
    public class ImprovedTextBox : TextBox
    {
        public static readonly StyledProperty<string> ActualWatermarkProperty =
        AvaloniaProperty.Register<ImprovedTextBox, string>(nameof(ActualWatermark));

        public string ActualWatermark
        {
            get => GetValue(ActualWatermarkProperty);
            set => SetValue(ActualWatermarkProperty, value);
        }
        protected override Type StyleKeyOverride => typeof(TextBox);
        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);
            ActualWatermark ??= Watermark;
            UpdateWatermark();
        }

        //Move Data Validation error message to the Watermark
        protected override void UpdateDataValidation(AvaloniaProperty _, BindingValueType __, Exception error) => UpdateWatermark(error?.Message);

        private void UpdateWatermark(string error = null)
        {
            if (error != null)
            {
                BorderBrush = Brushes.Red;
                Watermark = ActualWatermark + $"({error})";
            }
            else
            {
                BorderBrush = Brushes.Transparent;
                Watermark = ActualWatermark;
            }
        }
    }
}
