﻿

#pragma checksum "E:\Documents\Downloads\Oasis app\oasis15-WinApp (1)\oasis15-WinApp (1)\oasis15-WinApp (1)\HubApp4\HubApp4.WindowsPhone\SubItemPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D12F9D7BEEBD2BB9C5900B20B34E3D7A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HubApp4
{
    partial class SubItemPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 15 "..\..\SubItemPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.MarkFav_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 16 "..\..\SubItemPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.MarkUnfav_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}

