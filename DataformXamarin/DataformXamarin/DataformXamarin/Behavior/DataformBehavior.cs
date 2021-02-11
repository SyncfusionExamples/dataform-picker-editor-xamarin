using Syncfusion.XForms.DataForm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DataformXamarin
{
    public class DataformBehavior : Behavior<ContentPage>
    {
        public DataformBehavior()
        {

        }
        SfDataForm dataForm = null;
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            dataForm = bindable.FindByName<SfDataForm>("dataForm");
            dataForm.SourceProvider = new SourceProviderExt();
            dataForm.RegisterEditor("City", "Picker");
            dataForm.DataObject = new ContactInfo();
            dataForm.AutoGeneratingDataFormItem += DataForm_AutoGeneratingDataFormItem;

        }
        private void DataForm_AutoGeneratingDataFormItem(object sender, AutoGeneratingDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null && e.DataFormItem.Name == "City")
            {
                if (Device.RuntimePlatform != Device.UWP)
                {
                    (e.DataFormItem as DataFormPickerItem).DisplayMemberPath = "City";
                    (e.DataFormItem as DataFormPickerItem).ValueMemberPath = "PostalCode";
                }
            }
        }
    }
    public class SourceProviderExt : SourceProvider
    {
        public override IList GetSource(string sourceName)
        {
            if (sourceName == "City")
            {
                List<Address> details = new List<Address>();
                details.Add(new Address() { City = "Chennai", PostalCode = 1 });
                details.Add(new Address() { City = "Paris", PostalCode = 2 });
                details.Add(new Address() { City = "Vatican", PostalCode = 3 });

                return details;
            }
            return new List<string>();
        }
    }
}

    