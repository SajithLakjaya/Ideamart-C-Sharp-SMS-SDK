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
using System.Web.Script.Serialization;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for SmsListener
/// </summary>
public abstract class SmsListener : IHttpHandler
{
    private readonly String APPLICATION_RUNING_MESSAGE = "SMS Application Runing";
    public bool IsReusable
    {
        get { return false; }
    }

    public void ProcessRequest(HttpContext context)
    {
        MoSmsResp moSmsResp = null;
        string jsonString="";
        context.Response.ContentType = "application/json";
        try
        {
            byte[] PostData = context.Request.BinaryRead(context.Request.ContentLength);
            jsonString = Encoding.UTF8.GetString(PostData);
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            MoSmsReq moSmsReq = json_serializer.Deserialize<MoSmsReq>(jsonString);
            moSmsResp = GenerateStatus(true);
            onMessage(moSmsReq);
        }
        catch (Exception)
        {
            moSmsResp = GenerateStatus(false);
        }
        finally
        {
            if (jsonString.Equals(""))
                context.Response.Write(APPLICATION_RUNING_MESSAGE);
            else
                context.Response.Write(moSmsResp.ToString());
        }
    }

    public MoSmsResp GenerateStatus(bool Sucess)
    {
        if (Sucess)
            return new MoSmsResp(EnumStatusCode.S1000);
        return new MoSmsResp(EnumStatusCode.E1312);
    }

    protected abstract void onMessage(MoSmsReq moSmsReq);

}