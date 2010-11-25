﻿using System;
using System.Collections.Generic;
using System.Text;
using createsend_dotnet;
using Newtonsoft.Json;

namespace ConsoleRunner
{
    class Program
    {
        //testing - 011ebcadaeb71e9a
        //testing list - aa76d29949e7f10ab28712617634fd0b
        //apicreated  - 277da11f331fc698ad22a66c0c4b5c33
        //list in apicreated - 41a99346539316727de7f24491da29d6
        static void Main(string[] args)
        {
            Subscriber subscriber = new Subscriber("aa76d29949e7f10ab28712617634fd0b");

            List<SubscriberDetail> newSubscribers = new List<SubscriberDetail>();

            SubscriberDetail subscriber1 = new SubscriberDetail("test1@notarealdomain.com", "Test Person 1", new List<SubscriberCustomField>());
            subscriber1.CustomFields.Add(new SubscriberCustomField() { Key = "CustomFieldKey", Value = "Value" });
            subscriber1.CustomFields.Add(new SubscriberCustomField() { Key = "CustomFieldKey2", Value = "Value2" });

            newSubscribers.Add(subscriber1);

            SubscriberDetail subscriber2 = new SubscriberDetail("test2@notarealdomain.com", "Test Person 2", new List<SubscriberCustomField>());
            subscriber2.CustomFields.Add(new SubscriberCustomField() { Key = "CustomFieldKey", Value = "Value3" });
            subscriber2.CustomFields.Add(new SubscriberCustomField() { Key = "CustomFieldKey2", Value = "Value4" });

            newSubscribers.Add(subscriber2);

            try
            {
                BulkImportResults results = subscriber.Import(newSubscribers, true);
                Console.WriteLine(results.TotalNewSubscribers + " subscribers added");
                Console.WriteLine(results.TotalExistingSubscribers + " total subscribers in list");
            }
            catch (CreatesendException ex)
            {
                ErrorResult<BulkImportResults> error = (ErrorResult<BulkImportResults>)ex.Data["ErrorResult"];

                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);

                if (error.ResultData != null)
                {
                    //handle the returned data
                    BulkImportResults results = error.ResultData;

                    //success details are here as normal
                    Console.WriteLine(results.TotalNewSubscribers + " subscribers were still added");

                    //but we also have additional failure detail
                    foreach (ImportResult result in results.FailureDetails)
                    {
                        Console.WriteLine("Failed Address");
                        Console.WriteLine(result.Message + " - " + result.EmailAddress);
                    }
                }
            }
        }
    }
}
