﻿#pragma checksum "C:\Proyectos\W8Apps\CarrouselWizardSample\CarrouselWizardSample\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "431C059D57CAFC4379749B8BF7563A6C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.34014
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using CarrouselWizardSample.Controls;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace CarrouselWizardSample {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ImageGrid;
        
        internal System.Windows.Media.ImageBrush ImageContainer;
        
        internal System.Windows.Controls.StackPanel stkIndicator;
        
        internal CarrouselWizardSample.Controls.BaseRectangle WizardIndicator;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/CarrouselWizardSample;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ImageGrid = ((System.Windows.Controls.Grid)(this.FindName("ImageGrid")));
            this.ImageContainer = ((System.Windows.Media.ImageBrush)(this.FindName("ImageContainer")));
            this.stkIndicator = ((System.Windows.Controls.StackPanel)(this.FindName("stkIndicator")));
            this.WizardIndicator = ((CarrouselWizardSample.Controls.BaseRectangle)(this.FindName("WizardIndicator")));
        }
    }
}
