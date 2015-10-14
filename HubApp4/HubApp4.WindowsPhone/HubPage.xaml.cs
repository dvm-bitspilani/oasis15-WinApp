using HubApp4.Common;
using HubApp4.Data;

using System;
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
            var sampleDataGroups = await SampleDataSource.GetGroupsAsync();
            this.DefaultViewModel["Groups"] = sampleDataGroups;
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
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            if (!Frame.Navigate(typeof(ItemPage), itemId))
            {
                throw new Exception(this.resourceLoader.GetString("NavigationFailedExceptionMessage"));
            }
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
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion



        private void EventView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            if (!Frame.Navigate(typeof(EventItemPage), itemId))
            {
                throw new Exception(this.resourceLoader.GetString("NavigationFailedExceptionMessage"));
            }

        }

        private void SponsorsButton_Click(object sender, RoutedEventArgs e)
        {
            string uriToLaunch = @"http://bits-apogee.org/fallback/";
            DefaultLaunch(uriToLaunch);

        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {

        }
        async void DefaultLaunch(string uriToLaunch)
        {
            // Launch the URI

            var uri = new Uri(uriToLaunch);
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private void AboutTheFest_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutTheFest));
        }

        private void Prez_Tapped(object sender, TappedRoutedEventArgs e)
        {

            string number = "+917891799999";
            string name = "V.V.Sai Praneeth";
            PhoneCallManager.ShowPhoneCallUI(number, name);
        }


        private void RateReviewButton_Click(object sender, RoutedEventArgs e)
        {
            // await Windows.System.Launcher.LaunchUriAsync( new Uri("ms-windows-store:reviewapp?appid=[]" + CurrentApp.AppId));


        }

        private void MapButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Map));
        }

        private void ScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Schedule));
        }

        private void PartiButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Parti));
        }

        private void GenSec_Tapped(object sender, TappedRoutedEventArgs e)
        {

            string number = "+917737485915";
            string name = "Ashutosh Ajay Mundhada";
            PhoneCallManager.ShowPhoneCallUI(number, name);
        }

        private void Sponz_Tapped(object sender, TappedRoutedEventArgs e)
        {

            string number = "+919772227757";
            string name = "Sumantra Sharma";
            PhoneCallManager.ShowPhoneCallUI(number, name);
        }

        private void Pcr_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string number = "+919772064316";
            string name = "Mayank Juneja";
            PhoneCallManager.ShowPhoneCallUI(number, name);
        }

        private void Rec_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string number = "+919636946278";
            string name = "Akshay Singh Bisht";
            PhoneCallManager.ShowPhoneCallUI(number, name);
        }

        private void Adp_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string number = "+919660575881";
            string name = "Apoorva Pakhle";
            PhoneCallManager.ShowPhoneCallUI(number, name);
        }

        private void Pep_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string number = "+919772221902";
            string name = "Archit Gadhok";
            PhoneCallManager.ShowPhoneCallUI(number, name);
        }

        private void Controls_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string number = "+918854877550";
            string name = "Vidushi Dwivedi";
            PhoneCallManager.ShowPhoneCallUI(number, name);
        }

        private async void GenSecEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Name = "Ashutosh Ajay Mundhada",
                Address = "gensec@pilani.bits-pilani.ac.in"
            };
            EmailMessage mail = new EmailMessage();
            mail.Subject = "APOGEE 2015";
            mail.Body = "";
            mail.To.Add(sendTo);
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private void Dvm_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string number = "+919772227370";
            string name = "Sahib Singh Dhanjal";
            PhoneCallManager.ShowPhoneCallUI(number, name);
        }

        private async void PrezEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Name = "V.V.Sai Praneeth",
                Address = "president@pilani.bits-pilani.ac.in"
            };
            EmailMessage mail = new EmailMessage();
            mail.Subject = "APOGEE 2015";
            mail.Body = "";
            mail.To.Add(sendTo);
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private async void DvmEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Name = "Sahib Singh Dhanjal",
                Address = "dvm@bits-apogee.org"
            };
            EmailMessage mail = new EmailMessage();
            mail.Subject = "APOGEE 2015";
            mail.Body = "";
            mail.To.Add(sendTo);
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private async void ControlsEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Name = "Vidushi Dwivedi",
                Address = "controls@bits-apogee.org"
            };
            EmailMessage mail = new EmailMessage();
            mail.Subject = "APOGEE 2015";
            mail.Body = "";
            mail.To.Add(sendTo);
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private async void PepEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Name = "Archit Gadhok",
                Address = "pep@bits-apogee.org"
            };
            EmailRecipient sendTo1 = new EmailRecipient()
            {
                Name = "Archit Gadhok(Guest Lectures)",
                Address = "guestlectures@bits-apogee.org"
            };
            EmailMessage mail = new EmailMessage();
            mail.Subject = "APOGEE 2015";
            mail.Body = "";
            mail.To.Add(sendTo);
            mail.Bcc.Add(sendTo1);
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private async void RecEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Name = "Akshay Singh Bisht",
                Address = "reccnacc@bits-apogee.org"
            };
            EmailMessage mail = new EmailMessage();
            mail.Subject = "APOGEE 2015";
            mail.Body = "";
            mail.To.Add(sendTo);
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private async void AdpEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Name = "Apoorva Pakhle",
                Address = "adp@bits-apogee.org"
            };
            EmailMessage mail = new EmailMessage();
            mail.Subject = "APOGEE 2015";
            mail.Body = "";
            mail.To.Add(sendTo);
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private async void PcrEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Name = "Mayank Juneja",
                Address = "pcr@bits-apogee.org"
            };
            EmailMessage mail = new EmailMessage();
            mail.Subject = "APOGEE 2015";
            mail.Body = "";
            mail.To.Add(sendTo);
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private async void SponzEmail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Name = "Sumantra Sharma",
                Address = "sponsorship@bits-apogee.org"
            };
            EmailMessage mail = new EmailMessage();
            mail.Subject = "APOGEE 2015";
            mail.Body = "";
            mail.To.Add(sendTo);
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private void develop_click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(developers));
        }





    }
}
