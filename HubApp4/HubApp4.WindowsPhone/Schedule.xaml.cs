﻿using HubApp4.Common;
using HubApp4.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HubApp4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>


    /// <summary>
    /// Invoked when this page is about to be displayed in a Frame.
    /// </summary>
    /// <param name="e">Event data that describes how this page was reached.
    /// This parameter is typically used to configure the page.</param>

    public sealed partial class Schedule : Page
    {
        private readonly NavigationHelper navigationHelper;
        private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();
       

        public Schedule()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            var subitemDet = await SampleDataSource.GetGroupAsync("Schedule");
            this.DefaultViewModel["Group"] = subitemDet;
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {

        }
       

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        #region NavigationHelper registration
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }
        private void Mappin_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string mappinid = (string)btn.Tag;
            //object mappinid = (e.OriginalSource as Button).Tag;

           // var mappinid = ((SampleDataSubItem)e.ClickedItem).UniqueId;
            Frame.Navigate(typeof(Map), mappinid);


        }
        
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            string subitemId = ((SampleDataSubItem)e.ClickedItem).Id;
            var item = await SampleDataSource.IsItem((string)subitemId);
            if (item==0)
            {
                Frame.Navigate(typeof(SubItemPage), subitemId);

            }

            else
            {
               
                Frame.Navigate(typeof(ItemPage), subitemId);
            }


        }

    }
}
