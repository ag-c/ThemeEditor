﻿using System;
using Avalonia;
using ThemeEditor.Colors;

namespace ThemeEditor.Controls.ColorPicker
{
    public class RgbProperties : ColorPickerProperties
    {
        public static readonly StyledProperty<byte> RedProperty =
            AvaloniaProperty.Register<RgbProperties, byte>(nameof(Red), 0xFF, validate: ValidateRed);

        public static readonly StyledProperty<byte> GreenProperty =
            AvaloniaProperty.Register<RgbProperties, byte>(nameof(Green), 0x00, validate: ValidateGreen);

        public static readonly StyledProperty<byte> BlueProperty =
            AvaloniaProperty.Register<RgbProperties, byte>(nameof(Blue), 0x00, validate: ValidateBlue);

        private static byte ValidateRed(RgbProperties cp, byte red)
        {
            if (red < 0 || red > 255)
            {
                throw new ArgumentException("Invalid Red value.");
            }
            return red;
        }

        private static byte ValidateGreen(RgbProperties cp, byte green)
        {
            if (green < 0 || green > 255)
            {
                throw new ArgumentException("Invalid Green value.");
            }
            return green;
        }

        private static byte ValidateBlue(RgbProperties cp, byte blue)
        {
            if (blue < 0 || blue > 255)
            {
                throw new ArgumentException("Invalid Blue value.");
            }
            return blue;
        }

        private bool _updating = false;

        public RgbProperties() : base()
        {
            this.GetObservable(RedProperty).Subscribe(x => UpdateColorPickerValues());
            this.GetObservable(GreenProperty).Subscribe(x => UpdateColorPickerValues());
            this.GetObservable(BlueProperty).Subscribe(x => UpdateColorPickerValues());
        }

        public byte Red
        {
            get { return GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }

        public byte Green
        {
            get { return GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }

        public byte Blue
        {
            get { return GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }

        public override void UpdateColorPickerValues()
        {
            if (_updating == false && ColorPicker != null)
            {
                _updating = true;
                RGB rgb = new RGB(Red, Green, Blue);
                HSV hsv = rgb.ToHSV();
                ColorPicker.Value1 = hsv.H;
                ColorPicker.Value2 = hsv.S;
                ColorPicker.Value3 = hsv.V;
                _updating = false;
            }
        }

        public override void UpdatePropertyValues()
        {
            if (_updating == false && ColorPicker != null)
            {
                _updating = true;
                HSV hsv = new HSV(ColorPicker.Value1, ColorPicker.Value2, ColorPicker.Value3);
                RGB rgb = hsv.ToRGB();
                Red = (byte)rgb.R;
                Green = (byte)rgb.G;
                Blue = (byte)rgb.B;
                _updating = false;
            }
        }
    }
}