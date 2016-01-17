using HubApp4.Common;
using HubApp4.Data;


using System;
using System.Net;
using Windows.ApplicationModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Calls;
using Windows.ApplicationModel.Email;
using Windows.UI.Popups;
using System.Globalization;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;


// The Hub Application template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace HubApp4
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class HubPage : Page
    {
        private readonly NavigationHelper navigationHelper;
        private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();
        private readonly ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView("Resources");

        public HubPage()
        {
            this.InitializeComponent();

            // Hub is only supported in Portrait orientation
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            reviewfunction();
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
       
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            try {
                
                var sampleDataGroups = await SampleDataSource.GetGroupsAsync();
                this.DefaultViewModel["Groups"] = sampleDataGroups;
                
            }
            catch { }
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
            // TODO: Save the unique state of the page here.
        }

        /// <summary>
        /// Shows the details of a clicked group in the <see cref="SectionPage"/>.
        /// </summary>
        private void GroupSection_ItemClick(object sender, ItemClickEventArgs e)
        {
            var groupId = ((SampleDataGroup)e.ClickedItem).UniqueId;
            if (!Frame.Navigate(typeof(SectionPage), groupId))
            {
                throw new Exception(this.resourceLoader.GetString("NavigationFailedExceptionMessage"));
            }
        }

        /// <summary>
        /// Shows the details of an item clicked on in the <see cref="ItemPage"/>
        /// </summary>
        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            try
            {
                var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
                if (!Frame.Navigate(typeof(ItemPage), itemId))
                {
                    throw new Exception(this.resourceLoader.GetString("NavigationFailedExceptionMessage"));
                }
            }
            catch { }
        }
        public async void reviewfunction()
        {

            //For windows phone 8.1 app or universal app use the following line of code
            var settings = Windows.Storage.ApplicationData.Current.LocalSettings;


            //set the app name
            string Appname = "Oasis 2015 - BITS Pilani";

            if (!settings.Values.ContainsKey("review"))
            {
                settings.Values.Add("review", 1);
                settings.Values.Add("rcheck", 0);
            }
            else
            {
                int no = Convert.ToInt32(settings.Values["review"]);
                int check = Convert.ToInt32(settings.Values["rcheck"]);
                no++;
                if ((no == 4 || no == 6 || no % 10 == 0) && check == 0)
                {
                    settings.Values["review"] = no;
                    MessageDialog mydial = new MessageDialog("Thank you for using this application.\nWould you like to give some time to rate and review this application ");
                    mydial.Title = Appname;
                    mydial.Commands.Add(new UICommand(
                        "Yes",
                        new UICommandInvokedHandler(this.CommandInvokedHandler_yesclick)));
                    mydial.Commands.Add(new UICommand(
                       "No",
                       new UICommandInvokedHandler(this.CommandInvokedHandler_noclick)));
                    await mydial.ShowAsync();

                }
                else
                {
                    settings.Values["review"] = no;
                }
            }
        }

        private void CommandInvokedHandler_noclick(IUICommand command)
        {

        }

        private async void CommandInvokedHandler_yesclick(IUICommand command)
        {
            var settings = Windows.Storage.ApplicationData.Current.LocalSettings;
            settings.Values["rcheck"] = 1;
            var appId = Windows.ApplicationModel.Store.CurrentApp.AppId;
            //add your app id here
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + appId));
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
        /// <param name="e">Event data that describes how this page was reached.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            //StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            //statusBar.BackgroundColor =//Colors.Black;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion



        private void EventView_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
                Frame.Navigate(typeof(EventItemPage), itemId);
                

            }
            catch { }
            }





        async void DefaultLaunch(string uriToLaunch)
        {
            // Launch the URI
            try
            {
                var uri = new Uri(uriToLaunch);
                var success = await Windows.System.Launcher.LaunchUriAsync(uri);
            }
            catch { }
        }

       
 

        private void MapButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Map));
        }

        private void ScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Schedule));
        }
        private void EventsNow_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(eventnow));
        }
        private void favButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Favourite));
        }
        private void KnowId_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(know_your_id));
        }
        private void Results_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(results));
        }


       
        private void Pcr_Tapped(object sender, TappedRoutedEventArgs e)
        {

            string number = "+917240105156";
            string name = "Maheep Tripathi";
            PhoneCallManager.ShowPhoneCallUI(number, name);
        }
        private void Dvm_Tapped(object sender, TappedRoutedEventArgs e)
        {

            string number = "+919810885196";
            string name = "Siddhant Tuli";
            PhoneCallManager.ShowPhoneCallUI(number, name);
        }

        private void Sponz_Tapped(object sender, TappedRoutedEventArgs e)
        {

            string number = "+919772231910";
            string name = "Ojas Malpani";
            PhoneCallManager.ShowPhoneCallUI(number, name);
        }

        private void Controls_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string number = "+918441000746";
            string name = "Krishna Akhil";
            PhoneCallManager.ShowPhoneCallUI(number, name);
        }

        private void Rec_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string number = "+919772048822";
            string name = "Saketh Boddu";
            PhoneCallManager.ShowPhoneCallUI(number, name);
        }

        private void Adp_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string number = "+919660570469";
            string name = "Krishna Chaitanya";
            PhoneCallManager.ShowPhoneCallUI(number, name);
        }

        private void Stage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string number = "+918741064850";
            string name = "Akshansh Deva";
            PhoneCallManager.ShowPhoneCallUI(number, name);
        }

        

        private async void DvmEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Name = "Siddhant Tuli",
                Address = "webmaster@bits-oasis.org"
            };
            EmailMessage mail = new EmailMessage();
            mail.Subject = "OASIS 2015";
            mail.Body = "";
            mail.To.Add(sendTo);
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

       

        private async void PcrEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Name = "Maheep Tripathi",
                Address = "pcr@bits-oasis.org"
            };
            EmailMessage mail = new EmailMessage();
            mail.Subject = "OASIS 2015";
            mail.Body = "";
            mail.To.Add(sendTo);
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private async void SponzEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Name = "Ojas Malpani",
                Address = "sponsorship@bits-oasis.org"
            };
            EmailMessage mail = new EmailMessage();
            mail.Subject = "OASIS 2015";
            mail.Body = "";
            mail.To.Add(sendTo);
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private async void ControlsEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Name = "Krishna Akhil",
                Address = "controls@bits-oasis.org"
            };
            EmailMessage mail = new EmailMessage();
            mail.Subject = "OASIS 2015";
            mail.Body = "";
            mail.To.Add(sendTo);
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private async void StageEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Name = "Akshansh Deva",
                Address = "stagecontrols@bits-oasis.org"
            };
           /* EmailRecipient sendTo1 = new EmailRecipient()
            {
                Name = "Archit Gadhok(Guest Lectures)",
                Address = "guestlectures@bits-apogee.org"
            };*/
            EmailMessage mail = new EmailMessage();
            mail.Subject = "OASIS 2015";
            mail.Body = "";
            mail.To.Add(sendTo);
           // mail.Bcc.Add(sendTo1);
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private async void RecEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Name = "Saketh Boddu",
                Address = "recnacc@bits-oasis.org"
            };
            EmailMessage mail = new EmailMessage();
            mail.Subject = "OASIS 2015";
            mail.Body = "";
            mail.To.Add(sendTo);
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private async void AdpEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Name = "Krishna Chaitanya",
                Address = "adp@bits-oasis.org"
            };
            EmailMessage mail = new EmailMessage();
            mail.Subject = "OASIS 2015";
            mail.Body = "";
            mail.To.Add(sendTo);
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        

    
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchPage));
        }
        private void About_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutTheFest));
        }
        private void Parti_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(know_your_id));
        }
        private void Sponsors_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string uriToLaunch = @"http://bits-oasis.org/sponsors/";
            DefaultLaunch(uriToLaunch);
        }

        private void Pics_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pics));
        }
        private async void Rate_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            await Windows.System.Launcher.LaunchUriAsync( new Uri("ms-windows-store:reviewapp?appid=" + CurrentApp.AppId));
        }
        private void Developers_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(developers));
        }

        private void EngPress_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string uriToLaunch = @"http://bits-oasis.org/blogs/english/";
            DefaultLaunch(uriToLaunch);
        }
        private void fb_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string uriToLaunch = @"https://www.facebook.com/oasis.bitspilani";
            DefaultLaunch(uriToLaunch);
        }
        private void Twitter_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string uriToLaunch = @"https://twitter.com/bitsoasis";
            DefaultLaunch(uriToLaunch);
        }
        private void Youtube_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string uriToLaunch = @"https://www.youtube.com/user/OasisBITS";
            DefaultLaunch(uriToLaunch);
        }

        private void Results_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(results));
        }

        private void workshop_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string uriToLaunch = @"http://www.bits-oasis.org/workshops";
            DefaultLaunch(uriToLaunch);
        }
    }
}
