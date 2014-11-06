using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CarrouselWizardSample.Resources;
using Microsoft.Xna.Framework.Input.Touch;
using System.Threading.Tasks;
using GestureEventArgs = Microsoft.Phone.Controls.GestureEventArgs;

namespace CarrouselWizardSample
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        int currentIndexOriginal = 0;
        int currentIndex = 0;
        TouchPoint _first;
        private List<String> htmls = new List<String>();

        List<String> ImagesList = new List<string>();

        public MainPage()
        {
            InitializeComponent();
            LoadImages();
            WizardIndicator.ItemsCount = ImagesList.Count;
            WizardIndicator.SelectedIndex = 0;

            TouchPanel.EnabledGestures = GestureType.HorizontalDrag;
            NavigationInTransition navigateInTransition = new NavigationInTransition();
            navigateInTransition.Backward = new TurnstileTransition { Mode = TurnstileTransitionMode.BackwardIn };
            navigateInTransition.Forward = new TurnstileTransition { Mode = TurnstileTransitionMode.ForwardIn };

            NavigationOutTransition navigateOutTransition = new NavigationOutTransition();
            navigateOutTransition.Backward = new TurnstileTransition { Mode = TurnstileTransitionMode.BackwardOut };
            navigateOutTransition.Forward = new TurnstileTransition { Mode = TurnstileTransitionMode.ForwardOut };
            TransitionService.SetNavigationInTransition(this, navigateInTransition);
            TransitionService.SetNavigationOutTransition(this, navigateOutTransition);
            Touch.FrameReported += Touch_FrameReported;

            
        }

        private void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {

            TouchPoint mainTouch = e.GetPrimaryTouchPoint(null);
            Microsoft.Phone.Controls.PageOrientation currentOrientation = (App.Current.RootVisual as PhoneApplicationFrame).Orientation;
            int numPivotItems = htmls.Count;

            if (mainTouch.Action == TouchAction.Down)
                _first = mainTouch;
            else if (mainTouch.Action == TouchAction.Up && _first != null)
            {
                currentIndexOriginal = currentIndex;
                var xDelta = mainTouch.Position.X - _first.Position.X;
                var yDelta = Math.Abs(mainTouch.Position.Y - _first.Position.Y);
                var yDeltaLand = mainTouch.Position.Y - _first.Position.Y;
                bool movEsquerra = true;

                if (yDelta > Math.Abs(xDelta)) return;
                if (Math.Abs(xDelta - 0) > 30)
                {
                    if (xDelta < 0)
                    {
                        movEsquerra = true;
                        if (currentIndex < WizardIndicator.ItemsCount-1)
                        {
                            currentIndex++;
                        }
                    }
                    else
                    {
                        movEsquerra = false;
                        if (currentIndex > 0)
                        {
                            currentIndex--;
                        }
                    }
                }

                WizardIndicator.SelectedIndex = currentIndex;
                if (currentIndexOriginal != currentIndex)
                {
                    if (movEsquerra)
                    {
                        SlideTransition slideIni = new SlideTransition();
                        slideIni.Mode = SlideTransitionMode.SlideLeftFadeOut;
                        ITransition transitionIni = slideIni.GetTransition(ImageGrid);
                        transitionIni.Completed += transition_CompletedIzquierdaDerecha;
                        transitionIni.Begin();
                    }
                    else
                    {
                        SlideTransition slideIni = new SlideTransition();
                        slideIni.Mode = SlideTransitionMode.SlideRightFadeOut;
                        ITransition transitionIni = slideIni.GetTransition(ImageGrid);
                        transitionIni.Completed += transition_CompletedDerechaIzquierda;
                        transitionIni.Begin();
                    }

                }

            }
        }

        private async void transition_CompletedDerechaIzquierda(object sender, EventArgs e)
        {
            ITransition transition = (ITransition)sender;
            //Gestión de los botones interactivos sobre las imágenes. 

            //Cambiar Imagen
            ImageContainer.ImageSource = new BitmapImage(new Uri(ImagesList[currentIndex], UriKind.RelativeOrAbsolute));
            
            await Task.Delay(150);
            transition.Stop();

            SlideTransition slide = new SlideTransition();
            slide.Mode = SlideTransitionMode.SlideRightFadeIn;

            ITransition transitionfi = slide.GetTransition(ImageGrid);
            transitionfi.Completed += delegate
            {
                transitionfi.Stop();
            };

            transitionfi.Begin();
        }

        public async void transition_CompletedIzquierdaDerecha(object sender, EventArgs e)
        {
            ITransition transition = (ITransition)sender;

            //Cambiar Imagen
            ImageContainer.ImageSource = new BitmapImage(new Uri(ImagesList[currentIndex], UriKind.RelativeOrAbsolute));
            await Task.Delay(150);
            transition.Stop();

            SlideTransition slide = new SlideTransition();
            slide.Mode = SlideTransitionMode.SlideLeftFadeIn;

            ITransition transitionfi = slide.GetTransition(ImageGrid);
            transitionfi.Completed += delegate
            {
                transitionfi.Stop();
            };

            transitionfi.Begin();
        }

        private void Exit()
        {
            Touch.FrameReported -= Touch_FrameReported;
            /*TODO: EXIT EVENTS*/
        }


        private void btnAbrirTutorial_Click(object sender, RoutedEventArgs e)
        {
            currentIndex = 1;
            WizardIndicator.SelectedIndex = currentIndex;
            SlideTransition slideIni = new SlideTransition();
            slideIni.Mode = SlideTransitionMode.SlideLeftFadeOut;

            ITransition transitionIni = slideIni.GetTransition(ImageGrid);
            transitionIni.Completed += transition_CompletedIzquierdaDerecha;
            transitionIni.Begin();
        }

        private void LoadImages()
        {
            ImagesList = new List<string>()
            {
                "/Assets/Guide/0.png",
                "/Assets/Guide/1.png",
                "/Assets/Guide/2.png",
                "/Assets/Guide/3.png",
                "/Assets/Guide/4.png"
            };
            ImageContainer.ImageSource = new BitmapImage(new Uri(ImagesList[currentIndex], UriKind.RelativeOrAbsolute));
        }
    }
}