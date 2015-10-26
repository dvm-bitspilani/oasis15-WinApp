﻿using HubApp4.Common;
using HubApp4.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HubApp4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class eventnow : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public eventnow()
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
        private async Task evntnow()
        {
            CultureInfo provider = new CultureInfo("es-ES");
            var subitemDet = await SampleDataSource.GetGroupAsync("Schedule");
            schedulednotif n = new schedulednotif();
            int noOfItems = subitemDet.Items.Count;
            string date = "25/10/2015";
            DateTime ddt = DateTime.Now;
            List<SampleDataSubItem> evobj = new List<SampleDataSubItem>();

            for (int i = 0; i < noOfItems; i++)
            {
                int noOfsubitems = subitemDet.Items[i].SubItems.Count;
                MessageDialog msgbox4 = new MessageDialog(noOfsubitems.ToString());
                await msgbox4.ShowAsync();
                if (i == 0)
                {
                    date = "26/10/2015";
                }
                else if (i == 1)
                {
                    date = "26/10/2015";
                }
                else if (i == 2)
                {
                    date = "30/10/2015";
                }
                else if (i == 3)
                {
                    date = "31/10/2015";
                }
                else if (i == 4)
                {
                    date = "01/11/2015";
                }
                for (int j = 0; j < noOfsubitems; j++)
                {
                    string date1 = date + " " + subitemDet.Items[i].SubItems[j].ImagePath;
                    DateTime dt = DateTime.ParseExact(date1, "g", provider);
                    var diffInSeconds = (dt - ddt).TotalSeconds;
                    if (diffInSeconds < 0 && diffInSeconds > -7200)
                    {
                        evobj.Add(subitemDet.Items[i].SubItems[j]);
                    }
                    else if (diffInSeconds > 0 && diffInSeconds < 10800)
                    {
                        evobj.Add(subitemDet.Items[i].SubItems[j]);
                    }

                    //DateTime dt = Convert.ToDateTime(date + subitemDet.Items[0].SubItems[0].ImagePath);





                    MessageDialog msgbox7 = new MessageDialog("conv");
                    await msgbox7.ShowAsync();
                   
                }
            }
            FavListView.ItemsSource = evobj;
        }
        private async void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            await evntnow();


        }
        private void Mappin_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string mappinid = (string)btn.Tag;
            Frame.Navigate(typeof(Map), mappinid);

        }
        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            string subitemId = ((SampleDataSubItem)e.ClickedItem).UniqueId;
            // ListViewItem btn = sender as ListViewItem;
            // string subitemId = (string)btn.Tag;
            var item = await SampleDataSource.IsItem((string)subitemId);
            if (item == 0)
            {
                Frame.Navigate(typeof(SubItemPage), subitemId);

            }
            else
            {
                Frame.Navigate(typeof(ItemPage), subitemId);
            }
        }
        #endregion
    }
}

