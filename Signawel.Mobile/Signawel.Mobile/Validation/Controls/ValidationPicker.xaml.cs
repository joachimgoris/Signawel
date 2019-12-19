using System;
using System.Collections.Generic;
using Signawel.Domain.Reports;
using Signawel.Mobile.Constants;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Signawel.Mobile.Validation.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValidationPicker
    {
        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(nameof(ItemSource),
            typeof(List<ReportDefaultIssue>), typeof(ValidationPicker));
        public List<ReportDefaultIssue> ItemSource
        {
            get => (List<ReportDefaultIssue>)GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem),
            typeof(ReportDefaultIssue), typeof(ValidationPicker), null, BindingMode.TwoWay);
        public ReportDefaultIssue SelectedItem
        {
            get => (ReportDefaultIssue)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title),
            typeof(string), typeof(ValidationPicker), default(string));
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty PickerIndexProperty = BindableProperty.Create(nameof(PickerIndex),
            typeof(int), typeof(ValidationPicker), default(int), BindingMode.OneWayToSource);
        public int PickerIndex
        {
            get => (int)GetValue(PickerIndexProperty);
            set => SetValue(PickerIndexProperty, value);
        }

        public ValidationPicker()
        {
            InitializeComponent();

            ValidationPickerPicker.Title = Title;
            ValidationPickerPicker.TextColor = TextColor;
            ValidationPickerLabel.TextColor = ErrorColor;
            ValidationPickerPicker.ItemsSource = ItemSource;
            ValidationPickerPicker.Unfocused += ValidationPickerPickerOnUnfocused;
            ValidationPickerPicker.SelectedIndexChanged += ValidationPickerPickerOnSelectedIndexChanged;
        }

        public void ValidationPickerPickerOnSelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedItem = ValidationPickerPicker.SelectedItem as ReportDefaultIssue;
            ValidationPickerPicker.TextColor = TextColor;
            ValidationPickerLabel.IsVisible = false;
            IsValid = true;
        }

        public void ValidationPickerPickerOnUnfocused(object sender, FocusEventArgs e)
        {
            if (SelectedItem == null)
            {
                ValidationPickerLabel.IsVisible = true;
                ValidationPickerLabel.Text = TextConstants.NoIssueSelected;
                ValidationPickerPicker.TextColor = ErrorColor;
                IsValid = false;
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ItemSourceProperty.PropertyName)
            {
                ValidationPickerPicker.ItemsSource = ItemSource;
            }
            else if (propertyName == TitleProperty.PropertyName)
            {
                ValidationPickerPicker.Title = Title;
            }
            else if (propertyName == ErrorMessageProperty.PropertyName)
            {
                ValidationPickerLabel.Text = ErrorMessage;
            }
            else if (propertyName == ErrorColorProperty.PropertyName)
            {
                ValidationPickerLabel.TextColor = ErrorColor;
            }
            else if (propertyName == TextColorProperty.PropertyName)
            {
                ValidationPickerPicker.TextColor = TextColor;
            } 
            else if (propertyName == SelectedItemProperty.PropertyName)
            {
                ValidationPickerPicker.SelectedItem = SelectedItem;
            } 
            else if (propertyName == PickerIndexProperty.PropertyName)
            {
                ValidationPickerPicker.SelectedIndex = PickerIndex;
            }
        }
    }
}