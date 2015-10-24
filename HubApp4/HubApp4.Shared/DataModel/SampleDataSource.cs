﻿
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http.Filters;

// The data model defined by this file serves as a representative example of a strongly-typed
// model.  The property names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs. If using this model, you might improve app 
// responsiveness by initiating the data loading task in the code behind for App.xaml when the app 
// is first launched.

namespace HubApp4.Data
{
    /// <summary>
    /// Generic item data model.
    /// </summary>
     
    public class SampleDataItem
    {
        public SampleDataItem(String uniqueId, String title, String subtitle,  String imagePath, String content)
        {
            this.UniqueId = uniqueId;
          
            this.Title = title;
           this.Subtitle = subtitle;
          //  this.Description = description;
            this.ImagePath = imagePath;
            this.Content = content;
            this.SubItems = new ObservableCollection<SampleDataSubItem>();
        }
        public ObservableCollection<SampleDataSubItem> SubItems { get; private set; }

        public string UniqueId { get; private set; }

        public string Title { get; private set; }
        public string Subtitle { get; private set; }
     //   public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string Content { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    public class SampleDataSubItem
    {
        public SampleDataSubItem(String uniqueId, String title, String subtitle, String imagePath, String content)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            //  this.Description = description;
            this.ImagePath = imagePath;
            this.Content = content;
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        //   public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string Content { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }


    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class SampleDataGroup
    {
        public SampleDataGroup(String uniqueId, String title)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
       //     this.Subtitle = subtitle;
         //   this.Description = description;
        //    this.ImagePath = imagePath;
            this.Items = new ObservableCollection<SampleDataItem>();
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
       // public string Subtitle { get; private set; }
     //   public string Description { get; private set; }
     //   public string ImagePath { get; private set; }
        public ObservableCollection<SampleDataItem> Items { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class SampleDataSource
    {
        private static SampleDataSource _sampleDataSource = new SampleDataSource();

        private ObservableCollection<SampleDataGroup> _groups = new ObservableCollection<SampleDataGroup>();
        public ObservableCollection<SampleDataGroup> Groups
        {
            get { return this._groups; }
        }

        public static async Task<IEnumerable<SampleDataGroup>> GetGroupsAsync()
        {
            await _sampleDataSource.GetSampleDataAsync();

            return _sampleDataSource.Groups;
        }

        public static async Task<SampleDataGroup> GetGroupAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.Groups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() >= 1) return matches.First();
            return null;
        }

        public static async Task<SampleDataItem> GetItemAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.Groups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() >= 1) 
                return matches.First();
            return null;
        }
        public static async Task<SampleDataItem> GetItemAsync3(string title)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.Groups.SelectMany(group => group.Items).Where((item) => item.Title.Equals(title));
            if (matches.Count() >= 1)
                return matches.First();
            return null;
        }
        public static async Task<int> IsItem(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.Groups.SelectMany(group => group.Items).Where((Items) => Items.UniqueId.Equals(uniqueId));
            if (matches.Count() >= 1)
                return 1;
            else
            return 0;
        }

        public static async Task<SampleDataSubItem> GetSubItemAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            
           var matches = _sampleDataSource.Groups.SelectMany(group => group.Items);
                var match2 = matches.SelectMany(item => item.SubItems).Where((subitem) => subitem.UniqueId.Equals(uniqueId));
                if (match2.Count() >= 1)
                {
                    return match2.First();
                }
             else            
            return null;
        }
        public static async Task<SampleDataSubItem> GetSubItemAsync2(string uniqueid)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets

            var matches = _sampleDataSource.Groups.SelectMany(group => group.Items);
            var match2 = matches.SelectMany(item => item.SubItems).Where((subitem) => subitem.UniqueId.Equals(uniqueid));
            if (match2.Count() >= 1)
            {
                return match2.First();
            }

            return null;
        }
        public static async Task<SampleDataSubItem> GetSubItemAsync3(string title)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets

            var matches = _sampleDataSource.Groups.SelectMany(group => group.Items);
            var match2 = matches.SelectMany(item => item.SubItems).Where((subitem) => subitem.Title.Equals(title));
            if (match2.Count() >= 1)
            {
                return match2.First();
            }
            else
                return null;
        }


        private async Task GetSampleDataAsync()
        {
            if (this._groups.Count != 0)
                return;

            Uri dataUri = new Uri("ms-appx:///DataModel/SampleData.json");

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await FileIO.ReadTextAsync(file);
            try
            {


              //  Windows.Web.Http.HttpClient client = new Windows.Web.Http.HttpClient();
               // var jsonText = await client.GetStringAsync(new Uri("http://bits-bosm.org/SampleData.json"));
                JsonObject jsonObject = JsonObject.Parse(jsonText);
                JsonArray jsonArray = jsonObject["Groups"].GetArray();

                foreach (JsonValue groupValue in jsonArray)
                {
                    JsonObject groupObject = groupValue.GetObject();
                    SampleDataGroup group = new SampleDataGroup(groupObject["UniqueId"].GetString(),
                                                                groupObject["Title"].GetString()
                                                               //groupObject["Subtitle"].GetString(),
                                                               // groupObject["ImagePath"].GetString(),
                                                               // groupObject["Description"].GetString()
                                                               );
                    int i = 0;
                    foreach (JsonValue itemValue in groupObject["Items"].GetArray())
                    {

                        JsonObject itemObject = itemValue.GetObject();
                        group.Items.Add(new SampleDataItem(itemObject["UniqueId"].GetString(),
                                                          
                                                           itemObject["Title"].GetString(),
                                                          itemObject["Subtitle"].GetString(),
                                                           itemObject["ImagePath"].GetString(),
                                                           // itemObject["Description"].GetString(),
                                                           itemObject["Content"].GetString()));

                        foreach (JsonValue subitemValue in itemObject["SubItems"].GetArray())

                        {
                            JsonObject subitemObject = subitemValue.GetObject();
                            group.Items[i].SubItems.Add(new SampleDataSubItem(subitemObject["UniqueId"].GetString(),
                                                              
                                                               subitemObject["Title"].GetString(),
                                                              subitemObject["Subtitle"].GetString(),
                                                               subitemObject["ImagePath"].GetString(),
                                                               // itemObject["Description"].GetString(),
                                                               subitemObject["Content"].GetString()));
                        }
                        i++;
                    }
                    this.Groups.Add(group);
                }
            } 
            catch(Exception ex)
            {
                //if(ex.Message== "Exception from HRESULT: 0x80072EE7")
                //{
                //    MessageDialog msgbox3 = new MessageDialog("Check Your Network Connection. Web Access is required to get data.");
                //    await msgbox3.ShowAsync();
                //}
                //else
                //{
                    MessageDialog msgbox3 = new MessageDialog("There was a problem in getting data from the server. Error Message: "+ex.Message);
                   // await msgbox3.ShowAsync();
                //}
            }
            }
    }
}