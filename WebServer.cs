using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace HitmanPatcher
{
    internal class WebServer
    {
        [Required]
        ILoggingProvider logger;
        public WebServer(ILoggingProvider logger)
        {
            this.logger = logger;
        }

        static string GetResourceStr(string key)
        {
            ResourceManager rm = new ResourceManager("HitmanPatcher.Resources.Resource1",
                                         Assembly.GetExecutingAssembly());

            string str = Encoding.UTF8.GetString((byte[])rm.GetObject(key));
            return str;
        }

        async Task ProcessRequestAsync(HttpListenerRequest request, HttpListenerResponse response)
        {
            var builder = new UriBuilder(request.Url);
            switch (request.Url.Port)
            {
                case 5866:
                    {
                        builder.Host = "config.hitman.io";
                        break;
                    }

                case 5867:
                    {
                        builder.Host = "hm3-service.hitman.io";
                        break;
                    }
            }

            builder.Port = 443;
            builder.Scheme = "https";
            HttpWebRequest destinationRequest = (HttpWebRequest)WebRequest.Create(builder.Uri);
            destinationRequest.Method = request.HttpMethod;
            destinationRequest.ContentType = request.ContentType;
            destinationRequest.ContentLength = request.ContentLength64;
            destinationRequest.UserAgent = request.UserAgent;
            destinationRequest.CookieContainer = new CookieContainer();
            destinationRequest.CookieContainer.Add(request.Cookies);
            destinationRequest.Referer = request.UrlReferrer?.ToString();
            destinationRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            foreach (var key in request.Headers.AllKeys)
            {
                try
                {
                    destinationRequest.Headers.Add(key, request.Headers[key]);
                }
                catch (Exception e)
                {

                }
            }

            bool uriLogged = false;

            Action<string> logFunctionApplied = (string fnName) =>
            {
                if (!uriLogged)
                    logger.log($"[H] ↑ {builder.Uri.PathAndQuery.ToString()}\n");
                uriLogged = true;

                logger.log($"[A] {fnName}");
            };

            Func<string, bool> applyFuncOrNot = (string fnName) =>
            {
                if (MainForm.functionEnabled[fnName])
                {
                    logFunctionApplied(fnName);
                    return true;
                }
                return false;
            };

            var reqStream = request.InputStream;
            var replacedBody = "";

            if (request.Url.PathAndQuery.StartsWith("/authentication/api/userchannel/EventsService/SaveAndSynchronizeEvents4") || request.Url.PathAndQuery.StartsWith("/authentication/api/userchannel/EventsService/SaveEvents2"))
            {
                using (StreamReader reader = new StreamReader(request.InputStream))
                {
                    string inputString = reader.ReadToEnd();
                    JObject obj = JsonConvert.DeserializeObject<JObject>(inputString);

                    var input = JObject.Parse(inputString);

                    var values = input["values"];

                    foreach (var value in values)
                    {
                        var name = value["Name"].Value<string>();
                        System.Console.WriteLine(name);
                        if (name.Contains("Failed") && applyFuncOrNot("gameplay.no-fail-punishment"))
                        {
                            value["Name"] = "replaced";
                        }

                        if (name == "CpdSet")
                        {
                            foreach (var property in ((JObject)value["Value"]).Properties())
                            {
                                if (property.Name.EndsWith("_MissionFailed"))
                                {
                                    if ((bool?)value["Value"][property.Name] == true && applyFuncOrNot("gameplay.no-fail-punishment"))
                                    {
                                        ((JObject)value["Value"]).Remove(property.Name);
                                        ((JObject)value["Value"]).Remove("Inventory");
                                        ((JObject)value["Value"]).Remove("CPD_CampaignStep");
                                        ((JObject)value["Value"]).Remove("MyMoney");
                                    }

                                    break;
                                }
                            }


                        }
                    }

                    var outputString = JsonConvert.SerializeObject(input);

                    /*byte[] responseBody = Encoding.UTF8.GetBytes("");

                    using (Stream responseStream = response.OutputStream)
                    {
                        responseStream.Write(responseBody, 0, responseBody.Length);
                    }

                    response.Close();*/
                    replacedBody = outputString;
                    byte[] byteArray = Encoding.UTF8.GetBytes(outputString);
                    MemoryStream outputStream = new MemoryStream(byteArray);
                }
            }

            if (request.HasEntityBody)
            {
                if (replacedBody.Length != 0)
                {
                    byte[] byte1 = Encoding.UTF8.GetBytes(replacedBody);

                    destinationRequest.ContentType = "application/json";
                    destinationRequest.ContentLength = byte1.Length;
                    Stream stream = destinationRequest.GetRequestStream();
                    stream.Write(byte1, 0, byte1.Length);
                    stream.Close();
                }
                else
                {
                    using (Stream requestBodyStream = reqStream)
                    using (Stream destinationRequestStream = destinationRequest.GetRequestStream())
                    {
                        requestBodyStream.CopyTo(destinationRequestStream);
                    }
                }

            }

            try
            {
                // Get the response from the destination server
                HttpWebResponse destinationResponse = (HttpWebResponse)destinationRequest.GetResponse();
                // Send the response back to the client
                response.ContentType = destinationResponse.ContentType;
                // response.ContentLength64 = destinationResponse.ContentLength/**/;
                response.StatusCode = (int)destinationResponse.StatusCode;
                response.StatusDescription = destinationResponse.StatusDescription;
                response.Cookies = destinationResponse.Cookies;
                foreach (var key in destinationResponse.Headers.AllKeys)
                {
                    try
                    {
                        response.Headers.Add(key, destinationResponse.Headers[key]);
                    }
                    catch (Exception e)
                    {

                    }
                }

                // Read the response body into memory
                MemoryStream responseBodyStream = new MemoryStream();
                using (Stream destinationResponseStream = destinationResponse.GetResponseStream())
                {
                    destinationResponseStream.CopyTo(responseBodyStream);
                }
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                byte[] responseBody = responseBodyStream.ToArray();

                if (request.Url.PathAndQuery.StartsWith("/config/pc-prod"))
                {
                    if (applyFuncOrNot("basic.hook-service-addr"))
                    {
                        string responseBodyString = Encoding.UTF8.GetString(responseBody);
                        responseBodyString = responseBodyString.Replace("https://hm3-service.hitman.io", "http://localhost:5867");
                        responseBody = Encoding.UTF8.GetBytes(responseBodyString);
                    }
                }


                if (request.Url.PathAndQuery.StartsWith("/profiles/page/Stashpoint"))
                {
                    string responseBodyString = Encoding.UTF8.GetString(responseBody);
                    JObject obj = JsonConvert.DeserializeObject<JObject>(responseBodyString);

                    if (!request.Url.Query.Contains("disguise") && applyFuncOrNot("items.add-unlockables-stashpoint"))
                    {
                        var itemsData = new List<JToken>();
                        var unlockables = JsonConvert.DeserializeObject<List<JObject>>(GetResourceStr("allunlockables"));
                        JObject template = JsonConvert.DeserializeObject<JObject>(GetResourceStr("loadoutUnlockableTemplate.json"));
                        foreach (var unlockable in unlockables)
                        {
                            if (!"weapon,gear".Contains((string)unlockable["Type"])) continue;

                            JToken checkout = template.DeepClone();
                            checkout["Item"]["InstanceId"] = Guid.NewGuid();
                            checkout["Item"]["ProfileId"] = Guid.NewGuid();
                            checkout["Item"]["Unlockable"]["Properties"] = unlockable["Properties"];
                            checkout["Item"]["Unlockable"]["Id"] = unlockable["Id"];
                            checkout["Item"]["Unlockable"]["Guid"] = unlockable["Guid"];
                            checkout["Item"]["Unlockable"]["Type"] = unlockable["Type"];
                            checkout["Item"]["Unlockable"]["Subtype"] = unlockable["Subtype"];
                            checkout["SlotId"] = obj["data"]["SlotId"];

                            itemsData.Add(checkout);
                        }

                        obj["data"]["LoadoutItemsData"]["Items"] = JToken.FromObject(itemsData);
                    }

                    if (!request.Url.Query.Contains("disguise") && applyFuncOrNot("items.add-unlockables-num"))
                    {

                        foreach (var item in obj["data"]["LoadoutItemsData"]["Items"])
                        {
                            var id = (string)item["Item"]["Unlockable"]["Properties"]["RepositoryId"];
                            var list = Enumerable.Repeat(id, 100).ToList();
                            item["Item"]["Unlockable"]["Properties"]["RepositoryAssets"] = JToken.FromObject(list);
                        }
                    }

                    Func<string[], string, bool> addItemsByID = (string[] ids, string type) =>
                    {
                        JObject template = JsonConvert.DeserializeObject<JObject>(GetResourceStr("loadoutUnlockableTemplate.json"));
                        var itemsData = new List<JToken>();

                        foreach (var repoid in ids)
                        {
                            var item = template.DeepClone();
                            item["Item"]["Unlockable"]["Properties"]["RepositoryId"] = repoid;
                            item["Item"]["Unlockable"]["Guid"] = Guid.NewGuid();

                            item["Item"]["Unlockable"]["Properties"]["RepositoryAssets"] = JToken.FromObject(new string[] { repoid });
                            item["Item"]["InstanceId"] = Guid.NewGuid();
                            item["Item"]["ProfileId"] = Guid.NewGuid();
                            item["Item"]["Unlockable"]["Type"] = type;
                            item["Item"]["Unlockable"]["Subtype"] = type;
                            item["Item"]["Unlockable"]["Id"] = repoid;
                            item["Item"]["Unlockable"]["Properties"]["Name"] = repoid;
                            item["Item"]["Unlockable"]["Properties"]["Description"] = "自定义物品\n\nOrthrus\nMicroBlock";
                            item["Item"]["Unlockable"]["Properties"]["Id"] = repoid;
                            item["SlotId"] = obj["data"]["SlotId"];
                            itemsData.Add(item);
                        }

                        obj["data"]["LoadoutItemsData"]["Items"] = JToken.FromObject(
                                new JArray(obj["data"]["LoadoutItemsData"]["Items"].Concat(JToken.FromObject(itemsData)))
                                );

                        return true;
                    };

                    if (!request.Url.Query.Contains("disguise") && applyFuncOrNot("items.extra-items-1"))
                    {
                        var itemsData = new List<JToken>();
                        var unlockables = JsonConvert.DeserializeObject<List<JObject>>(GetResourceStr("extra_unlockables1"));
                        JObject template = JsonConvert.DeserializeObject<JObject>(GetResourceStr("loadoutUnlockableTemplate.json"));
                        foreach (var unlockable in unlockables)
                        {
                            JToken checkout = template.DeepClone();
                            checkout["Item"]["InstanceId"] = Guid.NewGuid();
                            checkout["Item"]["ProfileId"] = Guid.NewGuid();
                            checkout["Item"]["Unlockable"]["Properties"] = unlockable["Properties"];
                            checkout["Item"]["Unlockable"]["Id"] = unlockable["Id"];
                            checkout["Item"]["Unlockable"]["Guid"] = unlockable["Guid"];
                            checkout["Item"]["Unlockable"]["Type"] = "ExtraItems1";
                            checkout["Item"]["Unlockable"]["Subtype"] = "ExtraItems1";
                            checkout["SlotId"] = obj["data"]["SlotId"];

                            itemsData.Add(checkout);
                        }

                        obj["data"]["LoadoutItemsData"]["Items"] = JToken.FromObject(
                            obj["data"]["LoadoutItemsData"]["Items"].Concat(JToken.FromObject(itemsData))
                            );

                        addItemsByID(JsonConvert.DeserializeObject<string[]>(GetResourceStr("extra_unlockables1_ids")), "ExtraItems1");
                    }



                    if (applyFuncOrNot("items.custom-repoid"))
                    {
                        string[] items = new string[] { };
                        string fileName = "custom-items.json";

                        if (!File.Exists(fileName))
                            File.WriteAllText(fileName, "[\"1e2bc40b-505a-4cc6-a09c-94470470985b\"]");

                        string json = File.ReadAllText(fileName);
                        items = JsonConvert.DeserializeObject<string[]>(json);
                        addItemsByID(items, "CustomUnlockables");

                        const int ITEMS_PER_PART = 100;
                        for (var x = 0; x < items.Length / ITEMS_PER_PART; x++)
                        {
                            JObject template = JsonConvert.DeserializeObject<JObject>(GetResourceStr("loadoutUnlockableTemplate.json"));

                            var item = template.DeepClone();
                            item["Item"]["Unlockable"]["Properties"]["RepositoryId"] = items[ITEMS_PER_PART * x];
                            item["Item"]["Unlockable"]["Guid"] = Guid.NewGuid();

                            item["Item"]["Unlockable"]["Properties"]["RepositoryAssets"] = JToken.FromObject(
                                items.Skip(ITEMS_PER_PART*x).Take(Math.Min(ITEMS_PER_PART
                                ,items.Length - ITEMS_PER_PART * x )));
                            item["Item"]["InstanceId"] = Guid.NewGuid();
                            item["Item"]["ProfileId"] = Guid.NewGuid();
                            item["Item"]["Unlockable"]["Type"] = "CUSTOM_UNLOCKABLES_ALL_ITEM";
                            item["Item"]["Unlockable"]["Subtype"] = "所有自定义物品";
                            item["Item"]["Unlockable"]["Id"] = items[ITEMS_PER_PART * x];
                            item["Item"]["Unlockable"]["Properties"]["Name"] = "所有自定义物品 Part"+x;
                            item["Item"]["Unlockable"]["Properties"]["Description"] = "所有自定义物品\n\nOrthrus\nMicroBlock";
                            item["Item"]["Unlockable"]["Properties"]["Id"] = items[ITEMS_PER_PART * x];
                            item["SlotId"] = obj["data"]["SlotId"];

                            obj["data"]["LoadoutItemsData"]["Items"] = JToken.FromObject(
                            obj["data"]["LoadoutItemsData"]["Items"].Prepend(JToken.FromObject(item))
                            );
                        }
                       

                       
                    }

                    if (!request.Url.Query.Contains("disguise") && applyFuncOrNot("items.all-items"))
                    {
                        var ids = new List<JToken>();
                        foreach (var item in obj["data"]["LoadoutItemsData"]["Items"])
                        {
                            if (!"weapon,gear".Contains((string)item["Item"]["Unlockable"]["Type"])) continue;
                            ids.Add(item["Item"]["Unlockable"]["Properties"]["RepositoryId"]);
                        }

                        {
                            JObject template = JsonConvert.DeserializeObject<JObject>(GetResourceStr("loadoutUnlockableTemplate.json"));

                            Random random = new Random();
                            int value = random.Next(0, ids.Count);
                            var ico = ids[value];
                            template["Item"]["Unlockable"]["Properties"]["RepositoryId"] = ico;
                            template["Item"]["Unlockable"]["Guid"] = Guid.NewGuid();

                            template["Item"]["Unlockable"]["Properties"]["RepositoryAssets"] = JToken.FromObject(ids);
                            template["Item"]["InstanceId"] = Guid.NewGuid();
                            template["Item"]["ProfileId"] = Guid.NewGuid();
                            template["Item"]["Unlockable"]["Type"] = "All Items";
                            template["Item"]["Unlockable"]["Subtype"] = "All Items";
                            template["Item"]["Unlockable"]["Id"] = "All Items";
                            template["Item"]["Unlockable"]["Properties"]["Name"] = "All Items";
                            template["Item"]["Unlockable"]["Properties"]["Description"] = "选这个就可以获得可以共存的大部分道具每样一个\n\nOrthrus\nMicroBlock";
                            template["Item"]["Unlockable"]["Properties"]["Id"] = "All Items";
                            template["SlotId"] = obj["data"]["SlotId"];

                            obj["data"]["LoadoutItemsData"]["Items"] = JToken.FromObject(
                                obj["data"]["LoadoutItemsData"]["Items"].Prepend(template)
                                );
                        }
                    }

                    if (request.Url.Query.Contains("disguise") && applyFuncOrNot("items.add-unlockables-disguise"))
                    {
                        var itemsData = new List<JToken>();
                        var unlockables = JsonConvert.DeserializeObject<List<JObject>>(GetResourceStr("allunlockables"));
                        JObject template = JsonConvert.DeserializeObject<JObject>(GetResourceStr("loadoutUnlockableTemplate.json"));
                        foreach (var unlockable in unlockables)
                        {
                            if (!"disguise".Contains((string)unlockable["Type"])) continue;

                            JToken checkout = template.DeepClone();
                            checkout["Item"]["InstanceId"] = Guid.NewGuid();
                            checkout["Item"]["ProfileId"] = Guid.NewGuid();
                            checkout["Item"]["Unlockable"]["Properties"] = unlockable["Properties"];
                            checkout["Item"]["Unlockable"]["Id"] = unlockable["Id"];
                            checkout["Item"]["Unlockable"]["Guid"] = unlockable["Guid"];
                            checkout["Item"]["Unlockable"]["Type"] = unlockable["Type"];
                            checkout["Item"]["Unlockable"]["Subtype"] = unlockable["Subtype"];
                            checkout["SlotId"] = obj["data"]["SlotId"];

                            itemsData.Add(checkout);
                        }

                        obj["data"]["LoadoutItemsData"]["Items"] = JToken.FromObject(itemsData);
                    }

                    if (request.Url.Query.Contains("disguise") && applyFuncOrNot("items.add-map-disguises"))
                    {
                        var itemsData = new List<JToken>();
                        JObject template = JsonConvert.DeserializeObject<JObject>(GetResourceStr("loadoutUnlockableTemplate.json"));

                        // Create an HttpClient instance
                        var client = new HttpClient();

                        // Get the query string of the request URL
                        var queryString = request.Url.Query;

                        // Parse the query string into a NameValueCollection
                        var queryParams = HttpUtility.ParseQueryString(queryString);

                        // Get the value of the contractId parameter
                        var contractId = queryParams["contractId"];

                        // Set the auth header in the request
                        client.DefaultRequestHeaders.Add("authorization", request.Headers["authorization"]);

                        var jsonBody = JsonConvert.SerializeObject(new
                        {
                            contractId = contractId,
                            difficultyLevel = 2.000000
                        });

                        // Set the content type to application/json
                        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                        // Make the POST request
                        var responseR = await client.PostAsync("https://hm3-service.hitman.io/authentication/api/userchannel/ChallengesService/GetActiveChallengesAndProgression", content);

                        // Get the response as a string
                        var responseObject = JsonConvert.DeserializeObject<List<JObject>>(await responseR.Content.ReadAsStringAsync());

                        // Find the JObjects that have an "EligibleDisguises" property
                        var eligibleDisguisesObjects = responseObject.Where(jo => ((JObject)jo["Challenge"]?["Definition"]?["Constants"])?.ContainsKey("EligibleDisguises") == true);

                        // Extract the "EligibleDisguises" property from each JObject
                        var eligibleDisguises = eligibleDisguisesObjects.Select(jo => jo["Challenge"]["Definition"]["Constants"]["EligibleDisguises"]);

                        // Convert the "EligibleDisguises" JArrays to lists of strings
                        var eligibleDisguisesList = eligibleDisguises.First().ToObject<List<string>>();

                        foreach (var disguise in eligibleDisguisesList)
                        {
                            JToken checkout = template.DeepClone();
                            checkout["Item"]["InstanceId"] = Guid.NewGuid();
                            checkout["Item"]["ProfileId"] = Guid.NewGuid();
                            checkout["Item"]["Unlockable"]["Properties"]["RepositoryId"] = disguise;
                            checkout["Item"]["Unlockable"]["Id"] = Guid.NewGuid();
                            checkout["Item"]["Unlockable"]["Guid"] = Guid.NewGuid();
                            checkout["Item"]["Unlockable"]["Type"] = "Map Disguises";
                            checkout["Item"]["Unlockable"]["Subtype"] = "Map Disguises";
                            checkout["SlotId"] = obj["data"]["SlotId"];

                            obj["data"]["LoadoutItemsData"]["Items"] = JToken.FromObject(obj["data"]["LoadoutItemsData"]["Items"].Prepend(checkout));
                        }
                    }

                    string modifiedJson = JsonConvert.SerializeObject(obj);
                    responseBody = Encoding.UTF8.GetBytes(modifiedJson);
                }

                if (request.Url.PathAndQuery.StartsWith("/profiles/page/SelectEntrance"))
                {
                    string responseBodyString = Encoding.UTF8.GetString(responseBody);
                    JObject obj = JsonConvert.DeserializeObject<JObject>(responseBodyString);

                    if (applyFuncOrNot("entrance.unlock-all"))
                    {
                        foreach (var entrance in obj["data"]["OrderedUnlocks"])
                        {
                            JObject entranceProperties = (JObject)entrance["Properties"];

                            if (!entranceProperties.ContainsKey("UnlockOrder"))
                            {
                                entranceProperties.Add("UnlockOrder", 0);
                            }
                            else
                            {
                                entranceProperties["UnlockOrder"] = 0;
                            }

                            if (!entranceProperties.ContainsKey("UnlockLevel"))
                            {
                                entranceProperties.Add("UnlockLevel", "-1");
                            }
                            else
                            {
                                entranceProperties["UnlockLevel"] = "-1";
                            }

                            if (!entranceProperties.ContainsKey("UnlockedByDefault"))
                            {
                                entranceProperties.Add("UnlockedByDefault", true);
                            }
                            else
                            {
                                entranceProperties["UnlockedByDefault"] = true;
                            }

                            JObject entranceLoadoutSettings = (JObject)entranceProperties["LoadoutSettings"];
                            if (entranceLoadoutSettings == null) entranceLoadoutSettings = new JObject();

                            if (!entranceLoadoutSettings.ContainsKey("GearSlotsEnabledCount"))
                            {
                                entranceLoadoutSettings.Add("GearSlotsEnabledCount", 999);
                            }
                            else
                            {
                                entranceLoadoutSettings["GearSlotsEnabledCount"] = 999;
                            }

                            if (!entranceLoadoutSettings.ContainsKey("GearSlotsAllowContainers"))
                            {
                                entranceLoadoutSettings.Add("GearSlotsAllowContainers", true);
                            }
                            else
                            {
                                entranceLoadoutSettings["GearSlotsAllowContainers"] = true;
                            }

                            if (!entranceLoadoutSettings.ContainsKey("ConcealedWeaponSlotEnabled"))
                            {
                                entranceLoadoutSettings.Add("ConcealedWeaponSlotEnabled", true);
                            }
                            else
                            {
                                entranceLoadoutSettings["ConcealedWeaponSlotEnabled"] = true;
                            }
                        }
                    }

                    string modifiedJson = JsonConvert.SerializeObject(obj);
                    responseBody = Encoding.UTF8.GetBytes(modifiedJson);
                }

                if (request.Url.PathAndQuery.StartsWith("/profiles/page/SelectAgencyPickup"))
                {
                    string responseBodyString = Encoding.UTF8.GetString(responseBody);
                    JObject obj = JsonConvert.DeserializeObject<JObject>(responseBodyString);

                    if (applyFuncOrNot("agency-pickups.unlock-all"))
                    {
                        foreach (var entrance in obj["data"]["OrderedUnlocks"])
                        {
                            obj["data"]["Unlocked"] = JToken.FromObject(obj["data"]["Unlocked"].Prepend(entrance["Properties"]["RepositoryId"]));
                            JObject entranceProperties = (JObject)entrance["Properties"];

                            if (!entranceProperties.ContainsKey("UnlockOrder"))
                            {
                                entranceProperties.Add("UnlockOrder", 0);
                            }
                            else
                            {
                                entranceProperties["UnlockOrder"] = 1;
                            }

                            if (!entranceProperties.ContainsKey("UnlockLevel"))
                            {
                                entranceProperties.Add("UnlockLevel", "-1");
                            }
                            else
                            {
                                entranceProperties["UnlockLevel"] = "-1";
                            }

                            if (!entranceProperties.ContainsKey("UnlockedByDefault"))
                            {
                                entranceProperties.Add("UnlockedByDefault", true);
                            }
                            else
                            {
                                entranceProperties["UnlockedByDefault"] = true;
                            }
                        }
                    }

                    string modifiedJson = JsonConvert.SerializeObject(obj);
                    responseBody = Encoding.UTF8.GetBytes(modifiedJson);
                }


                if (request.Url.PathAndQuery.StartsWith("/profiles/page/Planning") ||
                    request.Url.PathAndQuery.StartsWith("/authentication/api/userchannel/ContractsService/GetForPlay2"))
                {
                    string responseBodyString = Encoding.UTF8.GetString(responseBody);
                    JObject obj = JsonConvert.DeserializeObject<JObject>(responseBodyString);

                    if (responseBodyString.Contains("CPD_")
                        && applyFuncOrNot("freelancer.all-items"))
                         obj["ContractProgressionData"]["PersistentItems"] = 
                           JArray.Parse(GetResourceStr("all_items_id"));
                    

                    string modifiedJson = JsonConvert.SerializeObject(obj);

                    if (applyFuncOrNot("planning.remove-limits"))
                    {
                        // 依次执行替换
                        modifiedJson = Regex.Replace(modifiedJson, @"""GearSlotsEnabledCount"":\s*\d+", @"""GearSlotsEnabledCount"": 99");
                        modifiedJson = Regex.Replace(modifiedJson, @"""GearSlotsAllowContainers"":\s*false", @"""GearSlotsAllowContainers"": true");
                        modifiedJson = Regex.Replace(modifiedJson, @"""ConcealedWeaponSlotEnabled"":\s*false", @"""ConcealedWeaponSlotEnabled"": true");
                        modifiedJson = Regex.Replace(modifiedJson, @"""Equip"":\s*\["".*?""\]", @"""Equip"": []");
                        modifiedJson = Regex.Replace(modifiedJson, @"""EnableSaving"":false", @"""EnableSaving"":true");
                        modifiedJson = Regex.Replace(modifiedJson, @"MandatoryLoadout", "_MandatoryLoadout");
                        modifiedJson = Regex.Replace(modifiedJson, @"ItemRemovedFromInventory", "_ItemRemovedFromInventory");

                    }

                    responseBody = Encoding.UTF8.GetBytes(modifiedJson);
                }

                using (Stream responseStream = response.OutputStream)
                {
                    responseStream.Write(responseBody, 0, responseBody.Length);
                }

                // Close the response streams
                response.Close();
                destinationResponse.Close();
            }
            catch (Exception e)
            {
                logger.log(e.ToString());
                response.StatusCode = 500;
                response.Close();
            }


        }

        public void Listen()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:5866/");
            listener.Prefixes.Add("http://localhost:5867/"); // https://hm3-service.hitman.io

            // Start the listener
            listener.Start();
            Console.WriteLine("Listening for requests");
            Task.Factory.StartNew(() =>
            {
                // Wait for a request and process it when it arrives
                while (true)
                {

                    // Wait for a request
                    HttpListenerContext context = listener.GetContext();
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response;
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            ProcessRequestAsync(request, response);
                        }
                        catch (Exception e)
                        {
                            logger.log(e.ToString());
                        }
                    });
                }
            });


        }
    }
}
