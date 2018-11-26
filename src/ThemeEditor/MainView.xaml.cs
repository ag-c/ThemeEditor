﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using ThemeEditor.ViewModels;
using ThemeEditor.Views;

namespace ThemeEditor
{
    public class MainView : UserControl
    {
        private ThemePreviewView _previewView = null;
        private TextBox _exportText = null;
        private ThemeEditorView _editorView = null;
        private DropDown _themeSelector = null;
        private StyleInclude _lightTheme = null;
        private StyleInclude _darkTheme = null;

        public MainView()
        {
            this.InitializeComponent();
            _previewView = this.Find<ThemePreviewView>("previewView");
            _exportText = this.Find<TextBox>("exportText");
            _editorView = this.Find<ThemeEditorView>("editorView");
            _themeSelector = this.Find<DropDown>("themeSelector");
            _themeSelector.SelectionChanged += (sender, e) =>
            {
                switch (_themeSelector.SelectedIndex)
                {
                    case 0:
                        Styles[0] = _lightTheme;
                        break;
                    case 1:
                        Styles[0] = _darkTheme;
                        break;
                }
            };
            _lightTheme = AvaloniaXamlLoader.Parse<StyleInclude>(@"<StyleInclude xmlns='https://github.com/avaloniaui' Source='avares://Avalonia.Themes.Default/Accents/BaseLight.xaml'/>");
            _darkTheme = AvaloniaXamlLoader.Parse<StyleInclude>(@"<StyleInclude xmlns='https://github.com/avaloniaui' Source='avares://Avalonia.Themes.Default/Accents/BaseDark.xaml'/>");
            Styles.Add(_darkTheme);
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);

            if (DataContext is ThemeEditorViewModel editor)
            {
                var defaultThem = editor.GetTheme(_lightTheme);
                defaultThem.Name = "BaseLight";
                editor.DefaultTheme = defaultThem;
                editor.Attach(_previewView.Resources, (x) => _exportText.Text = x.ToXaml());
            }
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromVisualTree(e);

            if (DataContext is ThemeEditorViewModel editor)
            {
                editor.Detach();
            }
        }
    }
}
