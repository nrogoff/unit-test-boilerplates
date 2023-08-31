using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using SampleDAL;

namespace UnitTestSampleAPIFunction.Functions
{
    public class GetCustomers
    {
        private ILogger _logger;
        private readonly IConfiguration _config;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="log">The host injects ILogger and ILoggerFactory services into constructors.</param>
        /// <param name="config">The Configuration object injected by the host</param>
        public GetCustomers(IConfiguration config)
        {
            // _logger = log;
            _config = config;
        }

        /// <summary>
        /// Get Customers API. This is a sample API to demonstrate how to write unit tests for Azure Functions. It simulates some of the features of OData, but is not using the OData SDK.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName("GetCustomers")]
        // Add these OpenAPI attributes
        [OpenApiOperation(operationId: "GetCustomers", tags: new[] { "customers" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "top", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "The number of records it wants to retrieve in each page")]
        [OpenApiParameter(name: "skip", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "The offset from the beginning of the results")]
        [OpenApiParameter(name: "filter", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "used to filter the results of a data query, so that only a subset of the data that satisfies the specified condition is returned.")]
        [OpenApiParameter(name: "count", In = ParameterLocation.Query, Required = false, Type = typeof(bool), Description = "Enable the count to be returned. Default = true")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomerListDto), Description = "The list of Customers, Total Count and Link to the next page.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NoContent, contentType: "application/json", bodyType: typeof(CustomerListDto), Description = "The request was not understood. Possibly a parameter is missing or incorrect.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "No customers matching the parameters could be found.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(Exception), Description = "Internal Server Error.")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger logger)
        {
            _logger = logger;
            #region Get the parameters from the query string

            // Each parameter has a default value in the configuration file.
            // If the parameter is not passed in the query string, the default value is used.
            // If the parameter passed in the query string is not valid, the default value is used.

            int top;
            if(!int.TryParse(req.Query["top"], out top))
            {
                top = _config.GetValue<int>("DefaultTop");
            }

            int skip;
            if (!int.TryParse(req.Query["skip"], out skip))
            {
                skip = _config.GetValue<int>("DefaultSkip");
            }

            string filter;
            if (string.IsNullOrEmpty(req.Query["filter"]))
            {
                filter = _config.GetValue<string>("DefaultFilter");
            }
            else
            {
                filter = req.Query["filter"];
            }

            string orderBy;
            if (string.IsNullOrEmpty(req.Query["orderby"]))
            {
                orderBy = _config.GetValue<string>("DefaultOrderBy");
            }
            else
            {
                orderBy = req.Query["orderby"];
            }
            
            bool count;
            if (!bool.TryParse(req.Query["count"], out count))
            {
                count = _config.GetValue<bool>("DefaultCount");
            }
            
            #endregion


            #region Get the parameters from the request body

            //get the parameters from the request body and overwrite the defaults if they are passed in
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            top = data?.top;
            skip = data?.skip;
            filter = data?.filter;
            orderBy = data?.orderby;
            count = data?.count;

            #endregion
            

            #region Application Insights Logging and setting up for timing and metrics

            // Write some meta data, properties and metrics for Application Insights logging
            var logProps = new Dictionary<string, string>
            {
                { "FunctionName", "GetCustomers" },
                { "RecordsRequested", top.ToString() }
            };
            // Create an Event Id
            var eventId = new EventId(1, "GetCustomers");
            _logger.Log(LogLevel.Information, eventId,"GetCustomer Function Called", logProps);
            // or use the explicit log level
            _logger.LogInformation(eventId,"GetCustomer Function Called", logProps);

            //Log some Metrics
            _logger.LogMetric("RecordsRequests",Convert.ToDouble(top));

            #endregion


            string responseMessage = "This HTTP triggered function executed successfully.";


            return new OkObjectResult(responseMessage);
        }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for Customer List Response
    /// </summary>
    public class CustomerListDto
    {
        /// <summary>
        /// Total count of records in the result set
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// List of Customers in this page
        /// </summary>
        public List<SalesLT_Customer> Customers { get; set; }

        /// <summary>
        /// Link to the next page
        /// </summary>
        public string NextLink { get; set; }
    }
}
