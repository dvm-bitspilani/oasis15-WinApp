using HubApp4.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HubApp4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class know_your_id : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public know_your_id()
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
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try {
                //HttpClient clientOb = new HttpClient();
                //Uri connectionUrl = new Uri("http://mydomain.com/request.php");
                //string pairs = ID.Text;
                //HttpStringContent formcontent = new HttpStringContent(pairs, 0);

                //HttpResponseMessage response = await clientOb.PostAsync(connectionUrl, formcontent);
                //if (response.IsSuccessStatusCode)
                //{
                Windows.Web.Http.HttpClient client = new Windows.Web.Http.HttpClient();
               
                var jsonText = await client.GetStringAsync(new Uri("http://www.bits-oasis.org/2015/pcode_json/?email="+ID.Text));
                //if(jsonText.Length==0)
                //{
                //    var dialog1 = new MessageDialog("ID not found");
                //    await dialog1.ShowAsync();
                //}
                jsonText =jsonText.Substring(11);
                
                int l = jsonText.Length;
                l = l - 2;
                jsonText= jsonText.Remove(l);
                //{ "pcode": "car5417"}
                if (jsonText.CompareTo("0") == 0)
                {
                    var dialog1 = new MessageDialog("ID not found");
                    await dialog1.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("Your ID is - " + jsonText);
                    await dialog.ShowAsync();
                }
            }
            catch { }
            }

        private void ID_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (ID.Text == "Enter E-mail ID Here")
                ID.Text = "";
        }
    }


}

