using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using static ExportSample.Module.BusinessObjects.Enums;

namespace ExportSample.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    public class Customer : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Customer(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Gender = Genders.Male;
            MaritalStatus = MaritalStatus.Single;
        }


        string firstName;
        [Size(100)]
        [VisibleInListView(false)]
        public string FirstName { get => firstName; set => SetPropertyValue(nameof(FirstName), ref firstName, value); }


        string middleName;
        [Size(100)]
        [VisibleInListView(false)]
        public string MiddleName
        {
            get => middleName;
            set => SetPropertyValue(nameof(MiddleName), ref middleName, value);
        }


        string lastName;
        [Size(100)]
        [VisibleInListView(false)]
        public string LastName { get => lastName; set => SetPropertyValue(nameof(LastName), ref lastName, value); }


        [PersistentAlias(
            "Iif(IsNullOrEmpty(MiddleName), Concat(FirstName, ' ', Iif(IsNullOrEmpty(LastName),'',LastName)), Concat(FirstName, ' ', MiddleName, ' ', LastName))")]
        public string FullName
        {
            get
            {
                try
                {
                    return EvaluateAlias("FullName") as String;
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }


        Genders gender;

        public Genders Gender { get => gender; set => SetPropertyValue(nameof(Gender), ref gender, value); }


        DateTime dateofBirth;
        [ModelDefault("DisplayFormat", "{0: MM/dd/yyyy}")]
        [ModelDefault("EditMask", "MM/dd/yyyy")]
        [VisibleInListView(false)]
        public DateTime DateOfBirth
        {
            get => dateofBirth;
            set => SetPropertyValue(nameof(DateOfBirth), ref dateofBirth, value);
        }


        MaritalStatus maritalStatus;

        public MaritalStatus MaritalStatus
        {
            get => maritalStatus;
            set => SetPropertyValue(nameof(MaritalStatus), ref maritalStatus, value);
        }


        string homePhone;

        [Size(20)]
        [VisibleInListView(false)]
        [ModelDefault("EditMask", "(000)000-0000")]
        [ModelDefault("DisplayFormat", "{0:(###)###-####}")]
        public string HomePhone { get => homePhone; set => SetPropertyValue(nameof(HomePhone), ref homePhone, value); }

        string officePhone;

        [Size(20)]
        [VisibleInListView(false)]
        [ModelDefault("EditMask", "(000)000-0000")]
        [ModelDefault("DisplayFormat", "{0:(###)###-####}")]
        public string OfficePhone
        {
            get => officePhone;
            set => SetPropertyValue(nameof(OfficePhone), ref officePhone, value);
        }

        string mobilePhone;

        [Size(20)]
        [ModelDefault("EditMask", "(000)000-0000")]
        [ModelDefault("DisplayFormat", "{0:(###)###-####}")]
        public string MobilePhone
        {
            get => mobilePhone;
            set => SetPropertyValue(nameof(MobilePhone), ref mobilePhone, value);
        }





    }
}