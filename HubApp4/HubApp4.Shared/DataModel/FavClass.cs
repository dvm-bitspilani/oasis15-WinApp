using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Windows.Data.Json;
using Windows.Storage;

namespace HubApp4
{
    public class FavClass
    {
        /*  public FavClass()
          {

          }
           public FavClass(String uniqueId, String id,String title, String subtitle,  String imagePath, String content)
          {
              this.UniqueId = uniqueId;
              this.Id = id;
              this.Title = title;
             this.Subtitle = subtitle;
            //  this.Description = description;
              this.ImagePath = imagePath;
              this.Content = content;
           
          }*/
        public string UniqueId { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ImagePath { get; set; }
        public string Content { get; set; }

        public static List<FavClass> ConvertToFavEvent(string json)
        {
            List<FavClass> fvc = new List<FavClass>();
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<FavClass>));
                using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
                {
                    fvc = serializer.ReadObject(ms) as List<FavClass>;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return fvc;
        }
    }
    /* public sealed class SampleFavSource
     {
         private static SampleFavSource _sampleFavSource = new SampleFavSource();
         private ObservableCollection<FavClass> _fav = new ObservableCollection<FavClass>();
         public ObservableCollection<FavClass> Groups
         {
             get { return this._fav; }
         }
         public static async Task<IEnumerable<SampleFavSource>> GetFavsAsync()
         {
            await _sampleFavSource.GetSampleDataAsync();
            return _sampleFavSource.Groups;
         }
         public static async Task<SampleFavSource> GetFavAsync(string uniqueId)
         {
             await _sampleFavSource.GetSampleDataAsync();
             var matches = _sampleFavSource.Groups.Where((fav)=>fav.UniqueId.Equals(uniqueId));
             if (matches.Count() == 1) return matches.First();
             return null;
         }

         private async Task GetSampleDataAsync()
         {
             if (this._fav.Count != 0)
                 return;

             Uri dataUri = new Uri("ms-appx:///DataModel/Fav.json");

             StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
             string jsonText = await FileIO.ReadTextAsync(file);
             JsonObject jsonObject = JsonObject.Parse(jsonText);
             JsonArray jsonArray = jsonObject["Groups"].GetArray();

             foreach (JsonValue favValue in jsonArray)
             {
                 JsonObject groupObject = favValue.GetObject();
                 FavClass group = new FavClass(groupObject["UniqueId"].GetString(),
                                                groupObject["Id"].GetString(),
                                                             groupObject["Title"].GetString(),
                                                             groupObject["Subtitle"].GetString(),
                                                             groupObject["ImagePath"].GetString(),
                                                             groupObject["Content"].GetString()
                     // groupObject["Description"].GetString()
                                                            );
                 this.Groups.Add(group);
             }

             //throw new NotImplementedException();
         }
     }
     */
    /*  public sealed class SampleDataSource
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
         if (matches.Count() == 1) return matches.First();
         return null;
     }*/
    /*
11          public string Id { get; set; }
12          public int  Score { get; set; }
13          public string Time { get; set; }
14          
15  
16          public static List<GameScore> ConvertToGameScoreItems(string json)
17          {
18              List<GameScore> rsp = new List<GameScore>();
19              try
20              {
21                  DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<GameScore>));
22                  using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
23                  {
24                      rsp = serializer.ReadObject(ms) as List<GameScore>;
25                  }
26              }
27              catch(Exception ex) { }
28              return rsp;
29          }
30      */

}
