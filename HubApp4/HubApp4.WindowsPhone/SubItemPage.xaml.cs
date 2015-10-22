using HubApp4.Common;
using HubApp4.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Hub Application template is documented at http://go.microsoft.com/fwlink/?LinkID=391641

namespace HubApp4
{
    /// <summary>
    /// A page that displays details for a single item within a group.
    /// </summary>
    public sealed partial class SubItemPage : Page
    {
        private readonly NavigationHelper navigationHelper;
        private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();
        public SampleDataSubItem subitem1;

        public SubItemPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        /// 
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
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>.
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            //var subId = (string)e.NavigationParameter;
            var subitem = await SampleDataSource.GetSubItemAsync((string)e.NavigationParameter);
            this.DefaultViewModel["SubItem"] = subitem;
            subitem1 = await SampleDataSource.GetSubItemAsync2(subitem.UniqueId);

            // else
            //   test.Text = "Returned Nothing";


        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/>.</param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            // TODO: Save the unique state of the page here.
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
        private async void MarkFav_Click(object sender, RoutedEventArgs e)
        {
            // AppBarButton btn = sender as AppBarButton;
            // string uid = (string)btn.Tag;
            // Frame.Navigate(typeof(Favourite),uid);
            await AddEntryIntoJsonAsync();
            MarkFav.Visibility = Visibility.Collapsed;
            MarkUnfav.Visibility = Visibility.Visible;

        }
        private async void MarkUnfav_Click(object sender, RoutedEventArgs e)
        {
            await DeleteEntryIntoJsonAsync();
            MarkFav.Visibility = Visibility.Visible;
            MarkUnfav.Visibility = Visibility.Collapsed;
        }
        private async Task AddEntryIntoJsonAsync()
        {
            string content = String.Empty;
            List<FavClass> ListCars = new List<FavClass>();
            StorageFolder local = ApplicationData.Current.LocalFolder;

            // Create a new file named DataFile.txt.
            var file = await local.CreateFileAsync("DataFile.json",
            CreationCollisionOption.OpenIfExists);
            // }
            if (local != null)
            {
                // var dataFolder = await local.GetFolderAsync("DataFolder");
                var myStream = await local.OpenStreamForReadAsync("DataFile.json");
                using (StreamReader reader = new StreamReader(myStream))
                {
                    content = await reader.ReadToEndAsync();
                }
                if (content != "")
                {
                    //Now add one more Entry.
                    ListCars = FavClass.ConvertToFavEvent(content);
                }
                //if (item != null)
                ListCars.Add(new FavClass() { UniqueId = subitem1.UniqueId, Id = subitem1.Id, Title = subitem1.Title, Subtitle = subitem1.Subtitle, ImagePath = subitem1.ImagePath, Content = subitem1.Content });
                //else
                //  ListCars.Add(new FavClass() { UniqueId = subitem.UniqueId, Id = subitem.Id, Title = subitem.Title, Subtitle = subitem.Subtitle, ImagePath = subitem.ImagePath, Content = subitem.Content });
                try
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<FavClass>));
                    using (var stream = await local.OpenStreamForWriteAsync(
                                  "DataFile.json",
                                  CreationCollisionOption.ReplaceExisting))
                    {
                        serializer.WriteObject(stream, ListCars);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }
        private async Task DeleteEntryIntoJsonAsync()
        {
            string content = String.Empty;
            List<FavClass> ListCars = new List<FavClass>();
            StorageFolder local = ApplicationData.Current.LocalFolder;
            // Create a new file named DataFile.txt.
            var file = await local.CreateFileAsync("DataFile.json",
            CreationCollisionOption.OpenIfExists);

            if (local != null)
            {
                // var dataFolder = await local.GetFolderAsync("DataFolder");
                var myStream = await local.OpenStreamForReadAsync("DataFile.json");
                using (StreamReader reader = new StreamReader(myStream))
                {
                    content = await reader.ReadToEndAsync();
                }
                if (content != "")
                {
                    //Now add one more Entry.
                    ListCars = FavClass.ConvertToFavEvent(content);

                    foreach (var favEvent in ListCars)
                    {
                        if (String.Compare(favEvent.UniqueId, subitem1.UniqueId) == 0)
                        {
                            ListCars.Remove((FavClass)favEvent);
                        }
                    }


                    //if (item != null)
                    //ListCars.Add(new FavClass() { UniqueId = item1.UniqueId, Id = item1.Id, Title = item1.Title, Subtitle = item1.Subtitle, ImagePath = item1.ImagePath, Content = item1.Content });
                    //else
                    //  ListCars.Add(new FavClass() { UniqueId = subitem.UniqueId, Id = subitem.Id, Title = subitem.Title, Subtitle = subitem.Subtitle, ImagePath = subitem.ImagePath, Content = subitem.Content });
                    try
                    {
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<FavClass>));
                        using (var stream = await local.OpenStreamForWriteAsync(
                                      "DataFile.json",
                                      CreationCollisionOption.ReplaceExisting))
                        {
                            serializer.WriteObject(stream, ListCars);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }

            }
        }

    }
}
