﻿using System;
using Avalonia;

namespace ThemeEditor.Controls.ColorPicker
{
    public class HsvProperties : ColorPickerProperties
    {
        public static readonly StyledProperty<double> HueProperty =
            AvaloniaProperty.Register<HsvProperties, double>(nameof(Hue), 0.0, validate: ValidateHue);

        public static readonly StyledProperty<double> SaturationProperty =
            AvaloniaProperty.Register<HsvProperties, double>(nameof(Saturation), 100.0, validate: ValidateSaturation);

        public static readonly StyledProperty<double> ValueProperty =
            AvaloniaProperty.Register<HsvProperties, double>(nameof(Value), 100.0, validate: ValidateValue);

        private static double ValidateHue(HsvProperties cp, double hue)
        {
            if (hue < 0.0 || hue > 360.0)
            {
                throw new ArgumentException("Invalid Hue value.");
            }
            return hue;
        }

        private static double ValidateSaturation(HsvProperties cp, double saturation)
        {
            if (saturation < 0.0 || saturation > 100.0)
            {
                throw new ArgumentException("Invalid Saturation value.");
            }
            return saturation;
        }

        private static double ValidateValue(HsvProperties cp, double value)
        {
            if (value < 0.0 || value > 100.0)
            {
                throw new ArgumentException("Invalid Value value.");
            }
            return value;
        }

        private bool _updating = false;

        public HsvProperties() : base()
        {
            this.GetObservable(HueProperty).Subscribe(x => UpdateColorPickerValues());
            this.GetObservable(SaturationProperty).Subscribe(x => UpdateColorPickerValues());
            this.GetObservable(ValueProperty).Subscribe(x => UpdateColorPickerValues());
        }

        public double Hue
        {
            get { return GetValue(HueProperty); }
            set { SetValue(HueProperty, value); }
        }

        public double Saturation
        {
            get { return GetValue(SaturationProperty); }
            set { SetValue(SaturationProperty, value); }
        }

        public double Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public override void UpdateColorPickerValues()
        {
            if (_updating == false && ColorPicker != null)
            {
                _updating = true;
                ColorPicker.Value1 = Hue;
                ColorPicker.Value2 = Saturation;
                ColorPicker.Value3 = Value;
                _updating = false;
            }
        }

        public override void UpdatePropertyValues()
        {
            if (_updating == false && ColorPicker != null)
            {
                _updating = true;
                Hue = ColorPicker.Value1;
                Saturation = ColorPicker.Value2;
                Value = ColorPicker.Value3;
                _updating = false;
            }
        }
    }
}