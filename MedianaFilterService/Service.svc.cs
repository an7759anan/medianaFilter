using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using Microsoft.ServiceModel.Web;
using System.Linq;
using System.Net;

[assembly: ContractNamespace("", ClrNamespace = "MedianaFilterService")]

namespace MedianaFilterService
{
    // TODO: Please set IncludeExceptionDetailInFaults to false in production environments
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed), ServiceContract]
    public partial class Service
    {
        /// <summary>
        /// Returns data in response to a HTTP GET request with URIs of the form http://<url-for-svc-file>/GetData?window=3&array=2,80,6,3
        /// </summary>
        /// <param name="window">window size</param>
        /// <param name="array">list of array items that are divided by commas</param>
        /// <returns>mediana filtered data</returns>
        [WebHelp(Comment = "GetData return mediana filterd data")]
        [WebGet(UriTemplate = "GetData?window={n}&array={arrayAsString}")]
        [OperationContract]
        public ResponseBody GetData(int n, string arrayAsString)
        {
            if (n < 0) throw new WebProtocolException(HttpStatusCode.BadRequest, "window cannot be negative", null);
            TargetArray array = new TargetArray();
            array.setArrayFromString(arrayAsString);
            array.filterArray(n);
            return new ResponseBody()
            {
                Value = array.array
            };
        }
    }

    /// <summary>
    /// Response body structure.
    /// </summary>
    public class ResponseBody
    {
        public int[] Value { get; set; }
    }
}