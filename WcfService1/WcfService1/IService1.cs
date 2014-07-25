﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        Oceny GetOcena(int id_s, int id_p);//chcemy całą ocenę (obiekt)

        [OperationContract]
        List<KrotkaOcen> GetOcena(int is_s);

        [OperationContract]
        int SetOcena(int id_s, int id_p, int wartosc_o); //identyfikator oceny chcemy (id_o)

        [OperationContract]
        Przedmioty GetPrzedmiot(int id_p);

        [OperationContract]
        int SetPrzedmiot(string nazwa_p);

        
    }

    //przenosimy to do 'DataClassDesigner1', ale nie disłownie, tuylko tworząc datacontract i datamember
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}
}
