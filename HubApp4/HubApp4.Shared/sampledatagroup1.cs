using System;
using System.Collections.Generic;
using System.Text;
using Parse;
using System.Collections.ObjectModel;

namespace HubApp4
{
    public sealed class SampleDataSource1
    {
        

        private ObservableCollection<parseresult> _groups = new ObservableCollection<parseresult>();
        public ObservableCollection<parseresult> Groups
        {
            get { return this._groups; }
        }

        
    }
}
