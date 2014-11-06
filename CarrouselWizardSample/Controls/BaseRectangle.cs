using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CarrouselWizardSample.Controls
{
    public class BaseRectangle : ContentControl
    {
        private readonly Canvas canvas;
        private readonly DoubleAnimation animation;
        private readonly Storyboard storyboard;
        private Border border;

        public int SelectedIndex
        {
            get { return (int)this.GetValue(SelectedIndexProperty); }
            set { this.SetValue(SelectedIndexProperty, value); }
        }

        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(
            "SelectedIndex",
            typeof(int),
            typeof(BaseRectangle),
            new PropertyMetadata(0, new PropertyChangedCallback(OnSelectedIndexChanged)));

        public int ItemsCount
        {
            get { return (int)this.GetValue(ItemsCountProperty); }
            set { this.SetValue(ItemsCountProperty, value); }
        }

        public static readonly DependencyProperty ItemsCountProperty = DependencyProperty.Register(
            "ItemsCount",
            typeof(int),
            typeof(BaseRectangle),
            new PropertyMetadata(1, new PropertyChangedCallback(OnItemsCountChanged)));

        public Color RectangleColor
        {
            get { return (Color) this.GetValue(RectangleColorProperty); }
            set { this.SetValue(RectangleColorProperty, value);}
        }

        public static readonly DependencyProperty RectangleColorProperty = DependencyProperty.Register(
            "RectangleColor", 
            typeof(Color), 
            typeof(BaseRectangle), 
            new PropertyMetadata(Color.FromArgb(255, 119, 95, 137),new PropertyChangedCallback(OnRectangleColorChanged)));

        public BaseRectangle()
        {
            this.canvas = new Canvas
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
            };
            this.Content = this.canvas;

            this.animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(0.5)),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            this.storyboard = new Storyboard();
            this.storyboard.Children.Add(this.animation);

            this.Loaded += new RoutedEventHandler(this.OnCurrentPivotIndicatorLoaded);
        }

        private void OnCurrentPivotIndicatorLoaded(object sender, RoutedEventArgs e)
        {
            this.RebuildIndicator();
        }

        private void RebuildIndicator()
        {
            double actualWidth = base.ActualWidth;
            double actualHeight = base.ActualHeight;

            this.canvas.Width = actualWidth;
            this.canvas.Height = actualHeight;
            this.canvas.Children.Clear();

            var translateTransform = new TranslateTransform();
            this.border = new Border
            {
                Width = (int)(actualWidth / ((double)this.ItemsCount)),
                Height = actualHeight,
                BorderBrush = new SolidColorBrush(this.RectangleColor),
                BorderThickness = new Thickness(0.0, actualHeight, 0.0, 0.0),
                RenderTransform = translateTransform,
                CacheMode = new BitmapCache()
            };

            this.storyboard.Stop();

            // animate TranlateTransform.X property
            // not that using TranslateTransform matters, a Canvas.Left animation for example will
            // have less good performance because the compositor thread will not handle it
            Storyboard.SetTarget(this.animation, translateTransform);
            Storyboard.SetTargetProperty(this.animation, new PropertyPath(TranslateTransform.XProperty));

            this.canvas.Children.Add(this.border);

            this.ChangeSelectedIndex(0, this.SelectedIndex);
        }

        private void ChangeSelectedIndex(int oldIndex, int newIndex)
        {
            if (this.border != null)
            {
                double width = this.border.Width;
                this.animation.From = width * oldIndex;
                this.animation.To = width * newIndex;
                this.storyboard.Begin();
            }
        }

        private void OnRectangleColorChanged(DependencyPropertyChangedEventArgs e)
        {
            this.RebuildIndicator();
        }

        private void OnSelectedIndexChanged(DependencyPropertyChangedEventArgs e)
        {
            this.ChangeSelectedIndex((int)e.OldValue, (int)e.NewValue);
        }

        private void OnItemsCountChanged(DependencyPropertyChangedEventArgs e)
        {
            this.RebuildIndicator();
        }

        private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BaseRectangle)d).OnSelectedIndexChanged(e);
        }

        private static void OnItemsCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BaseRectangle)d).OnItemsCountChanged(e);
        }

        private static void OnRectangleColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BaseRectangle)d).OnRectangleColorChanged(e);
        }
    }
}
