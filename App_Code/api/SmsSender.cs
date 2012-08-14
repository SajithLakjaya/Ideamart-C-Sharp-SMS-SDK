/*   
   Copyright 2012 Sajith Lakjaya

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Web.Script.Serialization;
using System.Text;

/// <summary>
/// Summary description for SmsSender
/// </summary>
public class SmsSender
{   
    public String url { get; set; }

    public SmsSender(String url)
    {
        this.url = url;
    }

    public MtSmsResp sendSMSReq(MtSmsReq mtSmsReq)
    {
        StreamWriter requestWriter;
        MtSmsResp mtSmsResp=null;

        try
        {
            var webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;

            if (webRequest != null)
            {
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json";
                using (requestWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    requestWriter.Write(mtSmsReq.ToString());
                }
            }

            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader responseReader = new StreamReader(responseStream);
            String jsonResponseString = responseReader.ReadToEnd();

            JavaScriptSerializer javascriptserializer = new JavaScriptSerializer();
            mtSmsResp = javascriptserializer.Deserialize<MtSmsResp>(jsonResponseString);
        }
        catch (Exception ex)
        {
            throw new SdpException(ex);
        }
        return mtSmsResp;
    }

}