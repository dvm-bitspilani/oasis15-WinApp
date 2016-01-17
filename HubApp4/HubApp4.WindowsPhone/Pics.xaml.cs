using HubApp4.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HubApp4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pics : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public Pics()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public Task MyDataManager { get; private set; }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private   void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            try {
                LoadingBar.IsEnabled = true;
                LoadingBar.Visibility = Visibility.Visible;
                                           
                Uri myUri1 = new Uri("http://www.bits-oasis.org/gauss1.jpg", UriKind.Absolute);
                Pic1.Source = new BitmapImage(myUri1);
                Uri myUri2 = new Uri("http://www.bits-oasis.org/gauss2.jpg", UriKind.Absolute);
                Pic2.Source = new BitmapImage(myUri2);
                Uri myUri3 = new Uri("http://www.bits-oasis.org/gauss3.jpg", UriKind.Absolute);
                Pic3.Source = new BitmapImage(myUri3);
                Uri myUri4 = new Uri("http://www.bits-oasis.org/gauss4.jpg", UriKind.Absolute);
                Pic4.Source = new BitmapImage(myUri4);
                
            }
            catch
            {
                
            }
            }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            await Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }
        private void Pic3_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        private async void Pic1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

            picload();
            MessageDialog msg1 = new MessageDialog("Failed to load new image. Check your internet connection");
            await msg1.ShowAsync();
        }

        private async void Pic2_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

            picload();
            MessageDialog msg2 = new MessageDialog("Failed to load new image. Check your internet connection");
            await msg2.ShowAsync();
        }

        private async void Pic3_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

            picload();
            MessageDialog msg3 = new MessageDialog("Failed to load new image. Check your internet connection");
            await msg3.ShowAsync();
        }

        private async void Pic4_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            picload();
            MessageDialog msg4 = new MessageDialog("Failed to load new image. Check your internet connection");
            await msg4.ShowAsync();
        }
        private void picload()
        {
            LoadingBar.IsEnabled = false;
            LoadingBar.Visibility = Visibility.Collapsed;
        }
        #endregion

        
    }
}
